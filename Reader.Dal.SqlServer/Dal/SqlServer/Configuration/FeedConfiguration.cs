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
    public class FeedConfiguration : EntityTypeConfiguration<Feed>
    {
        public FeedConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Url);
            Property(x => x.Title);
            Property(x => x.Subtitle);
            Property(x => x.LastUpdate);
            Property(x => x.LastDownload);
            Property(x => x.LastDownloadError);

            HasMany(x => x.Articles).WithRequired(x => x.Feed).HasForeignKey(x => x.FeedId);
        }
    }
}
