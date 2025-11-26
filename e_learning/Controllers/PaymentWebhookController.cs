using e_learning.Helper;
using e_learning.Models;
using Services.Implamentatios;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_learning.Controllers
{
    public class PaymentWebhookController : Controller
    {
        // GET: PaymentWebhook

        private readonly IPaymentWebhookService _courseRegistrationService;

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ReceivePayment(WebhookDataModel data)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào cơ bản
                if (data == null || string.IsNullOrEmpty(data.Content))
                {
                    return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
                }

                // Gọi Service để xử lý logic nghiệp vụ
                bool isSuccess = _courseRegistrationService.ProcessPaymentWebhook(data.Content, data.Amount);

                if (isSuccess)
                {
                    return Json(new { success = true, message = "Kích hoạt thành công" });
                }
                else
                {
                    // Có thể là do sai tiền, sai nội dung, hoặc lỗi DB
                    return Json(new { success = false, message = "Không tìm thấy đơn hoặc sai tiền" });
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi ex tại đây nếu cần
                return new HttpStatusCodeResult(500);
            }
        }

        private int ExtractOrderId(string content)
        {
            // Dùng Regex để lấy số
            var match = System.Text.RegularExpressions.Regex.Match(content, @"\d+");
            return match.Success ? int.Parse(match.Value) : 0;
        }

    }
}