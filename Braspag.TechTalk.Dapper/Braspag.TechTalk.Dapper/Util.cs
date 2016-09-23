using System.Collections.Generic;
using System.Linq;
using Braspag.TechTalk.Dapper.Entidades;
using FizzWare.NBuilder;

namespace Braspag.TechTalk.Dapper
{
    public class Util
    {
        public static List<Transaction> GetListOfTransaction(int numberOfTransactions = 5)
        {
            return Builder<Transaction>.CreateListOfSize(numberOfTransactions).All()
                .With(x => x.CardNumber = Faker.RandomNumber.Next(9).ToString())
                .With(x => x.HolderName = Faker.Name.FullName())
                .With(x => x.OrderId = Faker.RandomNumber.Next(99999).ToString())
                .With(x => x.MerchantId = Faker.RandomNumber.Next(1, 5)).Build().ToList();
        }

        public static List<Merchant> GetListMerchants(int numberOfTransactions = 5)
        {
            return Builder<Merchant>.CreateListOfSize(numberOfTransactions).All()
                .With(x => x.Name = Faker.Company.Name().ToString()).Build().ToList();
        }

        public static List<AnalysisResult> GetListAnalysResult(int numberOfTransactions = 5)
        {
            return Builder<AnalysisResult>.CreateListOfSize(numberOfTransactions).All()
                .With(x => x.TransactionId = Faker.RandomNumber.Next(1, 100000)).Build().ToList();
        }


    }
}
