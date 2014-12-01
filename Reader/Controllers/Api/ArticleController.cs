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
        public IHttpActionResult Get(string subscriptionId, string articleId, int count, string filter)
        {
            var articles = articleService.Get(GetUserId(), subscriptionId, articleId, count, filter);
            return Ok<IEnumerable<Article>>(articles);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string id)
        {
            var article = articleService.Get(id);
            if (article != null)
                return Ok<ArticleDetail>(article);
            else
                return NotFound();
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Article article)
        {
            var res = articleService.Post(GetUserId(), article);
            if( res )
                return Ok();

            return NotFound();
        }
        
    }
}