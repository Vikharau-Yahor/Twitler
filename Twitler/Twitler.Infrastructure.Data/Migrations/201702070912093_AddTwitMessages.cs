namespace Twitler.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTwitMessages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Twits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        DatePost = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Twits", "UserId", "dbo.Users");
            DropIndex("dbo.Twits", new[] { "UserId" });
            DropTable("dbo.Twits");
        }
    }
}
