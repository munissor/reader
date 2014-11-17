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

        /// <summary>
        /// Gets the mininimum date time supported by sql server.
        /// </summary>
        /// <remarks>Used as an undefined value for non null DB date fields</remarks>
        protected DateTime MinSqlDate
        {
            get { return System.Data.SqlTypes.SqlDateTime.MinValue.Value; }
 
        }

        public DataContext Context { get; private set; }
    }
}
