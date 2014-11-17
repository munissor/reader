using Reader.Model;
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
    public class ArticleController : BaseController
    {
         private IArticleService articleService;

         public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        // GET api/<controller>
        public IEnumerable<Article> Get(string subscriptionId, string articleId, int count)
        {
            return articleService.Get(GetUserId(), subscriptionId, articleId, count);
        }

        // GET api/<controller>/5
        public ArticleDetail Get(string id)
        {
            return articleService.Get(id);
        }

        // POST api/<controller>
        public void Post([FromBody]Article article)
        {
            articleService.Post(GetUserId(), article);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}