namespace Reader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeedUrlLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Feeds", "Url", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feeds", "Url", c => c.String(nullable: false));
        }
    }
}
