using Data_Oracle.Entities;
using e_learning.Helper;
using e_learning.Models;
using e_learning.Util;
using Services.Implamentatios;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace e_learning.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course

        private ICourseService _courseService;
        private IQuizzService _quizzService;
        private ICourseRegistrationService _courseRegistrationService;
        private IOderService _oderService;

        public CourseController(ICourseService courseService,
            IQuizzService quizzService,
            ICourseRegistrationService courseRegistrationService,
            IOderService oderService)
            
        {
            _courseService = courseService;
            _quizzService = quizzService;
            _courseRegistrationService = courseRegistrationService;
            _oderService = oderService;
        }

        public ActionResult CourseDetail(int courseID)
        {

            Course course = _courseService.GetCourseByID(courseID);

            if (course.CourseType.CourcesTypeID == 1)
            {

                Data_Oracle.Entities.CourseVideo courseVideo = _courseService.GetCourseVideoByID(courseID);

                List<Chapter> chapters = _courseService.GetChapterByCouresID((int)courseVideo.CourseVideoID);

                List<ChapterViewModel> chapterViewModel = new List<ChapterViewModel>();

                foreach (Chapter chapter in chapters)
                {

                    ChapterViewModel chapterViewModelTmp = new ChapterViewModel();    

                    List<Lession> lessions = _courseService.GetLessionByChapterId((int)chapter.ChapterID);

                    chapterViewModelTmp.InitValue(chapter.ChapterID, chapter.CourcesVideoID, chapter.ChapterName, chapter.ChapterIndex, chapter.ChapterComplated);

                    chapterViewModelTmp.Lessions = lessions;

                    chapterViewModel.Add(chapterViewModelTmp);

                }

                Models.CourseVideo courseVideo1 = new Models.CourseVideo(chapterViewModel);

                courseVideo1.InitValue(courseVideo.CourseVideoID, courseVideo.CourcesVideoName, courseVideo.CourcesVideoLevel, courseVideo.CourcesVideoDuration, courseVideo.NumberOfLession, courseVideo.NumberOfStudent);


                var tmp1 = courseVideo1;

                ViewBag.CourseVideo = courseVideo1;
                
            }

            if (course != null)
            {
               
                return View(course);
            }

            return View();
        }

        public ActionResult CourseLearn(int courseID)
        {

            Course course = _courseService.GetCourseByID(courseID);

            if (course.CourseType.CourcesTypeID == 1)
            {

                Data_Oracle.Entities.CourseVideo courseVideo = _courseService.GetCourseVideoByID(courseID);

                List<Chapter> chapters = _courseService.GetChapterByCouresID((int)courseVideo.CourseVideoID);

                List<ChapterViewModel> chapterViewModel = new List<ChapterViewModel>();

                foreach (Chapter chapter in chapters)
                {

                    ChapterViewModel chapterViewModelTmp = new ChapterViewModel();

                    List<Lession> lessions = _courseService.GetLessionByChapterId((int)chapter.ChapterID);

                    chapterViewModelTmp.InitValue(chapter.ChapterID, chapter.CourcesVideoID, chapter.ChapterName, chapter.ChapterIndex, chapter.ChapterComplated);

                    chapterViewModelTmp.Lessions = lessions;

                    chapterViewModel.Add(chapterViewModelTmp);

                }

                Models.CourseVideo courseVideo1 = new Models.CourseVideo(chapterViewModel);

                courseVideo1.InitValue(courseVideo.CourseVideoID, courseVideo.CourcesVideoName, courseVideo.CourcesVideoLevel, courseVideo.CourcesVideoDuration, courseVideo.NumberOfLession, courseVideo.NumberOfStudent);


                var tmp1 = courseVideo1;

                ViewBag.CourseVideo = courseVideo1;
               

            }

            if (course != null)
            {

                
                return View(course);
            }

            return View();
        }

        [HttpGet]
        public ActionResult LoadQuizPartial(int ChapterID)
        {

            Quizzes quizze = _courseService.getQuizzByChapterID(ChapterID);

            ViewBag.Quizzes = quizze;
            int currentUserId = 23; // Thay bằng Session UserID thực tế
            var latestAttempt = _quizzService.GetLatestAttempt(currentUserId, (int)quizze.QuizzesID);
            ViewBag.LatestAttempt = latestAttempt;
            return PartialView("_QuizPartial");
        }

        public ActionResult LoadVideoPartial(int lessionID)
        {
            if (lessionID == 0)
            {
                return PartialView("_VideoPlayer", null);
            }

            var lesson = _courseService.GetLessionByLessionID(lessionID);

            if (lesson == null)
            {
                return HttpNotFound();
            }

            // 2. Trả về PartialView CHỈ chứa video
            return PartialView("_VideoPlayer", lesson);
        }

        [HttpGet]
        public ActionResult _Sidebar(int CourseId)
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult JoinFreeTrial(int courseId)
        {
            int userId = 23; // Lấy từ Session
            bool success = _courseRegistrationService.RegisterFreeTrial(userId, courseId);

            if (success)
            {
                return Json(new
                {
                    success = true,
                    redirectUrl = Url.Action("CourseLearn", "Course", new { courseID = courseId })
                });
            }
            else
            {
                return Json(new { success = false, message = "Lỗi: Bạn đã đăng ký rồi hoặc khóa học không cho dùng thử." });
            }
        }

        [HttpPost]
        public ActionResult BuyNow(int courseId)
        {
            int userId = 23;
            // Tạo đơn hàng Pending
            int orderId = _courseRegistrationService.CreateBuyOrder(userId, courseId);

            if (orderId > 0)
            {
                // Chuyển sang trang thanh toán
                return RedirectToAction("Payment", new { orderId = orderId });
            }
            return Content("Lỗi tạo đơn hàng");
        }

        public ActionResult Payment(int orderId)
        {
            // Lấy thông tin đơn hàng
            var order = _courseRegistrationService.GetOrder(orderId); // Bạn cần viết hàm này
            var course = _courseService.GetCourseByID((int)order.OrderDetails.First().CourseID);

            // --- TẠO LINK VIETQR ---
            // Cấu trúc: https://img.vietqr.io/image/[BankID]-[AccountNo]-[Template].png?amount=[Price]&addInfo=[Content]

            string bankId = "VCB";
            string accountNo = "1032811062";
            string template = "compact2";

            // Ép kiểu double sang decimal (vì PriceAtPurchase trong Entity là double)
            //decimal amount = (decimal)order.OrderDetails.First().PriceAtPurchase;
            decimal amount = 1;
            // [QUAN TRỌNG] Mã hóa nội dung để xử lý dấu cách (Space)
            string rawContent = $"HOCPHI {orderId}";
            string content = HttpUtility.UrlEncode(rawContent);

            string qrUrl = $"https://img.vietqr.io/image/{bankId}-{accountNo}-{template}.png?amount={amount}&addInfo={content}&accountName=HE THONG LMS";

            ViewBag.QrUrl = qrUrl;
            ViewBag.Order = order;
            ViewBag.Course = course;

            return View();
        }

        // ACTION 4: Xác nhận thanh toán (Giả lập)
        public ActionResult ConfirmPayment(int orderId)
        {
            _courseRegistrationService.ConfirmPaymentSuccess(orderId);
            var order = _courseRegistrationService.GetOrder(orderId);
            int courseId = (int)order.OrderDetails.First().CourseID;

            return RedirectToAction("CourseLearn", "Course", new { courseID = courseId });
        }

        [HttpGet]
        public ActionResult CheckOrderStatus(int orderId)
        {
            // Query trực tiếp DB để lấy trạng thái mới nhất (quan trọng: dùng AsNoTracking để tránh cache)
            // Giả sử _dbContext là biến context trong Controller của bạn
            var order = _oderService.GetOrderByOrderID(orderId); // Hoặc query trực tiếp: _db.Orders.Find(orderId);

            if (order != null && order.OrderStatusID == StatusConst.ORDER_SUCCESS)
            {
                // Nếu đã thành công -> Lấy CourseID để chuyển hướng
                int courseId = (int)order.OrderDetails.First().CourseID;

                return Json(new
                {
                    status = "SUCCESS",
                    redirectUrl = Url.Action("CourseLearn", "Course", new { courseID = courseId })
                }, JsonRequestBehavior.AllowGet);
            }

            // Nếu chưa thành công (Vẫn Pending)
            return Json(new { status = "PENDING" }, JsonRequestBehavior.AllowGet);
        }


    }
}