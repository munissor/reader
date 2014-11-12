namespace Reader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubscriptionRelation : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Subscriptions", "FeedId");
            AddForeignKey("dbo.Subscriptions", "FeedId", "dbo.Feeds", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "FeedId", "dbo.Feeds");
            DropIndex("dbo.Subscriptions", new[] { "FeedId" });
        }
    }
}
