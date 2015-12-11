namespace MoTMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserIdLink", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserIdLink");
        }
    }
}
