USE FinancialPlanner;
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
/*===========================================================================================
-- Author:				Rick Donalson
-- Create date:		12/30/2015
-- Description: This procedure compiles all the user's debits and credits
--	into flat table with daily summaries and a running total.  
--	This flat table is in a chronological ledger arrangement with the sequential 
--	occurrances of every debit and credit within the user's selected timeframe.
--	All items will have these properties; occurrance date, daily summary, running total, 
--	individual amount and period type. 
--	The occurrance date, daily summary and running total will be repeated 
--	for every item that occurred on the given date.  
--	The individual amount and period type will distinct to that item.
--	The procedure's output is supplied to a Flot.js chart and a 
--	DataTables.js grid
--	This is the pathway used; procedure "spCreateLedgerReadout" 
--					->	Imported into an Entity Framework DbContext object "ItemDetailEntities"
--							in "FinancialPlanner.Data.Entity"
--					->	Managed in a Domain-Repository pattern class "LedgerRepository" located in
--							"FinancialPlanner.Infrastructure.Domain.ItemDetail.Ledger.Repository"
--					->	to a WebMethod and then to an jQuery ajax call.
--	Sequences in this procedure:
--		1.	Get the user's debits and credits and put them in the single table, "@ItemDetail".
--				The table has a field "ItemType", intended to aid in their deliniation; 
--				"1" for Credits and "2" for Debits.
--		2.	Generate a chronological list of all dates within the User's selected timeframe 
--				and store them in the "@LedgerMain" table.
--		3.	Iterate through the items in the "@ItemDetail" with the Cursor "curItemDetail".  
--				a.	Within the cursor's body there is 9 different sections for the 9 different 
--						periods:
--								Period	Description
--								------	--------------
--								1				One Time Occurrence
--								2				Daily
--								3				Weekly
--								4				Every Two Weeks
--								5				Bi-Monthly
--								6				Monthly
--								7				Quarterly
--								8				Semi-Annually
--								9				Annually
--						Each is designed to generate the necessary occurrance dates for the 
--						specific period type.
--				b.	Once the occurrance dates are generated in a specific section, transfer the 
--						item data to the "@LedgerDetail" table along with the primary key "PkLMain" 
--						from the "@LedgerMain" table, which placed in the field "FkLMain".
--		4.	Summarized the Item Amounts in the "@LedgerDetail" table for a given occurrance   
--				date and update the "DailyAmount" field in the "@LedgerMain" table for that date.
--		5.	Get the initial checking balance.
--		6.	Finally iterate through all the occurrance dates and updating the "RunningTotal" 
--				by adding the "DailyAmount" from the current date to the "RunningTotal" from the 
--				sequentially earlier date and updating the "RunningTotal" in the current.
--===========================================================================================*/
--CREATE PROCEDURE ItemDetail.spCreateLedgerReadout (
--	@TimeFrameBegin DATE, 
--	@TimeFrameEnd DATE,
--	@UserName NVARCHAR(75)
--) AS
--BEGIN
	SET NOCOUNT ON;
	/* Diagnostic - Convert procedure into script and uncomment */
	DECLARE @TimeFrameBegin DATE, @TimeFrameEnd DATE, @UserName NVARCHAR(75);
	SET @TimeFrameBegin = '2015-08-01';
	SET @TimeFrameEnd = '2017-09-30';
	SET @UserName = 'rick.donalson@me.com';

	DECLARE @workingDate DATE;
	DECLARE @LedgerMain TABLE (
		PkLMain INT IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		WDate DATE NULL,
		DailyAmount FLOAT NULL DEFAULT(0),
		CreditSummary FLOAT NULL DEFAULT(0),
		DebitSummary FLOAT NULL DEFAULT(0),
		RunningTotal FLOAT NULL DEFAULT(0)
	);
	DECLARE @LedgerDetail TABLE (
		PkLDetail INT IDENTITY(1,1) PRIMARY KEY CLUSTERED NOT NULL,
		FkLMain INT NULL,
		ItemType INT NULL,
		FkItemDetail INT NULL,
		PeriodName NVARCHAR(75) NULL,
		Name NVARCHAR(75) NULL,
		Amount MONEY NULL
	);
	DECLARE @ItemDates TABLE (
		ItemType INT NOT NULL,
		FkItemDetail INT NOT NULL,
		OccurrenceDate DATE NOT NULL,
		Name NVARCHAR(75) NULL,
		Amount MONEY NULL,
		PRIMARY KEY CLUSTERED (ItemType, FkItemDetail, OccurrenceDate)
	);
	DECLARE @ItemDetail TABLE (
		FkItemDetail INT NOT NULL,
		ItemType INT NOT NULL,
		Name NVARCHAR(75) NULL,
		Amount MONEY NULL,
		FkPeriod INT NULL,
		PeriodName NVARCHAR(75) NULL,
		BeginDate DATE NULL,
		EndDate DATE NULL,
		WeeklyDOW INT NULL,
		EverOtherWeekDOW INT NULL,
		BiMonthlyDay1 INT NULL,
		BiMonthlyDay2 INT NULL,
		MonthlyDOM INT NULL,
		Quarterly1Month INT NULL,
		Quarterly1Day INT NULL,
		Quarterly2Month INT NULL,
		Quarterly2Day INT NULL,
		Quarterly3Month INT NULL,
		Quarterly3Day INT NULL,
		Quarterly4Month INT NULL,
		Quarterly4Day INT NULL,
		SemiAnnual1Month INT NULL,
		SemiAnnual1Day INT NULL,
		SemiAnnual2Month INT NULL,
		SemiAnnual2Day INT NULL,
		AnnualMOY INT NULL,
		AnnualDOM INT NULL,
		DateRangeReq BIT NOT NULL,
		PRIMARY KEY CLUSTERED (FkItemDetail, ItemType)
	);

	/*==========================================================================
	--	Get the list of Credits & Debits
	==========================================================================*/
	/* Get a list of the Credits that are Every other Week */
	INSERT INTO @ItemDetail (
			ItemType,
			FkItemDetail,
			Name,
			Amount,
			FkPeriod,
			PeriodName,
			BeginDate,
			EndDate,
			WeeklyDOW,
			EverOtherWeekDOW,
			BiMonthlyDay1,
			BiMonthlyDay2,
			MonthlyDOM,
			Quarterly1Month,
			Quarterly1Day,
			Quarterly2Month,
			Quarterly2Day,
			Quarterly3Month,
			Quarterly3Day,
			Quarterly4Month,
			Quarterly4Day,
			SemiAnnual1Month,
			SemiAnnual1Day,
			SemiAnnual2Month,
			SemiAnnual2Day,
			AnnualMOY,
			AnnualDOM,
			DateRangeReq
	)
	SELECT 
			1,  /* Credit */
			PkCredit,
			Name,
			Amount,
			FkPeriod,
			PeriodName,
			BeginDate,
			EndDate,
			WeeklyDOW,
			EverOtherWeekDOW,
			BiMonthlyDay1,
			BiMonthlyDay2,
			MonthlyDOM,
			Quarterly1Month,
			Quarterly1Day,
			Quarterly2Month,
			Quarterly2Day,
			Quarterly3Month,
			Quarterly3Day,
			Quarterly4Month,
			Quarterly4Day,
			SemiAnnual1Month,
			SemiAnnual1Day,
			SemiAnnual2Month,
			SemiAnnual2Day,
			AnnualMOY,
			AnnualDOM,
			DateRangeReq
	FROM ItemDetail.vwCredits
	WHERE UserName = @UserName;

	/* Get a list of the Credits that are Every other Week */
	INSERT INTO @ItemDetail (
			ItemType,
			FkItemDetail,
			Name,
			Amount,
			FkPeriod,
			PeriodName,
			BeginDate,
			EndDate,
			WeeklyDOW,
			EverOtherWeekDOW,
			BiMonthlyDay1,
			BiMonthlyDay2,
			MonthlyDOM,
			Quarterly1Month,
			Quarterly1Day,
			Quarterly2Month,
			Quarterly2Day,
			Quarterly3Month,
			Quarterly3Day,
			Quarterly4Month,
			Quarterly4Day,
			SemiAnnual1Month,
			SemiAnnual1Day,
			SemiAnnual2Month,
			SemiAnnual2Day,
			AnnualMOY,
			AnnualDOM,
			DateRangeReq
	)
	SELECT 
			2,	 /* Debit */
			PkDebit,
			Name,
			/*----------------------------------------------------------------
			--	Debit Amounts are positive when entered in the UI, so 
			--	make them negative here for calculation and display purposes
			----------------------------------------------------------------*/
			(Amount * -1) AS Amount,	
			FkPeriod,
			PeriodName,
			BeginDate,
			EndDate,
			WeeklyDOW,
			EverOtherWeekDOW,
			BiMonthlyDay1,
			BiMonthlyDay2,
			MonthlyDOM,
			Quarterly1Month,
			Quarterly1Day,
			Quarterly2Month,
			Quarterly2Day,
			Quarterly3Month,
			Quarterly3Day,
			Quarterly4Month,
			Quarterly4Day,
			SemiAnnual1Month,
			SemiAnnual1Day,
			SemiAnnual2Month,
			SemiAnnual2Day,
			AnnualMOY,
			AnnualDOM,
			DateRangeReq
	FROM ItemDetail.vwDebits
	WHERE UserName = @UserName;

	/* Diagnostic */
	--SELECT * FROM @ItemDetail;

	/* Initialization of Working date for parent table, "@Working", with list of Dates within time frame */
	SET @workingDate = @TimeFrameBegin;

	/*-------------------------------------------------------------
	--	Initialize the working table with dates within the timeframe
	-------------------------------------------------------------*/
	WHILE (@workingDate < @TimeFrameEnd)
	BEGIN
		SET @workingDate = DATEADD(DAY,1,@workingDate);
		INSERT INTO @LedgerMain (WDate) VALUES (@workingDate);
	END;

	/* Diagnostic */
	--SELECT * FROM @Working

	/*==========================================================================
	--	Begin cycling through the Credits & Debits
	--	Calculate their occurrence dates, match them with the sequential dates
	--	in the @LedgerMain table, then get the row id for that date and insert
	--	into the @LedgerDetail foreign key.
	==========================================================================*/
	/* Declarations & Initializations */
	DECLARE @Month VARCHAR(2), @Day VARCHAR(2);
	DECLARE 	
		@FkItemDetail INT,
		@ItemType INT ,
		@Name NVARCHAR(75),
		@Amount MONEY,
		@FkPeriod INT,
		@PeriodName NVARCHAR(75),
		@BeginDate DATE,
		@EndDate DATE,
		@WeeklyDOW INT,
		@EverOtherWeekDOW INT,
		@BiMonthlyDay1 INT,
		@BiMonthlyDay2 INT,
		@MonthlyDOM INT,
		@Quarterly1Month INT,
		@Quarterly1Day INT,
		@Quarterly2Month INT,
		@Quarterly2Day INT,
		@Quarterly3Month INT,
		@Quarterly3Day INT,
		@Quarterly4Month INT,
		@Quarterly4Day INT,
		@SemiAnnual1Month INT,
		@SemiAnnual1Day INT,
		@SemiAnnual2Month INT,
		@SemiAnnual2Day INT,
		@AnnualMOY INT,
		@AnnualDOM INT,
		@DateRangeReq BIT;

	DECLARE curItemDetail CURSOR FAST_FORWARD READ_ONLY LOCAL FOR
	SELECT 
		ItemType
		,FkItemDetail
		,Name
		,Amount
		,FkPeriod
		,PeriodName
		,BeginDate
		,EndDate
		,WeeklyDOW
		,EverOtherWeekDOW
		,BiMonthlyDay1
		,BiMonthlyDay2
		,MonthlyDOM
		,Quarterly1Month
		,Quarterly1Day
		,Quarterly2Month
		,Quarterly2Day
		,Quarterly3Month
		,Quarterly3Day
		,Quarterly4Month
		,Quarterly4Day
		,SemiAnnual1Month
		,SemiAnnual1Day
		,SemiAnnual2Month
		,SemiAnnual2Day
		,AnnualMOY
		,AnnualDOM
		,DateRangeReq
	FROM @ItemDetail; 

	/* Open cursor and get first record */
	OPEN curItemDetail;
	FETCH NEXT FROM curItemDetail
	INTO 	
		@ItemType
		,@FkItemDetail
		,@Name
		,@Amount
		,@FkPeriod
		,@PeriodName
		,@BeginDate
		,@EndDate
		,@WeeklyDOW
		,@EverOtherWeekDOW
		,@BiMonthlyDay1
		,@BiMonthlyDay2
		,@MonthlyDOM
		,@Quarterly1Month
		,@Quarterly1Day
		,@Quarterly2Month
		,@Quarterly2Day
		,@Quarterly3Month
		,@Quarterly3Day
		,@Quarterly4Month
		,@Quarterly4Day
		,@SemiAnnual1Month
		,@SemiAnnual1Day
		,@SemiAnnual2Month
		,@SemiAnnual2Day
		,@AnnualMOY
		,@AnnualDOM
		,@DateRangeReq;

	/* Diagnostic */
	--PRINT '@TimeFrameBegin: ' + CONVERT(VARCHAR, @TimeFrameBegin) + CHAR(13)
	--		+ '@TimeFrameEnd: ' + CONVERT(VARCHAR, @TimeFrameEnd);

	/* Begin looping */
	WHILE (@@FETCH_STATUS = 0) BEGIN
		/*-------------------------------------------------------------
		-- If there is no timeframe on item, then set @EndDate 
		-- to @TimeFrameEnd date 
		-------------------------------------------------------------*/
		IF (@DateRangeReq = 0)
			SET @EndDate = @TimeFrameEnd;
		IF (@BeginDate IS NULL)
			SET @workingDate = @TimeFrameBegin;
		ELSE
			SET @workingDate = @BeginDate;

		/* Diagnostic */
		--PRINT '@DateRangeReq: ' + CONVERT(VARCHAR, @DateRangeReq) + CHAR(9) 
		--		+ '@BeginDate: ' + ISNULL(CONVERT(VARCHAR, @BeginDate), '          ') + CHAR(9) 
		--		+ '@EndDate: ' + ISNULL(CONVERT(VARCHAR, @EndDate), '          ') + CHAR(9) 
		--		+ '@workingDate: ' + ISNULL(CONVERT(VARCHAR, @workingDate), '          ') + CHAR(9) 
		--		+ '@FkPeriod: ' + ISNULL(CONVERT(VARCHAR, @FkPeriod), ' '); 

		/*==========================================================================
		-- Begin:	Period (1) - One Time Occurrence
		==========================================================================*/
		IF (@FkPeriod = 1) BEGIN
			/*-------------------------------------------------------------
			--	Get the occurrence dates for this item
			-------------------------------------------------------------*/
			INSERT INTO @ItemDates ( 
				ItemType,
				FkItemDetail, 
				OccurrenceDate, 
				Name,
				Amount ) 
			VALUES (
				@ItemType,
				@FkItemDetail, 
				@workingDate, 
				@Name,
				@Amount); 
					
			/* Diagnostic */
			--SELECT * FROM @ItemDates;

			INSERT INTO @LedgerDetail (
				FkLMain,
				ItemType,
				PeriodName,
				FkItemDetail,
				Name,
				Amount )
			SELECT 
				w.PkLMain,
				cd.ItemType,
				@PeriodName,
				cd.FkItemDetail,
				cd.Name,
				cd.Amount
			FROM @LedgerMain AS w
			INNER JOIN @ItemDates AS cd
			ON w.WDate = cd.OccurrenceDate; 
		END;
		/*==========================================================================
		-- End:		Period (1) - One Time Occurrence
		==========================================================================*/

		/*==========================================================================
		-- Begin:	Period (2) - Daily
		==========================================================================*/
		IF (@FkPeriod = 2) BEGIN
			/*-------------------------------------------------------------
			--	Set the working date
			-------------------------------------------------------------*/
			SET @workingDate = DATEADD(DAY,1,@workingDate);

			/*-------------------------------------------------------------
			--	Get the occurrence dates for this item
			-------------------------------------------------------------*/
			WHILE (@workingDate < @EndDate)
			BEGIN
				INSERT INTO @ItemDates ( 
					ItemType,
					FkItemDetail, 
					OccurrenceDate, 
					Name,
					Amount ) 
				VALUES (
					@ItemType,
					@FkItemDetail, 
					@workingDate, 
					@Name,
					@Amount); 

				/* Diagnostic */
				--PRINT @workingDate;

				/* Advance One Day */
				SET @workingDate = DATEADD(DAY,1,@workingDate); 
			END;
		
			/* Diagnostic */
			--SELECT * FROM @ItemDates;

			INSERT INTO @LedgerDetail (
				FkLMain,
				ItemType,
				PeriodName,
				FkItemDetail,
				Name,
				Amount )
			SELECT 
				w.PkLMain,
				cd.ItemType,
				@PeriodName,
				cd.FkItemDetail,
				cd.Name,
				cd.Amount
			FROM @LedgerMain AS w
			INNER JOIN @ItemDates AS cd
			ON w.WDate = cd.OccurrenceDate; 
		END;
		/*==========================================================================
		-- End:		Period (2) - Daily
		==========================================================================*/

		/*==========================================================================
		-- Begin: Period (3) - Weekly
		==========================================================================*/
		IF (@FkPeriod = 3) BEGIN
			/*-------------------------------------------------------------
			--	Find the Day of the Week for this item's first occurrence
			--	and set the working date with that value.
			-------------------------------------------------------------*/
			WHILE (@workingDate < @EndDate)
			BEGIN
				SET @workingDate = DATEADD(DAY,1,@workingDate);
				IF(DATEPART(WEEKDAY,@workingDate) = @WeeklyDOW) 
					BREAK;
				 ELSE
					CONTINUE;
			END;

			/*-------------------------------------------------------------
			--	Get the occurrence dates for this item
			-------------------------------------------------------------*/
			WHILE (@workingDate < @EndDate)
			BEGIN

				INSERT INTO @ItemDates ( 
					ItemType,
					FkItemDetail, 
					OccurrenceDate, 
					Name,
					Amount ) 
				VALUES (
					@ItemType,
					@FkItemDetail, 
					@workingDate, 
					@Name,
					@Amount); 

				/* Diagnostic */
				--PRINT @workingDate;

				/* Advance One Week */
 				SET @workingDate = DATEADD(WEEK,1,@workingDate);
			END;
		
			/* Diagnostic */
			--SELECT * FROM @ItemDates;

			INSERT INTO @LedgerDetail (
				FkLMain,
				ItemType,
				PeriodName,
				FkItemDetail,
				Name,
				Amount )
			SELECT 
				w.PkLMain,
				cd.ItemType,
				@PeriodName,
				cd.FkItemDetail,
				cd.Name,
				cd.Amount
			FROM @LedgerMain AS w
			INNER JOIN @ItemDates AS cd
			ON w.WDate = cd.OccurrenceDate; 
		END;
		/*==========================================================================
		-- End:		Period (3) - Weekly
		==========================================================================*/
	
		/*==========================================================================
		-- Begin:	Period (4) - Every Two Weeks
		==========================================================================*/
		IF (@FkPeriod = 4) BEGIN
			/*-------------------------------------------------------------
			--	Find the Day of the Week for this item's first occurrence
			--	and set the working date with that value.
			-------------------------------------------------------------*/
			WHILE (@workingDate < @EndDate)
			BEGIN
				SET @workingDate = DATEADD(DAY,1,@workingDate);
				IF(DATEPART(WEEKDAY,@workingDate) = @EverOtherWeekDOW) 
					BREAK;
				 ELSE
					CONTINUE;
			END;

			/*-------------------------------------------------------------
			--	Get the occurrence dates for this item
			-------------------------------------------------------------*/
			WHILE (@workingDate < @EndDate)
			BEGIN
				INSERT INTO @ItemDates ( 
					ItemType,
					FkItemDetail, 
					OccurrenceDate, 
					Name,
					Amount ) 
				VALUES (
					@ItemType,
					@FkItemDetail, 
					@workingDate, 
					@Name,
					@Amount); 

				/* Diagnostic */
				--PRINT @workingDate;
							
				/* Advance Two Weeks */
 				SET @workingDate = DATEADD(WEEK,2,@workingDate);
			END;
		
			/* Diagnostic */
			--SELECT * FROM @ItemDates;

			INSERT INTO @LedgerDetail (
				FkLMain,
				ItemType,
				PeriodName,
				FkItemDetail,
				Name,
				Amount )
			SELECT 
				w.PkLMain,
				cd.ItemType,
				@PeriodName,
				cd.FkItemDetail,
				cd.Name,
				cd.Amount
			FROM @LedgerMain AS w
			INNER JOIN @ItemDates AS cd
			ON w.WDate = cd.OccurrenceDate; 
		END;
		/*==========================================================================
		-- End:		Period (4) - Every Two Weeks
		==========================================================================*/
	
		/*==========================================================================
		-- Begin:	Period (5) - Bi-Monthly
		==========================================================================*/
		IF (@FkPeriod = 5) BEGIN
			/*-------------------------------------------------------------
			--	Find the first and second monthly occurrences
			-------------------------------------------------------------*/
			WHILE (@workingDate < @EndDate)
			BEGIN
				IF(DATEPART(DAY, @workingDate) = @BiMonthlyDay1) OR (DATEPART(DAY, @workingDate) = @BiMonthlyDay2) BEGIN 
					/*-------------------------------------------------------------
					--	Get the occurrence dates for this item
					-------------------------------------------------------------*/
					INSERT INTO @ItemDates ( 
						ItemType,
						FkItemDetail, 
						OccurrenceDate, 
						Name,
						Amount ) 
					VALUES (
						@ItemType,
						@FkItemDetail, 
						@workingDate, 
						@Name,
						@Amount); 
					END;
					/* Advance One Day */
					SET @workingDate = DATEADD(DAY,1,@workingDate); 
			END;
				
			/* Diagnostic */
			--SELECT * FROM @ItemDates;

			INSERT INTO @LedgerDetail (
				FkLMain,
				ItemType,
				PeriodName,
				FkItemDetail,
				Name,
				Amount )
			SELECT 
				w.PkLMain,
				cd.ItemType,
				@PeriodName,
				cd.FkItemDetail,
				cd.Name,
				cd.Amount
			FROM @LedgerMain AS w
			INNER JOIN @ItemDates AS cd
			ON w.WDate = cd.OccurrenceDate; 
		END;
		/*==========================================================================
		-- End:		Period (5) - Bi-Monthly
		==========================================================================*/

		/*==========================================================================
		-- Begin:	Period (6) - Monthly
		==========================================================================*/
		IF (@FkPeriod = 6) BEGIN
			/*-------------------------------------------------------------
			--	Find the monthly occurrences
			-------------------------------------------------------------*/
			WHILE (@workingDate < @EndDate)
			BEGIN
				IF(DATEPART(DAY, @workingDate) = @MonthlyDOM) BEGIN 
					/*-------------------------------------------------------------
					--	Get the occurrence dates for this item
					-------------------------------------------------------------*/
					INSERT INTO @ItemDates ( 
						ItemType,
						FkItemDetail, 
						OccurrenceDate, 
						Name,
						Amount ) 
					VALUES (
						@ItemType,
						@FkItemDetail, 
						@workingDate, 
						@Name,
						@Amount); 
				END;
				/* Advance One Day */
				SET @workingDate = DATEADD(DAY,1,@workingDate); 
			END;
				
			/* Diagnostic */
			--SELECT * FROM @ItemDates;

			INSERT INTO @LedgerDetail (
				FkLMain,
				ItemType,
				PeriodName,
				FkItemDetail,
				Name,
				Amount )
			SELECT 
				w.PkLMain,
				cd.ItemType,
				@PeriodName,
				cd.FkItemDetail,
				cd.Name,
				cd.Amount
			FROM @LedgerMain AS w
			INNER JOIN @ItemDates AS cd
			ON w.WDate = cd.OccurrenceDate; 
		END;
		/*==========================================================================
		-- End:		Period (6) - Monthly
		==========================================================================*/
	
		/*==========================================================================
		-- Begin:	Period (7) - Quarterly
		==========================================================================*/
		IF (@FkPeriod = 7) BEGIN
			/*-------------------------------------------------------------
			--	Find the Quarterly occurrences by combining the current year
			--	with the Quarterly Month and Day integer value to create the
			--	Quarterly dates, then cycle through the days of the period
			--	and when there is a match insert the Quarterly Item.
			-------------------------------------------------------------*/
			WHILE (@workingDate < @EndDate)
			BEGIN
				/* Find the month & the day*/			
				SET @Month = DATEPART(MONTH,@workingDate);
				SET @Day = DATEPART(DAY,@workingDate);		

				IF(@Month = @Quarterly1Month AND @Day = @Quarterly1Day) 
					OR (@Month = @Quarterly2Month AND @Day = @Quarterly2Day) 
					OR (@Month = @Quarterly3Month AND @Day = @Quarterly3Day) 
					OR (@Month = @Quarterly4Month AND @Day = @Quarterly4Day) BEGIN 
					/*-------------------------------------------------------------
					--	Get the occurrence dates for this item
					-------------------------------------------------------------*/
					INSERT INTO @ItemDates ( 
						ItemType,
						FkItemDetail, 
						OccurrenceDate, 
						Name,
						Amount ) 
					VALUES (
						@ItemType,
						@FkItemDetail, 
						@workingDate, 
						@Name,
						@Amount); 
					END;
					/* Advance One Day */
					SET @workingDate = DATEADD(DAY,1,@workingDate); 
			END;
						
			/* Diagnostic */
			--SELECT * FROM @ItemDates;

			INSERT INTO @LedgerDetail (
				FkLMain,
				ItemType,
				PeriodName,
				FkItemDetail,
				Name,
				Amount )
			SELECT 
				w.PkLMain,
				cd.ItemType,
				@PeriodName,
				cd.FkItemDetail,
				cd.Name,
				cd.Amount
			FROM @LedgerMain AS w
			INNER JOIN @ItemDates AS cd
			ON w.WDate = cd.OccurrenceDate; 		
		END;
		/*==========================================================================
		-- End:		Period (7) - Quarterly
		==========================================================================*/
	
		/*==========================================================================
		-- Begin:	Period (8) - Semi-Annually
		==========================================================================*/
		IF (@FkPeriod = 8) BEGIN
			/*-------------------------------------------------------------
			--	Find the Quarterly occurrences by combining the current year
			--	with the Quarterly Month and Day integer value to create the
			--	Quarterly dates, then cycle through the days of the period
			--	and when there is a match insert the Quarterly Item.
			-------------------------------------------------------------*/
			WHILE (@workingDate < @EndDate)
			BEGIN
				/* Find the month & the day*/			
				SET @Month = DATEPART(MONTH,@workingDate);
				SET @Day = DATEPART(DAY,@workingDate);		

				IF(@Month = @SemiAnnual1Month AND @Day = @SemiAnnual1Day) 
					OR (@Month = @SemiAnnual2Month AND @Day = @SemiAnnual2Day) BEGIN 
					/*-------------------------------------------------------------
					--	Get the occurrence dates for this item
					-------------------------------------------------------------*/
					INSERT INTO @ItemDates ( 
						ItemType,
						FkItemDetail, 
						OccurrenceDate, 
						Name,
						Amount ) 
					VALUES (
						@ItemType,
						@FkItemDetail, 
						@workingDate, 
						@Name,
						@Amount); 
					END;
					/* Advance One Day */
					SET @workingDate = DATEADD(DAY,1,@workingDate); 
			END;
						
			/* Diagnostic */
			--SELECT * FROM @ItemDates;

			INSERT INTO @LedgerDetail (
				FkLMain,
				ItemType,
				PeriodName,
				FkItemDetail,
				Name,
				Amount )
			SELECT 
				w.PkLMain,
				cd.ItemType,
				@PeriodName,
				cd.FkItemDetail,
				cd.Name,
				cd.Amount
			FROM @LedgerMain AS w
			INNER JOIN @ItemDates AS cd
			ON w.WDate = cd.OccurrenceDate; 
		END;
		/*==========================================================================
		-- End:		Period (8) - Semi-Annually
		==========================================================================*/

		/*==========================================================================
		-- Begin:	Period (9) - Annually
		==========================================================================*/
		IF (@FkPeriod = 9) BEGIN
			/*-------------------------------------------------------------
			--	Find the Quarterly occurrences by combining the current year
			--	with the Quarterly Month and Day integer value to create the
			--	Quarterly dates, then cycle through the days of the period
			--	and when there is a match insert the Quarterly Item.
			-------------------------------------------------------------*/
			WHILE (@workingDate < @EndDate)
			BEGIN
				/* Find the month & the day*/			
				SET @Month = DATEPART(MONTH,@workingDate);
				SET @Day = DATEPART(DAY,@workingDate);		

				IF(@Month = @AnnualMOY AND @Day = @AnnualDOM) BEGIN 
					/*-------------------------------------------------------------
					--	Get the occurrence dates for this item
					-------------------------------------------------------------*/
					INSERT INTO @ItemDates ( 
						ItemType,
						FkItemDetail, 
						OccurrenceDate, 
						Name,
						Amount ) 
					VALUES (
						@ItemType,
						@FkItemDetail, 
						@workingDate, 
						@Name,
						@Amount); 
					END;
					/* Advance One Day */
					SET @workingDate = DATEADD(DAY,1,@workingDate); 
			END;
						
			/* Diagnostic */
			--SELECT * FROM @ItemDates;

			INSERT INTO @LedgerDetail (
				FkLMain,
				ItemType,
				PeriodName,
				FkItemDetail,
				Name,
				Amount )
			SELECT 
				w.PkLMain,
				cd.ItemType,
				@PeriodName,
				cd.FkItemDetail,
				cd.Name,
				cd.Amount
			FROM @LedgerMain AS w
			INNER JOIN @ItemDates AS cd
			ON w.WDate = cd.OccurrenceDate; 
		END;
		/*==========================================================================
		-- End:		Period (9) - Annually
		==========================================================================*/

		/* Reset Table for next group of dates */
		DELETE FROM @ItemDates;

		FETCH NEXT FROM curItemDetail
		INTO 	
			@ItemType
			,@FkItemDetail
			,@Name
			,@Amount
			,@FkPeriod
			,@PeriodName
			,@BeginDate
			,@EndDate
			,@WeeklyDOW
			,@EverOtherWeekDOW
			,@BiMonthlyDay1
			,@BiMonthlyDay2
			,@MonthlyDOM
			,@Quarterly1Month
			,@Quarterly1Day
			,@Quarterly2Month
			,@Quarterly2Day
			,@Quarterly3Month
			,@Quarterly3Day
			,@Quarterly4Month
			,@Quarterly4Day
			,@SemiAnnual1Month
			,@SemiAnnual1Day
			,@SemiAnnual2Month
			,@SemiAnnual2Day
			,@AnnualMOY
			,@AnnualDOM
			,@DateRangeReq;
	END;

	CLOSE curItemDetail;
	DEALLOCATE curItemDetail;
	/*==========================================================================
	-- End of Credit & Debit Cycling
	==========================================================================*/

	/*==========================================================================
	-- Begin Summary & Running Total Updates
	==========================================================================*/
	/*-------------------------------------------------------------
	--	Summarize the items for each day and update the daily 
	--	amount
	-------------------------------------------------------------*/
	UPDATE @LedgerMain 
	SET DailyAmount = wd.DailyAmount,
		CreditSummary = ISNULL(credits.CreditSummary,0),
		DebitSummary = ISNULL(debits.DebitSummary,0)		
	FROM @LedgerMain AS w
	INNER JOIN (
			SELECT 
				wd.FkLMain,
				SUM(wd.Amount) AS DailyAmount			-- Daily Overall Summary
			FROM @LedgerDetail AS wd
			GROUP BY wd.FkLMain
	) AS wd
	ON w.PkLMain = wd.FkLMain
	LEFT OUTER JOIN (
			SELECT 
				wd.FkLMain,
				SUM(wd.Amount) AS CreditSummary		-- Daily Credit Summary
			FROM @LedgerDetail AS wd
			WHERE ItemType = 1
			GROUP BY wd.FkLMain
	) AS credits
	ON w.PkLMain = credits.FkLMain
	LEFT OUTER JOIN (
			SELECT 
				wd.FkLMain,
				SUM(wd.Amount) AS DebitSummary		-- Daily Debit Summary
			FROM @LedgerDetail AS wd
			WHERE ItemType = 2
			GROUP BY wd.FkLMain
	) AS debits
	ON w.PkLMain = debits.FkLMain;;

	/* Diagnostic */
	--SELECT 
	--	wd.FkLMain,
	--	SUM(wd.Amount) AS DailyAmount
	--FROM @LedgerDetail AS wd
	--GROUP BY wd.FkLMain

	/* Diagnostic */
	--SELECT 
	--	wd.FkLMain,
	--	SUM(wd.Amount) AS CreditSummary
	--FROM @LedgerDetail AS wd
	--WHERE ItemType = 1
	--GROUP BY wd.FkLMain

	/* Diagnostic */
	--SELECT 
	--	wd.FkLMain,
	--	SUM(wd.Amount) AS DebitSummary
	--FROM @LedgerDetail AS wd
	--WHERE ItemType = 2
	--GROUP BY wd.FkLMain

	/*-------------------------------------------------------------
	--	Get the initial checking balance
	-------------------------------------------------------------*/
	DECLARE @InitialAmount MONEY;
	SELECT @InitialAmount = Amount 
	FROM ItemDetail.InitialAmount
	WHERE UserName = @UserName;

	/* Diagnostic */
	--PRINT @InitialAmount;

	/*-------------------------------------------------------------
	--	Update the running total in @LedgerMain
	-------------------------------------------------------------*/
	/* Local declarations & Initializations */
	DECLARE @PkLMain INT, @WDate DATE, @DailyAmount MONEY, @RunningTotal MONEY;

	/* Initialize running total with Initial Amount */
	SET @RunningTotal = @InitialAmount;

	/* Initialize cursor */
	DECLARE curRunningTotal CURSOR LOCAL FOR
	SELECT PkLMain, WDate, DailyAmount
	FROM @LedgerMain ORDER BY WDate FOR UPDATE OF RunningTotal;

	/* Open cursor */
	OPEN curRunningTotal;

	/* Get first record */
	FETCH NEXT FROM curRunningTotal 
	INTO @PkLMain, @WDate, @DailyAmount;

	/* Begin looping */
	WHILE (@@FETCH_STATUS = 0) BEGIN
	
		/* Diagnostic */
		--PRINT '-----------' + CHAR(13) + '** Before **' + CHAR(13) 
		--		+	'@DailyAmount: ' + ISNULL(CONVERT(VARCHAR, @DailyAmount), '-') + CHAR(9) 
		--		+ '@RunningTotal: ' + ISNULL(CONVERT(VARCHAR, @RunningTotal), '-') 

		/* Update running total */
		SET @RunningTotal = @RunningTotal + @DailyAmount;
		/* Update running total in @LedgerMain table */
		UPDATE @LedgerMain SET RunningTotal = @RunningTotal WHERE CURRENT OF curRunningTotal;

		/* Diagnostic */
		--PRINT '** After **' + CHAR(13) 
		--		+	'@DailyAmount: ' + ISNULL(CONVERT(VARCHAR, @DailyAmount), '-') + CHAR(9) 
		--		+ '@RunningTotal: ' + ISNULL(CONVERT(VARCHAR, @RunningTotal), '-') 

		/* Get next record */
		FETCH NEXT FROM curRunningTotal 
		INTO @PkLMain, @WDate, @DailyAmount;  
	END;

	/* Close cursor & Release memory */
	CLOSE curRunningTotal;
	DEALLOCATE curRunningTotal;

	/* Diagnostic */
	--SELECT * FROM @LedgerMain;
	--SELECT * FROM @LedgerDetail ORDER BY FkLMain;

	/*==========================================================================
	-- End Summary Calculations
	==========================================================================*/

	/*==========================================================================
	-- Return Result
	==========================================================================*/
	SELECT 
		w.PkLMain,
		w.WDate,
		w.CreditSummary,
		w.DebitSummary,
		w.DailyAmount,
		w.RunningTotal,
		wd.ItemType,
		wd.FkItemDetail,
		wd.PeriodName,
		wd.Name,
		wd.Amount
	FROM @LedgerMain AS w
	INNER JOIN @LedgerDetail AS wd
	ON w.PkLMain = wd.FkLMain
	ORDER BY w.PkLMain, w.WDate; 
--END;

