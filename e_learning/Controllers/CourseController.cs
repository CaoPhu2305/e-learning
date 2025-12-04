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
        private IPaymentWebhookService _paymentWebhookService;

        public CourseController(ICourseService courseService,
            IQuizzService quizzService,
            ICourseRegistrationService courseRegistrationService,
            IOderService oderService,
            IPaymentWebhookService paymentWebhookService)
            
        {
            _courseService = courseService;
            _quizzService = quizzService;
            _courseRegistrationService = courseRegistrationService;
            _oderService = oderService;
            _paymentWebhookService = paymentWebhookService;
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
            // 1. Kiểm tra đăng nhập (Thêm cái này cho an toàn, tránh lỗi null session)
            if (Session["UserID"] == null) return RedirectToAction("Login", "Account");
            int userID = Convert.ToInt32(Session["UserID"]);

            // --- [LOGIC CŨ GIỮ NGUYÊN + BỔ SUNG LOGIC MỚI] ---

            // A. Kiểm tra xem đã mua chưa (Logic cũ)
            var enrollment = _courseRegistrationService.GetEnrollment(userID, courseID);
            bool isEnrolled = (enrollment != null && enrollment.EnrollmentStatusID == e_learning.Helper.StatusConst.ENROLL_ACTIVE);

            // B. Kiểm tra xem có phải chủ sở hữu (Giảng viên) không (Logic mới)
            // Bạn cần thêm hàm này vào Service (xem hướng dẫn phía dưới)
            bool isOwner = _courseService.IsCourseOwner(userID, courseID);

            // C. Quyết định cuối cùng: Đã mua HOẶC Là chủ sở hữu
            bool isPurchased = isEnrolled || isOwner;

            // Truyền biến này sang View
            ViewBag.IsPurchased = isPurchased;

            // --- [KẾT THÚC PHẦN SỬA ĐỔI] ---

            Course course = _courseService.GetCourseByID(courseID);

            if (course != null && course.CourseType.CourcesTypeID == 1)
            {
                Data_Oracle.Entities.CourseVideo courseVideo = _courseService.GetCourseVideoByID(courseID);

                // Kiểm tra null để tránh lỗi crash nếu dữ liệu video chưa có
                if (courseVideo != null)
                {
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

                    ViewBag.CourseVideo = courseVideo1;
                }
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
            // 1. Lấy Quiz từ DB
            Quizzes quizze = _courseService.getQuizzByChapterID(ChapterID);

            // [QUAN TRỌNG] Kiểm tra nếu chưa có Quiz thì báo lỗi nhẹ nhàng hoặc return view trống
            if (quizze == null)
            {
                // Trả về thông báo HTML đơn giản để hiện vào khung
                return Content(@"
            <div class='alert alert-warning text-center m-4'>
                <i class='bi bi-exclamation-triangle'></i> 
                Chương này chưa có bài tập trắc nghiệm.
            </div>");
            }

            ViewBag.Quizzes = quizze;

            // 2. Lấy UserID an toàn (Tránh lỗi nếu Session hết hạn)
            if (Session["UserID"] != null)
            {
                int currentUserId = Convert.ToInt32(Session["UserID"]);

                // Chỉ lấy attempt nếu đã có quizze (đã check null ở trên nên an toàn)
                var latestAttempt = _quizzService.GetLatestAttempt(currentUserId, (int)quizze.QuizzesID);
                ViewBag.LatestAttempt = latestAttempt;
            }
            else
            {
                ViewBag.LatestAttempt = null;
            }

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
            // Lấy UserID từ Session (Logic giao diện)
            if (Session["UserID"] == null)
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập!" });
            }

            int userId = Convert.ToInt32(Session["UserID"]);

            // Gọi Service xử lý
            bool isSuccess = _courseRegistrationService.RegisterFreeTrial(userId, courseId);

            if (isSuccess)
            {
                return Json(new
                {
                    success = true,
                    redirectUrl = Url.Action("CourseLearn", "Course", new { courseID = courseId })
                });
            }
            else
            {
                return Json(new { success = false, message = "Khóa học này không hỗ trợ học thử hoặc lỗi hệ thống." });
            }
        }

       

        [HttpPost]
        public ActionResult BuyNow(int courseId)
        {
            int userId = Convert.ToInt32(Session["UserID"]);
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
            decimal amount = (decimal)order.OrderDetails.First().PriceAtPurchase;
            //decimal amount = 1;
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
            // 1. Lấy thông tin Order
            var order = _oderService.GetOrderByOrderID(orderId);

            // 2. Nếu thấy Order đã thành công
            if (order != null && order.OrderStatusID == StatusConst.ORDER_SUCCESS)
            {
                // --- LOGIC TỰ SỬA LỖI (AUTO-FIX) ---
                var detail = order.OrderDetails.First();

                // Gọi Service kiểm tra và kích hoạt Enrollment ngay lập tức
                // (Bạn cần thêm hàm này vào Service hoặc gọi Repo tương ứng)
               
                // -----------------------------------

                return Json(new
                {
                    status = "SUCCESS",
                    redirectUrl = Url.Action("CourseLearn", "Course", new { courseID = detail.CourseID })
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "PENDING" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SimulatePaymentSuccess(int orderId)
        {
            // 1. Lấy thông tin đơn hàng để biết số tiền
            var order = _oderService.GetOrderByOrderID(orderId); // Hoặc _ordersRepository.GetById(orderId)
            if (order == null) return Json(new { success = false });

            decimal amount = (decimal)order.OrderDetails.First().PriceAtPurchase;
            string content = $"HOCPHI {orderId}"; // Giả lập nội dung chuyển khoản chuẩn

            // 2. Gọi lại đúng cái Service xử lý Webhook bạn đã viết
            // Lưu ý: Controller này phải có _courseRegistrationService được inject vào
            bool result = _paymentWebhookService.ProcessPaymentWebhook(content, amount);

            return Json(new { success = result });
        }



        public ActionResult MyCourses()
        {
            int userId = 23; // Lấy từ Session

            // 1. Gọi Service lấy dữ liệu thô (Entity)
            var rawData = _courseService.GetEnrollmentsByUserId(userId);

            // 2. Mapping từ Entity sang ViewModel (Logic hiển thị nằm ở đây)
            var viewModel = rawData.Select(e => new MyCourseViewModel
            {
                CourseID = e.Course.CourseID,     // Lấy từ bảng Course được Include
                CourseName = e.Course.CourseName,
                Image = e.Course.Image,
                EnrollmentDate = e.EnrollmentsDate,

                // Gán StatusID
                StatusID = e.EnrollmentStatusID,

                // Logic hiển thị tên trạng thái
                StatusName = StatusConst.GetStatusName(e.EnrollmentStatusID),

                IsTrial = e.EnrollmentStatusID == StatusConst.ENROLL_TRIAL,
                ProgressPercent = 0
            }).ToList();

            return View(viewModel);
        }
    }
}