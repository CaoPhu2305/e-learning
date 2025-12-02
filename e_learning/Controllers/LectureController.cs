using e_learning.Filters;
using e_learning.Models;
using Services.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_learning.Controllers
{
    public class LectureController : Controller
    {
        // GET: Lecture
        //[AuthorizeRole("Course", "View")]       

        private readonly ICourseService _courseService;

        public LectureController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public ActionResult LectureHomePage()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Sau này có thể lấy thống kê thật từ Service để truyền vào ViewBag
            // Ví dụ:
            // ViewBag.TotalCourses = _courseService.CountCourses(userId);
            // ViewBag.TotalStudents = ...

            return View();
        }

        public ActionResult Dashboard()
        {
            // 1. Kiểm tra Session (Bắt buộc đăng nhập)
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // 2. Lấy ID Giảng viên từ Session
            // Lưu ý: Session lưu object, cần ép kiểu sang int hoặc decimal tùy code bạn
            int lecturerId = Convert.ToInt32(Session["UserID"]);

            // 3. Gọi Service lấy danh sách khóa học
            List<Data_Oracle.Entities.Course> myCourses = _courseService.GetLecturerCourses(lecturerId);

            // 4. Trả về View (Dashboard.cshtml đã tạo trước đó)
            // Model truyền sang View là List<Course>
            return View(myCourses);
        }


        public ActionResult CreateCourse()
        {
            return View();
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(CourseCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // 1. Xử lý upload ảnh
                    string fileName = "default_course.jpg";
                    if (model.ImageFile != null)
                    {
                        string ext = System.IO.Path.GetExtension(model.ImageFile.FileName);
                        fileName = "course_" + DateTime.Now.Ticks + ext;
                        string path = Server.MapPath("~/Content/Assets/images/courses/" + fileName);
                        model.ImageFile.SaveAs(path);
                    }

                    int lecturerId = Convert.ToInt32(Session["UserID"]);

                    // 2. Gọi Service tạo khóa học (Hàm này bạn đã viết ở phần trước)
                    // Lưu ý: Map từ ViewModel sang DTO nếu cần
                    var courseDto = new CourseDto
                    {
                        CourseName = model.CourseName,
                        Description = model.Description,
                        Price = (decimal)model.Price,
                        IsTrialAvailable = model.IsTrialAvailable,
                        // ImageName truyền riêng
                    };

                    // Gọi Service (Hàm này sẽ trả về CourseID vừa tạo)
                    // Bạn cần sửa lại Service một chút để trả về ID thay vì void
                     int newCourseId = _courseService.CreateFullCourse(courseDto, lecturerId, fileName);

                    if (newCourseId > 0)
                    {
                        // 3. Chuyển sang trang quản lý nội dung (Bước 2)
                        return RedirectToAction("ManageCourseContent", new { id = newCourseId });
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi hệ thống: " + ex.Message);
                }
            }
            return View(model);
        }

        public ActionResult ManageCourseContent(int id)
        {
            // 1. Lấy thông tin khóa học
            var course = _courseService.GetCourseByID(id);
            if (course == null) return HttpNotFound("Không tìm thấy khóa học");

            // 2. Lấy Video gốc
            var courseVideo = _courseService.GetCourseVideoByID(id);

            // [QUAN TRỌNG] Kiểm tra nếu chưa có CourseVideo (dữ liệu rác) thì return lỗi hoặc list rỗng
            if (courseVideo == null)
            {
                ViewBag.Course = course;
                ViewBag.Error = "Khóa học này chưa được khởi tạo nội dung video.";
                return View(new List<Data_Oracle.Entities.Chapter>());
            }

            // 3. Lấy danh sách chương
            var chapters = _courseService.GetChapterByCouresID((int)courseVideo.CourseVideoID);

            ViewBag.Course = course;
            return View(chapters);
        }

        // POST: Thêm Chương
        [HttpPost]
        public ActionResult CreateChapter(CreateChapterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map ViewModel -> DTO
                var dto = new ChapterDto
                {
                    CourseId = model.CourseId,
                    ChapterName = model.ChapterName,
                    ChapterCompleted = model.ChapterCompleted
                };

                _courseService.AddChapter(dto);
                TempData["Success"] = "Thêm chương thành công!";
            }
            return RedirectToAction("ManageCourseContent", new { id = model.CourseId });
        }

        // POST: Thêm Bài học
        [HttpPost]
        public ActionResult CreateLesson(CreateLessonViewModel model)
        {
            if (model.VideoFile != null && model.VideoFile.ContentLength > 0)
            {
                // 1. Lưu file video lên Server (Controller làm việc này)
                string fileName = DateTime.Now.Ticks + "_" + model.VideoFile.FileName;
                string path = Server.MapPath("~/Content/Assets/videos/course/" + fileName);
                model.VideoFile.SaveAs(path);

                // 2. Map ViewModel -> DTO
                var dto = new LessonDto
                {
                    ChapterId = model.ChapterId,
                    LessonName = model.LessonName,
                    Description = model.Description,
                    LessonCompleted = model.LessonCompleted
                };

                // 3. Gọi Service để lưu DB (Truyền DTO + Tên file)
                _courseService.AddLesson(dto, fileName);

                TempData["Success"] = "Thêm bài học thành công!";
            }
            else
            {
                TempData["Error"] = "Vui lòng chọn video!";
            }

            return RedirectToAction("ManageCourseContent", new { id = model.CourseId });
        }


    }
}