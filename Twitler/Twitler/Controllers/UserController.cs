using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Twitler.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public ActionResult Main()
        {
            return View();
        }
    }
}