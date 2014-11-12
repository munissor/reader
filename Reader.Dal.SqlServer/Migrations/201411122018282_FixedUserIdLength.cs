namespace Reader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedUserIdLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subscriptions", "UserId", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subscriptions", "UserId", c => c.String(nullable: false));
        }
    }
}
