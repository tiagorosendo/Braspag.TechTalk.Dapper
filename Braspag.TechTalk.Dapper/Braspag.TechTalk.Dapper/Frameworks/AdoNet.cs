using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Braspag.TechTalk.Dapper.Entidades;
using Braspag.TechTalk.Dapper.Models;

namespace Braspag.TechTalk.Dapper.Frameworks
{
    public class AdoNet : ITestScenario
    {
        private readonly string _connectionString;

        public AdoNet()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["TechTalkConString"].ConnectionString;
        }
        public long GetTransactions(int transactionId)
        {
            var timer = Stopwatch.StartNew();
            using (var con = new SqlConnection(_connectionString))
            {
                var sql = @"select *
                            from [Transaction] where Id = @Id";

                var command = new SqlCommand(sql, con);

                command.Parameters.Add("Id", SqlDbType.Int).Value = transactionId;

                con.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var transaction = new Transaction
                    {
                        Id = reader.GetInt32(0),
                        CardNumber = reader.GetString(1),
                        HolderName = reader.GetString(2),
                        OrderId = reader.GetString(3),
                        TransactionDate = reader.GetDateTime(4),
                        MerchantId = reader.GetInt32(5)
                    };
                }
            }
            timer.Stop();
            return timer.ElapsedMilliseconds;
        }

        public long GetMerchants()
        {
            var timer = Stopwatch.StartNew();
            using (var con = new SqlConnection(_connectionString))
            {
                var sql = @"select m.Id, m.Name
                            from Merchant m";
                var command = new SqlCommand(sql, con);
                con.Open();
                var reader = command.ExecuteReader();

                var merchants = new List<Merchant>();
                while (reader.Read())
                {
                    var merchant = new Merchant
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
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
                var sql = @"INSERT INTO [dbo].[Transaction]
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
                var command = new SqlCommand(sql, con);
                con.Open();

                foreach (var t in listInsert)
                {
                    command.Parameters.Add("@CardNumber", SqlDbType.VarChar).Value = t.CardNumber;
                    command.Parameters.Add("@HolderName", SqlDbType.VarChar).Value = t.HolderName;
                    command.Parameters.Add("@OrderId", SqlDbType.VarChar).Value = t.OrderId;
                    command.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = t.TransactionDate;
                    command.Parameters.Add("@MerchantId", SqlDbType.Int).Value = t.MerchantId;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }

            timer.Stop();
            return timer.ElapsedMilliseconds;

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

                var report = new List<AnalysisResult>();

                while (reader.Read())
                {
                    var result = new AnalysisResult
                    {
                        Id = reader.GetInt32(0),
                        TransactionId = reader.GetInt32(1),
                        RuleId = reader.GetInt32(2),
                        AnalysisDate = reader.GetDateTime(3),
                        Transaction = new Transaction
                        {
                            Id = reader.GetInt32(4),
                            CardNumber = reader.GetString(5),
                            HolderName = reader.GetString(6),
                            OrderId = reader.GetString(7),
                            TransactionDate = reader.GetDateTime(8),
                            MerchantId = reader.GetInt32(9)
                        }
                    };
                    report.Add(result);
                }
            }

            timer.Stop();
            return timer.ElapsedMilliseconds;
        }
    }
}
