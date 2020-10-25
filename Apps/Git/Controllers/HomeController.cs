using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    using Git.Constants;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect(RedirectConst.AllRepositories);
            }

            return this.View();
        }
    }
}
