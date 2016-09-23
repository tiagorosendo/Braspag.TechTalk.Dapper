using System;

namespace Braspag.TechTalk.Dapper.Entidades
{
    public class Transaction
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string HolderName { get; set; }
        public string OrderId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int MerchantId { get; set; }
        public Merchant Merchant { get; set; }
    }
}
