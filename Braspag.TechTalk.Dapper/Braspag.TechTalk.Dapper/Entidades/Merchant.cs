using System.Collections.Generic;

namespace Braspag.TechTalk.Dapper.Entidades
{
    public class Merchant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Rule> Rules { get; set; }
    }
}
