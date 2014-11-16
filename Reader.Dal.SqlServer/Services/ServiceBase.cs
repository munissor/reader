using Reader.Dal.SqlServer.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Services
{
    public class ServiceBase
    {
        protected ServiceBase(DataContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.Context = context;
        }

        public DataContext Context { get; private set; }
    }
}
