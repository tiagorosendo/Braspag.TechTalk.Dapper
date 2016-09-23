using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using Braspag.TechTalk.Dapper.Entidades;
using Braspag.TechTalk.Dapper.Models;
using Dapper;

namespace Braspag.TechTalk.Dapper.Frameworks
{
    public class Dapper : ITestScenario
    {
        private readonly string _connectionString;

        public Dapper()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["TechTalkConString"].ConnectionString;
        }

        public long GetTransactions(int transactionId)
        {
            var timer = Stopwatch.StartNew();

            using (var con = new SqlConnection(_connectionString))
            {
                var sql = Constants.GetTransactionQuery;

                con.Open();
                var transaction = con.Query<Transaction>(sql, new { Id = transactionId });
            }

            timer.Stop();
            return timer.ElapsedMilliseconds;
        }

        public long GetMerchants()
        {
            var timer = Stopwatch.StartNew();

            using (var con = new SqlConnection(_connectionString))
            {
                var sql = Constants.GetMerchantsQuery;

                con.Open();
                var merchants = con.Query<Merchant>(sql);
            }

            timer.Stop();
            return timer.ElapsedMilliseconds;
        }

        public long InsertTransactions()
        {
            var timer = Stopwatch.StartNew();
            var listInsert = Util.GetListOfTransaction();

            using (var con = new SqlConnection(_connectionString))
            {
                var sql = Constants.InsertTransactionQuery;
                con.Open();
                con.Execute(sql, listInsert);
            }

            timer.Stop();
            return timer.ElapsedMilliseconds;
        }

        public long ReportTransactionsAnalyzed(int merchantId)
        {
            var timer = Stopwatch.StartNew();

            using (var con = new SqlConnection(_connectionString))
            {
                var sql = Constants.ReportTransactionAnalyzedQuery;
                con.Open();
                var report = con.Query<AnalysisResult, Transaction, AnalysisResult>(sql, (analysis, transaction) =>
                {
                    analysis.Transaction = transaction;
                    return analysis;
                }, new { MerchantId = merchantId });
            }

            timer.Stop();
            return timer.ElapsedMilliseconds;
        }
    }
}
