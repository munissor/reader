using AutoMapper;
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
        public IList<SubscriptionViewModel> Get()
        {
            var res = subscriptionService.Get(GetUserId());
            return Mapper.Map<IList<Subscription>, IList<SubscriptionViewModel>>(res);
        }

        // GET: api/Subscription/5
        public SubscriptionViewModel Get(string id)
        {
            return null;
        }

        // POST: api/Subscription
        public void Post([FromBody]SubscriptionViewModel value)
        {
            var model = Mapper.Map<SubscriptionViewModel, Subscription>(value);
            subscriptionService.Post(GetUserId(), model);
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
            subscriptionService.Delete(GetUserId(), id);
        }
    }
}
