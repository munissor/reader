using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Services
{
    public interface ITaskService
    {
        void UpdateFeed(long feedId);


        void UpdateFeeds();
    }
}
