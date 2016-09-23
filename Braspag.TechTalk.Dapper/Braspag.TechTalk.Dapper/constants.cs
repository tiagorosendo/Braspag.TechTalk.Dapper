namespace Braspag.TechTalk.Dapper
{
    public static class Constants
    {
        public static readonly string InsertTransactionQuery = @"INSERT INTO [dbo].[Transaction]
                           ([CardNumber]
                           ,[HolderName]
                           ,[OrderId]
                           ,[TransactionDate]
                           ,[MerchantId])
                     VALUES
                           (@CardNumber,
                            @HolderName,
                            @OrderId,
                            @TransactionDate,
                            @MerchantId)";

        public static readonly string ReportTransactionAnalyzedQuery =
            @"select ar.Id,ar.TransactionId,ar.RuleId,ar.Score,ar.AnalysisDate,t.Id,t.CardNumber,t.HolderName,t.OrderId,t.TransactionDate,t.MerchantId
                            from AnalysisResult ar
                            inner join [Transaction] t on t.Id = ar.TransactionId
                            where t.MerchantId = @MerchantId";

        public static readonly string GetMerchantsQuery = @"select m.Id, m.Name
                            from Merchant m";

        public static readonly string GetTransactionQuery = @"select *
                            from [Transaction] where Id = @Id";

        public static readonly string InsertMerchant = @"insert into [Merchant] Values (@Name)";

        public static readonly string InsertAnalysisResult = @"INSERT INTO [dbo].[AnalysisResult]
           ([TransactionId]
           ,[RuleId]
           ,[Score]
           ,[AnalysisDate])
     VALUES
           (@TransactionId,
		    @RuleId,
			@Score,
			@AnalysisDate)";
    }
}
