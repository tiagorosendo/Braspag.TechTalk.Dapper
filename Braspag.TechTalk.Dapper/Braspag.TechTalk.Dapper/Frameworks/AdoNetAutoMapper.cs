using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Braspag.TechTalk.Dapper.Entidades;
using Braspag.TechTalk.Dapper.Models;

namespace Braspag.TechTalk.Dapper.Frameworks
{
    public class AdoNetAutoMapper : ITestScenario
    {
        private readonly string _connectionString;

        public AdoNetAutoMapper()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["TechTalkConString"].ConnectionString;
        }

        public long GetTransactions(int transactionId)
        {
            throw new System.NotImplementedException();
        }

        public long GetMerchants()
        {
            throw new System.NotImplementedException();
        }

        public long InsertTransactions()
        {
            throw new System.NotImplementedException();
        }

        public long ReportTransactionsAnalyzed(int merchantId)
        {
            var timer = Stopwatch.StartNew();

            using (var con = new SqlConnection(_connectionString))
            {
                var sql = @"select ar.Id,ar.TransactionId,ar.RuleId,ar.Score,ar.AnalysisDate,t.Id,t.CardNumber,t.HolderName,t.OrderId,t.TransactionDate,t.MerchantId
                            from AnalysisResult ar
                            inner join [Transaction] t on t.Id = ar.TransactionId
                            where t.MerchantId = @MerchantId";

                var command = new SqlCommand(sql, con);
                command.Parameters.Add("@MerchantId", SqlDbType.Int).Value = merchantId;
                con.Open();

                var reader = command.ExecuteReader();

                var report = AutoMapper.Mapper.Map<List<AnalysisResult>>(reader);
            }

            timer.Stop();
            return timer.ElapsedMilliseconds;
        }
    }
}
