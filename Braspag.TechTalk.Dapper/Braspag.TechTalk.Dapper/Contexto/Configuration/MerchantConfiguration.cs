using System.Data.Entity.ModelConfiguration;
using Braspag.TechTalk.Dapper.Entidades;

namespace Braspag.TechTalk.Dapper.Contexto.Configuration
{
    public class MerchantConfiguration : EntityTypeConfiguration<Merchant>
    {
        public MerchantConfiguration()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).HasMaxLength(50).IsRequired();
        }
    }

    public class RuleConfiguration : EntityTypeConfiguration<Rule>
    {
        public RuleConfiguration()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).HasMaxLength(50).IsRequired();

            HasRequired(x => x.Merchant).WithMany(x => x.Rules).HasForeignKey(x => x.MerchantId);
        }
    }

    public class TransactionConfiguration : EntityTypeConfiguration<Transaction>
    {
        public TransactionConfiguration()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.CardNumber).IsRequired();
            Property(x => x.HolderName).IsRequired();
            Property(x => x.OrderId).IsRequired();
            Property(x => x.TransactionDate).IsRequired();

            HasRequired(x => x.Merchant).WithMany().HasForeignKey(x => x.MerchantId);
        }
    }

    public class AnalysisResultConfiguration : EntityTypeConfiguration<AnalysisResult>
    {
        public AnalysisResultConfiguration()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Score).IsRequired();
            Property(x => x.AnalysisDate).IsRequired();

            HasRequired(x => x.Transaction).WithMany().HasForeignKey(x => x.TransactionId);
            HasRequired(x => x.Rule).WithMany().HasForeignKey(x => x.RuleId);
        }
    }
}
