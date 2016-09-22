using System;
using System.Collections.Generic;
using System.Linq;
using Braspag.TechTalk.Dapper.Frameworks;
using Braspag.TechTalk.Dapper.Models;

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
            }
        }

        private static void ReportTransactions()
        {
            var results = new List<TesteResults>();

            for (var i = 1; i <= NumberOfExecutions; i++)
            {
                var efTest = new EntityFramework();

                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.Entity,
                    RunNumber = i,
                    Time = efTest.ReportTransactionsAnalyzed(i)
                });

                var adoTest = new AdoNet();
                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.AdoNet,
                    RunNumber = i,
                    Time = adoTest.ReportTransactionsAnalyzed(i)
                });

                var adoReaderTest = new AdoNetAutoMapper();
                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.AdoNetAutoMapper,
                    RunNumber = i,
                    Time = adoReaderTest.ReportTransactionsAnalyzed(i)
                });

                var dapperTest = new Frameworks.Dapper();
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

            for (var i = 1; i <= NumberOfExecutions; i++)
            {
                var efTest = new EntityFramework();

                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.Entity,
                    RunNumber = i,
                    Time = efTest.GetTransactions(i)
                });

                var adoTest = new AdoNet();
                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.AdoNet,
                    RunNumber = i,
                    Time = adoTest.GetTransactions(i)
                });

                var adoReaderTest = new AdoNetAutoMapper();
                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.AdoNetAutoMapper,
                    RunNumber = i,
                    Time = adoReaderTest.GetTransactions(i)
                });

                var dapperTest = new Frameworks.Dapper();
                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.Dapper,
                    RunNumber = i,
                    Time = dapperTest.GetTransactions(i)
                });
            }
            ProcessResults(results);
        }

        private static void GetMerchants()
        {
            var results = new List<TesteResults>();

            for (var i = 1; i <= NumberOfExecutions; i++)
            {
                var efTest = new EntityFramework();

                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.Entity,
                    RunNumber = i,
                    Time = efTest.GetMerchants()
                });

                var adoTest = new AdoNet();
                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.AdoNet,
                    RunNumber = i,
                    Time = adoTest.GetMerchants()
                });

                var adoReaderTest = new AdoNetAutoMapper();
                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.AdoNetAutoMapper,
                    RunNumber = i,
                    Time = adoReaderTest.GetMerchants()
                });

                var dapperTest = new Frameworks.Dapper();
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

            for (var i = 1; i <= NumberOfExecutions; i++)
            {
                var efTest = new EntityFramework();

                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.Entity,
                    RunNumber = i,
                    Time = efTest.InsertTransactions()
                });

                var adoTest = new AdoNet();
                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.AdoNet,
                    RunNumber = i,
                    Time = adoTest.InsertTransactions()
                });

                var adoReaderTest = new AdoNetAutoMapper();
                results.Add(new TesteResults
                {
                    Framework = EnumFrameworks.AdoNetAutoMapper,
                    RunNumber = i,
                    Time = adoReaderTest.InsertTransactions()
                });

                var dapperTest = new Frameworks.Dapper();
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
                Console.WriteLine("Run #\tTimes #");
                var orderedResults = group.OrderBy(x => x.RunNumber);
                foreach (var orderResult in orderedResults)
                {
                    Console.WriteLine(orderResult.RunNumber + "\t\t" + orderResult.Time);
                }
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
            Console.WriteLine("Option:");
        }
    }
}
