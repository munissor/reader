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
            if (subscriptionService == null)
                throw new ArgumentNullException("subscriptionService");

            this.subscriptionService = subscriptionService;
        }

        // GET: api/Subscription
        public IHttpActionResult Get()
        {
            return Ok<IList<Subscription>>(subscriptionService.Get(GetUserId()));
        }

        
        // POST: api/Subscription
        public IHttpActionResult Post([FromBody]Subscription subscription)
        {

            var res = subscriptionService.Post(GetUserId(), subscription);
            if (res)
                return Ok();

            return NotModified();
        }

        
        // DELETE: api/Subscription/5
        public IHttpActionResult Delete(string id)
        {
            var res = subscriptionService.Delete(GetUserId(), id);
            if (res)
                return Ok();

            return NotFound();
        }
    }
}
