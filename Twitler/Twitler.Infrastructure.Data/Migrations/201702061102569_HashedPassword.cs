namespace Twitler.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HashedPassword : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Password", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String());
        }
    }
}
