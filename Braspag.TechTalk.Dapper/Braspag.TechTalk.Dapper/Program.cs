using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Braspag.TechTalk.Dapper.Frameworks;
using Braspag.TechTalk.Dapper.Models;
using Dapper;

namespace Braspag.TechTalk.Dapper
{
    class Program
    {
        private const int NumberOfExecutions = 5;
        private static void Main(string[] args)
        {

            char option;
            do
            {
                ShowMenu();
                option = Console.ReadLine().First();
                SelectScenario(option);

            } while (option != 'S');
        }

        private static void SelectScenario(char option)
        {
            switch (option)
            {
                case 'S':
                    break;
                case 'A':
                    ReportTransactions();
                    break;
                case 'B':
                    GetTransaction();
                    break;
                case 'C':
                    GetMerchants();
                    break;
                case 'D':
                    InserTransactions();
                    break;
                case 'F':
                    PopulateDb();
                    break;
            }
        }

        private static void ReportTransactions()
        {
            var results = new List<TesteResults>();
            var efTest = new EntityFramework();
            var adoTest = new AdoNet();
            var adoReaderTest = new AdoNetAutoMapper();
            var dapperTest = new Frameworks.Dapper();

            for (var i = 1; i <= NumberOfExecutions; i++)
            {
                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.Entity,
                    RunNumber = i,
                    Time = efTest.ReportTransactionsAnalyzed(i)
                });


                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.AdoNet,
                    RunNumber = i,
                    Time = adoTest.ReportTransactionsAnalyzed(i)
                });


                //results.Add(new TesteResults
                //{
                //    Framework = EnumFrameworks.AdoNetAutoMapper,
                //    RunNumber = i,
                //    Time = adoReaderTest.ReportTransactionsAnalyzed(i)
                //});


                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.Dapper,
                    RunNumber = i,
                    Time = dapperTest.ReportTransactionsAnalyzed(i)
                });
            }
            ProcessResults(results);
        }

        private static void GetTransaction()
        {
            var results = new List<TesteResults>();
            var efTest = new EntityFramework();
            var adoTest = new AdoNet();
            var adoReaderTest = new AdoNetAutoMapper();
            var dapperTest = new Frameworks.Dapper();

            for (var i = 1; i <= NumberOfExecutions; i++)
            {
                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.Entity,
                    RunNumber = i,
                    Time = efTest.GetTransactions(Faker.RandomNumber.Next(1, 102000))
                });


                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.AdoNet,
                    RunNumber = i,
                    Time = adoTest.GetTransactions(Faker.RandomNumber.Next(1, 102000))
                });


                //results.Add(new TesteResults
                //{
                //    Framework = EnumFrameworks.AdoNetAutoMapper,
                //    RunNumber = i,
                //    Time = adoReaderTest.GetTransactions(i)
                //});


                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.Dapper,
                    RunNumber = i,
                    Time = dapperTest.GetTransactions(Faker.RandomNumber.Next(1, 102000))
                });
            }
            ProcessResults(results);
        }

        private static void GetMerchants()
        {
            var results = new List<TesteResults>();
            var efTest = new EntityFramework();
            var adoTest = new AdoNet();
            var adoReaderTest = new AdoNetAutoMapper();
            var dapperTest = new Frameworks.Dapper();

            for (var i = 1; i <= NumberOfExecutions; i++)
            {

                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.Entity,
                    RunNumber = i,
                    Time = efTest.GetMerchants()
                });


                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.AdoNet,
                    RunNumber = i,
                    Time = adoTest.GetMerchants()
                });


                //results.Add(new TesteResults
                //{
                //    Framework = EnumFrameworks.AdoNetAutoMapper,
                //    RunNumber = i,
                //    Time = adoReaderTest.GetMerchants()
                //});


                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.Dapper,
                    RunNumber = i,
                    Time = dapperTest.GetMerchants()
                });
            }
            ProcessResults(results);
        }

        private static void InserTransactions()
        {
            var results = new List<TesteResults>();
            var efTest = new EntityFramework();
            var adoTest = new AdoNet();
            var dapperTest = new Frameworks.Dapper();
            var adoReaderTest = new AdoNetAutoMapper();
            for (var i = 1; i <= NumberOfExecutions; i++)
            {


                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.Entity,
                    RunNumber = i,
                    Time = efTest.InsertTransactions()
                });


                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.AdoNet,
                    RunNumber = i,
                    Time = adoTest.InsertTransactions()
                });


                //results.Add(new TesteResults
                //{
                //    Framework = EnumFrameworks.AdoNetAutoMapper,
                //    RunNumber = i,
                //    Time = adoReaderTest.InsertTransactions()
                //});


                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.Dapper,
                    RunNumber = i,
                    Time = dapperTest.InsertTransactions()
                });
            }
            ProcessResults(results);
        }

        public static void ProcessResults(List<TesteResults> results)
        {
            var groupedResults = results.GroupBy(x => x.Framework);
            foreach (var group in groupedResults)
            {
                Console.WriteLine(group.Key.ToString() + " Results");
                Console.WriteLine("Run #\t\tTimes #");
                var orderedResults = group.OrderBy(x => x.RunNumber);
                foreach (var orderResult in orderedResults)
                {
                    Console.WriteLine(orderResult.RunNumber + "\t\t" + orderResult.Time);
                }

                Console.WriteLine("Avenger Time #");
                var avengerTimes = Math.Round(group.Average(x => x.Time), 2);
                Console.WriteLine(avengerTimes);
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Escolha uma opcao: ");
            Console.WriteLine("S - Sair");
            Console.WriteLine("A - Executar Relatorio de Transacoes Analisadas");
            Console.WriteLine("B - Executar Get Transacao especifica");
            Console.WriteLine("C - Executar Get Merchants");
            Console.WriteLine("D - Insert 5 Transacoes");
            Console.WriteLine("F - Preencher a BASE");
            Console.WriteLine("Option:");
        }

        private static void PopulateDb()
        {
            var connString = ConfigurationManager.ConnectionStrings["TechTalkConString"].ConnectionString;

            using (var con = new SqlConnection(connString))
            {
                Console.WriteLine("Criando Lista de Merchants... Aguarde...");
                var listMerchants = Task.Run(() => Util.GetListMerchants(300000));

                Console.WriteLine("Criando Lista de Transacoes... Aguarde...");
                var listTransactions = Task.Run(() => Util.GetListOfTransaction(102000));

                Console.WriteLine("Criando Lista de AnalysisResult... Aguarde...");
                var listAnalysisResult = Task.Run(() => Util.GetListAnalysResult(100000));

                Task.WaitAll(listMerchants, listTransactions, listAnalysisResult);

                Console.WriteLine("Inserinto Merchants... Aguarde...");
                con.Execute(Constants.InsertMerchant, listMerchants.Result);
                Console.WriteLine("Merchants inseridas com Sucesso");

                Console.WriteLine("Inserinto Transacoes... Aguarde...");
                con.Execute(Constants.InsertTransactionQuery, listTransactions.Result);
                Console.WriteLine("Transacoes inseridas com Sucesso");

                Console.WriteLine("Inserinto AnalysisResult... Aguarde...");
                con.Execute(Constants.InsertAnalysisResult, listAnalysisResult.Result);
                Console.WriteLine("Transacoes inseridas com Sucesso");


            }
        }
    }
}
