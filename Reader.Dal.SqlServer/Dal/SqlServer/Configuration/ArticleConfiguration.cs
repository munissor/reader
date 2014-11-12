using Reader.Dal.SqlServer.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Dal.SqlServer.Configuration
{
    class ArticleConfiguration : EntityTypeConfiguration<Article>
    {
        public ArticleConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Guid).IsRequired().HasMaxLength(500);
            Property(x => x.Title).HasMaxLength(500);
            Property(x => x.Content);
            Property(x => x.Link).IsRequired();
            Property(x => x.PublicationDate);
            Property(x => x.UpdateDate);
                  
            HasRequired(x => x.Feed).WithMany(x => x.Articles).HasForeignKey(x => x.FeedId);
            
            HasMany(x => x.Authors).WithRequired(x => x.Article).HasForeignKey(x => x.ArticleId);

            HasMany(x => x.Categories)
            .WithMany(x => x.Articles)
            .Map(mc =>
            {
                mc.MapLeftKey("ArticleId");
                mc.MapRightKey("CategoryId");
                mc.ToTable("ArticlesCategories");
            });
        }
    }
}
