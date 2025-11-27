using Data_Oracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICourseRegistrationService
    {


        // Logic: Chỉ tạo ENROLLMENTS, không tạo ORDER.
        bool RegisterFreeTrial(int userId, int courseId);

        // Logic: Tạo ORDER -> ORDER_DETAIL -> ENROLLMENT (Transaction)
        int CreateBuyOrder(int userId, int courseId);

        // Hàm này gọi khi API VietQR/MoMo báo thành công
        void ConfirmPaymentSuccess(int orderId);

        Order GetOrder(int orderID);


        Enrollments GetEnrollment(int userId, int courseId);
    }
}
