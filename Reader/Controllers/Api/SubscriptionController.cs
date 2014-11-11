using Reader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Reader.Controllers.Api
{
    [Authorize]
    public class SubscriptionController : BaseController
    {
        // GET: api/Subscription
        public IEnumerable<SubscriptionViewModel> Get()
        {
            return new SubscriptionViewModel[]{};
        }

        // GET: api/Subscription/5
        public SubscriptionViewModel Get(string id)
        {
            return null;
        }

        // POST: api/Subscription
        public void Post([FromBody]SubscriptionViewModel value)
        {
            // add
        }

        // PUT: api/Subscription/5
        public void Put(string id, [FromBody]SubscriptionViewModel value)
        {
            // upd
        }

        // DELETE: api/Subscription/5
        public void Delete(string id)
        {
        }
    }
}
