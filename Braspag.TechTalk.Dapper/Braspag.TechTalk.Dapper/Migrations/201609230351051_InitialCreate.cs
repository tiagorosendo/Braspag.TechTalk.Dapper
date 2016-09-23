namespace Braspag.TechTalk.Dapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnalysisResult",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionId = c.Int(nullable: false),
                        RuleId = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        AnalysisDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rule", t => t.RuleId)
                .ForeignKey("dbo.Transaction", t => t.TransactionId)
                .Index(t => t.TransactionId)
                .Index(t => t.RuleId);
            
            CreateTable(
                "dbo.Rule",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        MerchantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Merchant", t => t.MerchantId)
                .Index(t => t.MerchantId);
            
            CreateTable(
                "dbo.Merchant",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardNumber = c.String(nullable: false, maxLength: 100, unicode: false),
                        HolderName = c.String(nullable: false, maxLength: 100, unicode: false),
                        OrderId = c.String(nullable: false, maxLength: 100, unicode: false),
                        TransactionDate = c.DateTime(nullable: false),
                        MerchantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Merchant", t => t.MerchantId)
                .Index(t => t.MerchantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnalysisResult", "TransactionId", "dbo.Transaction");
            DropForeignKey("dbo.Transaction", "MerchantId", "dbo.Merchant");
            DropForeignKey("dbo.AnalysisResult", "RuleId", "dbo.Rule");
            DropForeignKey("dbo.Rule", "MerchantId", "dbo.Merchant");
            DropIndex("dbo.Transaction", new[] { "MerchantId" });
            DropIndex("dbo.Rule", new[] { "MerchantId" });
            DropIndex("dbo.AnalysisResult", new[] { "RuleId" });
            DropIndex("dbo.AnalysisResult", new[] { "TransactionId" });
            DropTable("dbo.Transaction");
            DropTable("dbo.Merchant");
            DropTable("dbo.Rule");
            DropTable("dbo.AnalysisResult");
        }
    }
}
