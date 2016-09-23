using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Braspag.TechTalk.Dapper.Contexto.Configuration;
using Braspag.TechTalk.Dapper.Entidades;

namespace Braspag.TechTalk.Dapper.Contexto
{
    public class EfContext : DbContext
    {
        public EfContext() : base("TechTalkConString")
        {
        }

        public IDbSet<Merchant> Merchant { get; set; }
        public IDbSet<Rule> Rule { get; set; }
        public IDbSet<Transaction> Transaction { get; set; }
        public IDbSet<AnalysisResult> AnalysisResult { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>().Configure(x => x.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(x => x.HasMaxLength(100));

            modelBuilder.Configurations.Add(new MerchantConfiguration());
            modelBuilder.Configurations.Add(new RuleConfiguration());
            modelBuilder.Configurations.Add(new TransactionConfiguration());
            modelBuilder.Configurations.Add(new AnalysisResultConfiguration());
        }
    }
}
