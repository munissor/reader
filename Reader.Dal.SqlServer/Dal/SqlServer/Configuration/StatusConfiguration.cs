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
    public class StatusConfiguration : EntityTypeConfiguration<Status>
    {
        public StatusConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Read).IsRequired();
            Property(x => x.ReadDate).IsRequired();
            Property(x => x.Starred).IsRequired();
            Property(x => x.StarredDate).IsRequired();
        }
    }
}
