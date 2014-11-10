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
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name);

            HasMany(x => x.Articles)
           .WithMany(x => x.Categories)
           .Map(mc =>
           {
               mc.MapLeftKey("CategoryId");
               mc.MapRightKey("ArticleId");
               mc.ToTable("ArticlesCategories");
           });
        }
    }
}
