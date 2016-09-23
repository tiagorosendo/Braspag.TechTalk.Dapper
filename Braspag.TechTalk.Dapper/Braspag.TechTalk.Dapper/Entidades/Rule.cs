namespace Braspag.TechTalk.Dapper.Entidades
{
    public class Rule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MerchantId { get; set; }
        public Merchant Merchant { get; set; }
    }
}
