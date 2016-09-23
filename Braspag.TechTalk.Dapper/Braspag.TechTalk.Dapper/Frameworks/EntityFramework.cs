using System;
using System.Diagnostics;
using System.Linq;
using Braspag.TechTalk.Dapper.Contexto;
using Braspag.TechTalk.Dapper.Models;

namespace Braspag.TechTalk.Dapper.Frameworks
{
    public class EntityFramework : ITestScenario
    {
        public long GetTransactions(int transactionId)
        {
            var timer = Stopwatch.StartNew();
            using (var db = new EfContext())
            {
                var merchants = db.Transaction.FirstOrDefault(x => x.Id == transactionId);
            }
            timer.Stop();
            return timer.ElapsedMilliseconds;
        }

        public long GetMerchants()
        {
            var timer = Stopwatch.StartNew();
            using (var db = new EfContext())
            {
                var merchants = db.Merchant.ToList();
            }
            timer.Stop();
            return timer.ElapsedMilliseconds;
        }

        public long InsertTransactions()
        {
            var timer = Stopwatch.StartNew();
            var listInsert = Util.GetListOfTransaction();

            using (var db = new EfContext())
            {
                foreach (var transaction in listInsert)
                {
                    db.Transaction.Add(transaction);
                }

                db.SaveChanges();
            }

            timer.Stop();
            return timer.ElapsedMilliseconds;
        }

        public long ReportTransactionsAnalyzed(int merchantId)
        {
            var timer = Stopwatch.StartNew();

            using (var db = new EfContext())
            {
                var report = db.AnalysisResult.Where(x => x.Transaction.MerchantId == merchantId).ToList();
            }

            timer.Stop();
            return timer.ElapsedMilliseconds;
        }
    }
}
