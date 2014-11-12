using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace Reader.Controllers.Api
{
    public abstract class BaseController : ApiController
    {
        protected string GetUserId()
        {
            return User.Identity.GetUserId();
        }
    }
}