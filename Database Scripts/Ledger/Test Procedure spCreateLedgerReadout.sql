USE [FinancialPlanner]
GO

DECLARE	@return_value int

EXEC	@return_value = [ItemDetail].[spCreateLedgerReadout]
		@TimeFrameBegin = '2015-08-01',
		@TimeFrameEnd = '2017-09-30',
		--@UserName = N'rick.donalson@me.com'
		@UserName = 'admin@financialplanner.net'

SELECT	'Return Value' = @return_value

GO
