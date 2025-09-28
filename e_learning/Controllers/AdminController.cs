using e_learning.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_learning.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        //[AuthorizeRole("User", "View")]

        public ActionResult AdminHomePage()
        {
            return View();
        }

    }
}