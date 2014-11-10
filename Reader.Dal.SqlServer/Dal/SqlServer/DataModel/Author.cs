using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Dal.SqlServer.DataModel
{
    public class Author
    {
        public virtual long Id { get; set; }

        public virtual long ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        
    }
}
