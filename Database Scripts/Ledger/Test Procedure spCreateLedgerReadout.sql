

DECLARE	@return_value int

EXEC	@return_value = [ItemDetail].[spCreateLedgerReadout]
		@TimeFrameBegin = '1/1/2016',
		@TimeFrameEnd = '1/1/2025',
		@UserName = N'admin@financialplanner.net',
		@GroupingTranform = true

SELECT	'Return Value' = @return_value

GO
