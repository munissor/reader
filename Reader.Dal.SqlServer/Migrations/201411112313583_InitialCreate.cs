namespace Reader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feeds",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Url = c.String(nullable: false),
                        Title = c.String(nullable: false, maxLength: 250),
                        Subtitle = c.String(maxLength: 1000),
                        LastUpdate = c.DateTime(nullable: false),
                        LastDownload = c.DateTime(nullable: false),
                        LastDownloadError = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FeedId = c.Long(nullable: false),
                        Guid = c.String(nullable: false, maxLength: 500),
                        Title = c.String(maxLength: 500),
                        Content = c.String(),
                        Link = c.String(nullable: false),
                        PublicationDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Feeds", t => t.FeedId, cascadeDelete: true)
                .Index(t => t.FeedId);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ArticleId = c.Long(nullable: false),
                        Name = c.String(maxLength: 250),
                        Email = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        FeedId = c.Long(nullable: false),
                        SubscriptionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SubscriptionId = c.Long(nullable: false),
                        ArticleId = c.Long(nullable: false),
                        Read = c.Boolean(nullable: false),
                        ReadDate = c.DateTime(nullable: false),
                        Starred = c.Boolean(nullable: false),
                        StarredDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticlesCategories",
                c => new
                    {
                        ArticleId = c.Long(nullable: false),
                        CategoryId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ArticleId, t.CategoryId })
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "FeedId", "dbo.Feeds");
            DropForeignKey("dbo.ArticlesCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.ArticlesCategories", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Authors", "ArticleId", "dbo.Articles");
            DropIndex("dbo.ArticlesCategories", new[] { "CategoryId" });
            DropIndex("dbo.ArticlesCategories", new[] { "ArticleId" });
            DropIndex("dbo.Authors", new[] { "ArticleId" });
            DropIndex("dbo.Articles", new[] { "FeedId" });
            DropTable("dbo.ArticlesCategories");
            DropTable("dbo.Status");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.Categories");
            DropTable("dbo.Authors");
            DropTable("dbo.Articles");
            DropTable("dbo.Feeds");
        }
    }
}
