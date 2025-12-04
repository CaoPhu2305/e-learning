using e_learning.Filters;
using e_learning.Helper;
using e_learning.Models;
using Services.DTO;
using Services.Implamentatios;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_learning.Controllers
{
    [NoCache]
    public class LectureController : Controller
    {
        // GET: Lecture
        //[AuthorizeRole("Course", "View")]       

        private readonly ICourseService _courseService;
        private readonly IQuizzService _quizzService;

        public LectureController(ICourseService courseService,
            IQuizzService quizzService)
        {
            _courseService = courseService;
            _quizzService = quizzService;
        }

        public ActionResult LectureHomePage()
        {
            if (Session["UserID"] == null) return RedirectToAction("Login", "Account");
            int lecturerId = Convert.ToInt32(Session["UserID"]);

            // 1. Lấy danh sách khóa học
            var myCourses = _courseService.GetLecturerCourses(lecturerId);

            // 2. Tính toán thống kê
            ViewBag.TotalCourses = myCourses.Count;

            // [MỚI] Gọi Service lấy số liệu thật
            ViewBag.TotalStudents = _courseService.GetTotalStudents(lecturerId);

            // Phần đánh giá trung bình tạm thời để fake hoặc tính sau
            ViewBag.AvgRating = 4.8;

            return View(myCourses);
        }

        public ActionResult EditCourseContent(int id)
        {
            // Chuyển hướng sang trang ManageCourseContent bạn đã làm
            return RedirectToAction("ManageCourseContent", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourse(int id)
        {
            try
            {
                _courseService.DeleteCourse(id);
                TempData["Success"] = "Đã xóa khóa học thành công!";
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi chi tiết hơn nếu cần
                TempData["Error"] = "Không thể xóa: " + ex.Message;
            }
            return RedirectToAction("LectureHomePage"); // Hoặc Dashboard
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

                    var a = lecturerId;

                    var b = courseDto;

                    var c = fileName;

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


        public ActionResult CreateQuiz(int chapterId)
        {
            ViewBag.ChapterId = chapterId;
            return View();
        }

        [HttpGet]
        public ActionResult EditQuiz(int quizId)
        {
            var quiz = _quizzService.GetQuizzes(quizId);
            if (quiz == null) return HttpNotFound("Không tìm thấy bài kiểm tra.");

            var questionsEntity = _quizzService.GetQuestions(quizId);

            var model = new CreateQuizViewModel
            {
                ChapterId = (int)quiz.ChapterID,
                QuizName = quiz.QuizzesName,
                TimeLimit =60,
                PassScore = quiz.Pass_Score_Percent,
                Questions = new List<QuestionItem>()
            };

            // 4. Map danh sách Câu hỏi và Đáp án
            if (questionsEntity != null && questionsEntity.Count > 0)
            {
                foreach (var q in questionsEntity)
                {
                    // Tạo item câu hỏi cho ViewModel
                    var qItem = new QuestionItem
                    {
                        Content = q.QuestionsContent,
                        Answers = new List<AnswerItem>()
                    };

                    // Lấy danh sách đáp án của câu hỏi này từ Service
                    var answersEntity = _quizzService.GetAnswerOptions((int)q.QuestionsID);

                    if (answersEntity != null)
                    {
                        foreach (var a in answersEntity)
                        {
                            qItem.Answers.Add(new AnswerItem
                            {
                                Content = a.AnswerOptionsName,
                                // Entity của bạn là bool? hay string? 
                                // Nếu DB là bool (1/0) và Entity là bool:
                                IsCorrect = a.IsCorrect == true
                            });
                        }
                    }

                    // Thêm vào danh sách câu hỏi của Model
                    model.Questions.Add(qItem);
                }
            }

            // 5. Truyền QuizId qua ViewBag để dùng cho việc Update sau này
            ViewBag.ChapterId = (int)quiz.ChapterID;
            ViewBag.QuizId = quizId;

            // 6. Trả về View "CreateQuiz" nhưng kèm dữ liệu đã có
            return View("CreateQuiz", model);
        }

        [HttpPost]
        public ActionResult SaveQuiz(CreateQuizViewModel model, int? quizId)
        {
            // DEBUG 1: Kiểm tra đầu vào
            System.Diagnostics.Debug.WriteLine("--- START SAVE QUIZ ---");
            System.Diagnostics.Debug.WriteLine($"QuizId: {quizId}, ChapterId: {model.ChapterId}");

            if (model.Questions == null)
            {
                System.Diagnostics.Debug.WriteLine("LỖI: Model.Questions bị NULL -> Model Binding thất bại.");
                return Json(new { success = false, message = "Lỗi: Dữ liệu câu hỏi bị rỗng (Model Binding Failed)." });
            }

            if (model.Questions.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("LỖI: Model.Questions.Count = 0");
                return Json(new { success = false, message = "Đề thi phải có ít nhất 1 câu hỏi!" });
            }

            try
            {
                // DEBUG 2: Bắt đầu Mapping
                System.Diagnostics.Debug.WriteLine("Bắt đầu Mapping DTO...");

                var quizDto = new QuizDto
                {
                    ChapterId = model.ChapterId,
                    QuizName = model.QuizName,
                    TimeLimit = model.TimeLimit,
                    PassScore = model.PassScore,
                    Questions = model.Questions.Select(q => new QuestionDto
                    {
                        Content = q.Content,
                        // Thêm kiểm tra null ở đây để tránh Crash khi Mapping
                        Answers = (q.Answers != null) ? q.Answers.Select(a => new AnswerDto
                        {
                            Content = a.Content,
                            IsCorrect = a.IsCorrect
                        }).ToList() : new List<AnswerDto>()
                    }).ToList()
                };

                // DEBUG 3: Đến được đây là Mapping thành công
                System.Diagnostics.Debug.WriteLine("Mapping OK. Chuẩn bị gọi Service...");

                bool result;
                if (quizId.HasValue && quizId.Value > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Gọi UPDATE...");
                    result = _quizzService.UpdateFullQuiz(quizId.Value, quizDto);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Gọi CREATE...");
                    result = _quizzService.CreateFullQuiz(quizDto);
                }

                if (result) return Json(new { success = true });
                else return Json(new { success = false, message = "Lỗi khi lưu dữ liệu (Service trả về false)." });
            }
            catch (Exception ex)
            {
                // DEBUG 4: Bắt lỗi Exception
                System.Diagnostics.Debug.WriteLine("EXCEPTION: " + ex.ToString());
                return Json(new { success = false, message = "Lỗi server: " + ex.Message });
            }
        }


    }
}