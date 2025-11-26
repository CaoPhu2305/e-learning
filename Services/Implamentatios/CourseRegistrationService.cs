using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using e_learning.Helper;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Implamentatios
{
    public class CourseRegistrationService : ICourseRegistrationService
    {

        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEnrollmentsRepository _enrollmentsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;

        public CourseRegistrationService(ICourseRepository courseRepository,
            IUserRepository userRepository,
            IEnrollmentsRepository enrollmentsRepository,
            IOrdersRepository ordersRepository,
            IOrderDetailsRepository orderDetailsRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _ordersRepository = ordersRepository;
            _enrollmentsRepository = enrollmentsRepository;
        }

        public void ConfirmPaymentSuccess(int orderId)
        {

            var order = _ordersRepository.GetOrderByID(orderId);

            if (order != null && order.OrderStatusID == StatusConst.ORDER_PENDING)
            {
                order.OrderStatusID = StatusConst.ORDER_SUCCESS;
            }

            var orderDetail = _orderDetailsRepository.GetOrderDetailByOderId(orderId);

            if (orderDetail != null)
            {
                var enrollment = _enrollmentsRepository.GetEnrollmentsByUserIdAndOderId((int)order.UserID,(int) orderDetail.CourseID);

                if (enrollment != null)
                {
                    enrollment.EnrollmentStatusID = StatusConst.ENROLL_ACTIVE;
                }

                _enrollmentsRepository.SaveChange();

            }

            
        }

        public int CreateBuyOrder(int userId, int courseId)
        {
            return _ordersRepository.CreateBuyOrder(userId, courseId);
        }

        public Order GetOrder(int orderID)
        {
            return _ordersRepository.GetOrderByID((int)orderID);
        }

        public bool RegisterFreeTrial(int userId, int courseId)
        {

            var course = _courseRepository.GetCourseByID(userId);

            if (course == null || course.IsTrialAvailable ==  false)
            {
                return false;
            }


            var enrollment_test = _enrollmentsRepository.GetEnrollmentsByUserID(userId);

            if (enrollment_test != null) return true;

            var enrollment = new Enrollments
            {
                EnrollmentsName = "Trial - " + course.CourseName,
                UserID = userId,
                CourseID = courseId,
                EnrollmentsDate = DateTime.Now,
                EnrollmentStatusID = StatusConst.ENROLL_TRIAL // Trạng thái Dùng thử
            };

            _enrollmentsRepository.Save(enrollment);

            return true;
            
        }
    }
}
