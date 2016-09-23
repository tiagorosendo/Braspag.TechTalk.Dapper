using System;

namespace Braspag.TechTalk.Dapper.Entidades
{
    public class AnalysisResult
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int RuleId { get; set; }
        public int Score { get; set; }
        public DateTime AnalysisDate { get; set; }
        public Transaction Transaction { get; set; }
        public Rule Rule { get; set; }
    }
}
