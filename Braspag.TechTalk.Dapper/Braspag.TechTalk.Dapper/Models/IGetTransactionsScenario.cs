namespace Braspag.TechTalk.Dapper.Models
{
    public interface IGetTransactionsScenario
    {
        long GetTransactions(int transactionId);
    }
}