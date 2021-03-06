﻿using Reader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Services
{
    public interface IArticleService
    {
        IList<Article> Get(string userId, string subscriptionId, string lastArticleId, int count, string filter);

        ArticleDetail Get(string articleId);

        bool Post(string userId, Article article);
    }
}
