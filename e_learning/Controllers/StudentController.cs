using Data_Oracle.Entities;
using e_learning.Filters;
using Services.Interfaces;
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
        
        private readonly ICourseService _courseService;
   

        public StudentController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public ActionResult StudentHomePage()
        {

            List<Course> courses = _courseService.GetCourses();

         

            if(courses != null)
            {
                return View(courses);
            }

            ViewBag.Error = "List Course Is Null";

            return View();
        }

    }
}