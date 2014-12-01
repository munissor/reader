using Reader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Services
{
    public interface ISubscriptionService
    {
        IList<Subscription> Get(string userId);

        bool Post(string userId, Subscription model);

        bool Delete(string userId, string subscriptionId);
    }
}
