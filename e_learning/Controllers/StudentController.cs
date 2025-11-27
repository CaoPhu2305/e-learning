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

            int currentUserId = 23; // ID người dùng hiện tại (lấy từ Session)

            // Gọi hàm mới để lấy danh sách loại trừ
            List<Course> courses = _courseService.GetUnpurchasedCourses(currentUserId);

            if (courses != null && courses.Any())
            {
                return View(courses);
            }

            // Nếu mua hết sạch rồi thì báo danh sách rỗng hoặc hiển thị thông báo khác
            ViewBag.Error = "Bạn đã sở hữu tất cả khóa học hiện có!";
            return View(new List<Course>());
        }

    }
}