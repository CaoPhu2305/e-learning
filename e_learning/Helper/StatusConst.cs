using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_learning.Helper
{
    public class StatusConst
    {

        public const int ENROLL_TRIAL = 1;    // Đang dùng thử
        public const int ENROLL_PENDING = 2;  // Chờ thanh toán
        public const int ENROLL_ACTIVE = 3;   // Đã kích hoạt (Học chính thức)

        // ORDER_STATUS_ID
        public const int ORDER_PENDING = 1;   // Chờ thanh toán
        public const int ORDER_SUCCESS = 2;   // Thành công
        public const int ORDER_FAILED = 3;    // Thất bại

    }
}