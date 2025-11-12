using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_learning.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult CourseDetail()
        {
            return View();
        }

        public ActionResult CourseLearn()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoadQuizPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult LoadVideoPartial(string videoUrl)
        {
            ViewBag.VideoUrl = videoUrl;
            return PartialView();
        }

    }
}