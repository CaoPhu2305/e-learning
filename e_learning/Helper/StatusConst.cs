using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_learning.Helper
{
    public class StatusConst
    {
        public const int COURSE_PENDING = 1;   // Chờ duyệt (Under Review)
        public const int COURSE_APPROVED = 2;  // Đã duyệt (Approved)
        public const int COURSE_DENIED = 3;

        public const int ENROLL_TRIAL = 1;    // Đang dùng thử
        public const int ENROLL_PENDING = 2;  // Chờ thanh toán
        public const int ENROLL_ACTIVE = 3;   // Đã kích hoạt (Học chính thức)

        // ORDER_STATUS_ID
        public const int ORDER_PENDING = 1;   // Chờ thanh toán
        public const int ORDER_SUCCESS = 2;   // Thành công
        public const int ORDER_FAILED = 3;    // Thất bại

        public static string GetStatusName(decimal statusId)
        {
            if (statusId == StatusConst.ENROLL_TRIAL) return "Dùng thử";
            if (statusId == StatusConst.ENROLL_PENDING) return "Chờ thanh toán";
            if (statusId == StatusConst.ENROLL_ACTIVE) return "Đã sở hữu";
            return "";
        }

    }
}