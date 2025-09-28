using e_learning.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_learning.Controllers
{
    public class LectureController : Controller
    {
        // GET: Lecture
        //[AuthorizeRole("Course", "View")]       
        
        public ActionResult LectureHomePage()
        {
            return View();
        }

    }
}