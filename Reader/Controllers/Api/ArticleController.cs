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
    /// <summary>
    /// API to manage articles
    /// </summary>
    [Authorize]
    public class ArticleController : BaseController
    {
        private IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        /// <summary>
        /// Gets a list of article
        /// </summary>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <param name="articleId">The article identifier. Only articles after the specified one will be returned (used for pagination)</param>
        /// <param name="count">The count of articles to be returned.</param>
        /// <param name="filter">Specifies which articles to get (Read, unread, starred)</param>
        /// <returns>The list of articles.</returns>
        public IHttpActionResult Get(string subscriptionId, string articleId, int count, string filter)
        {
            var articles = articleService.Get(GetUserId(), subscriptionId, articleId, count, filter);
            return Ok<IEnumerable<Article>>(articles);
        }

        /// <summary>
        /// Gets an article by ID
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The article requested.</returns>
        public IHttpActionResult Get(string id)
        {
            var article = articleService.Get(id);
            if (article != null)
                return Ok<ArticleDetail>(article);
            else
                return NotFound();
        }

        /// <summary>
        /// Updates the specified article
        /// </summary>
        /// <param name="article">The article to be updated.</param>
        public IHttpActionResult Post([FromBody]Article article)
        {
            var res = articleService.Post(GetUserId(), article);
            if( res )
                return Ok();

            return NotFound();
        }
        
    }
}