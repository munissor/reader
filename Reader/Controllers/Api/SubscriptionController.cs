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
    /// <summary>
    /// API to manage subscriptions to feeds
    /// </summary>
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

        /// <summary>
        /// Gets a list of subscriptions for the logged in user.
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            return Ok<IList<Subscription>>(subscriptionService.Get(GetUserId()));
        }


        /// <summary>
        /// Updates a subscription
        /// </summary>
        /// <param name="subscription">The subscription.</param>
        public IHttpActionResult Post([FromBody]Subscription subscription)
        {

            var res = subscriptionService.Post(GetUserId(), subscription);
            if (res)
                return Ok();

            return NotModified();
        }


        /// <summary>
        /// Deletes the specified subscription by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public IHttpActionResult Delete(string id)
        {
            var res = subscriptionService.Delete(GetUserId(), id);
            if (res)
                return Ok();

            return NotFound();
        }
    }
}
