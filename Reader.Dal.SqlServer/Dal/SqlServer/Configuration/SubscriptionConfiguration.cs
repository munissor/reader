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
    public class SubscriptionConfiguration : EntityTypeConfiguration<Subscription>
    {
        public SubscriptionConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.UserId).IsRequired().HasMaxLength(128);
            Property(x => x.FeedId).IsRequired();
            Property(x => x.SubscriptionDate).IsRequired();

            HasRequired(x => x.Feed).WithMany().HasForeignKey(x => x.FeedId);
        }
    }
}
