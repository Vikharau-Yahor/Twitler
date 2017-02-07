namespace Twitler.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHashTags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HashTags",
                c => new
                    {
                        HashValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HashValue);
            
            CreateTable(
                "dbo.TwitHashTags",
                c => new
                    {
                        Twit_Id = c.Int(nullable: false),
                        HashTag_HashValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Twit_Id, t.HashTag_HashValue })
                .ForeignKey("dbo.Twits", t => t.Twit_Id, cascadeDelete: true)
                .ForeignKey("dbo.HashTags", t => t.HashTag_HashValue, cascadeDelete: true)
                .Index(t => t.Twit_Id)
                .Index(t => t.HashTag_HashValue);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TwitHashTags", "HashTag_HashValue", "dbo.HashTags");
            DropForeignKey("dbo.TwitHashTags", "Twit_Id", "dbo.Twits");
            DropIndex("dbo.TwitHashTags", new[] { "HashTag_HashValue" });
            DropIndex("dbo.TwitHashTags", new[] { "Twit_Id" });
            DropTable("dbo.TwitHashTags");
            DropTable("dbo.HashTags");
        }
    }
}
