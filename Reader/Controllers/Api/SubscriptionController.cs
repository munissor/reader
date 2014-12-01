using Reader.Model;
using Reader.Models;
using Reader.Services;
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
        private ISubscriptionService subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            this.subscriptionService = subscriptionService;
        }

        // GET: api/Subscription
        public IList<Subscription> Get()
        {
            return subscriptionService.Get(GetUserId());
        }

        // GET: api/Subscription/5
        public Subscription Get(string id)
        {
            return null;
        }

        // POST: api/Subscription
        public void Post([FromBody]Subscription subscription)
        {

            subscriptionService.Post(GetUserId(), subscription);
            // add
        }

        // PUT: api/Subscription/5
        public void Put(string id, [FromBody]Subscription value)
        {
            // upd
        }

        // DELETE: api/Subscription/5
        public void Delete(string id)
        {
            subscriptionService.Delete(GetUserId(), id);
        }
    }
}
