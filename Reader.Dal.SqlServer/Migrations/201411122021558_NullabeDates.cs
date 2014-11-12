namespace Reader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullabeDates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Feeds", "LastUpdate", c => c.DateTime());
            AlterColumn("dbo.Feeds", "LastDownload", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feeds", "LastDownload", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Feeds", "LastUpdate", c => c.DateTime(nullable: false));
        }
    }
}
