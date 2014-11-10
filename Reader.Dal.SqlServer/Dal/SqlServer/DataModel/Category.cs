using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Dal.SqlServer.DataModel
{
    public class Category
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<Article> Articles { get; set; }
    }
}
