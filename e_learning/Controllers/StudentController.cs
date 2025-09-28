using e_learning.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_learning.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        //[AuthorizeRole("Course", "View")]      
        
        public ActionResult StudentHomePage()
        {
            return View();
        }

    }
}