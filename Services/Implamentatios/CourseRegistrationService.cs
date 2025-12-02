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

        public Enrollments GetEnrollment(int userId, int courseId)
        {
            return _enrollmentsRepository.GetEnrollmentByUserAndCourse((decimal)userId, (decimal)courseId);
        }

        public Order GetOrder(int orderID)
        {
            return _ordersRepository.GetOrderByID((int)orderID);
        }

        public bool IsRegisterFreeTrial(int userId, int courseId)
        {
            throw new NotImplementedException();
        }

        public bool RegisterFreeTrial(int userId, int courseId)
        {
            try
            {
                // 1. Kiểm tra khóa học (Dùng CourseRepository)
                var course = _courseRepository.GetCourseByID(courseId);

                // Logic kiểm tra điều kiện nghiệp vụ
                if (course == null || course.IsTrialAvailable == false)
                {
                    return false;
                }

                // 2. Kiểm tra đã đăng ký chưa (Dùng EnrollmentsRepository)
                var existing = _enrollmentsRepository.GetEnrollmentByUserAndCourse(userId, courseId);
                if (existing != null)
                {
                    return true; // Đã có rồi thì coi như thành công
                }

                // 3. Tạo đối tượng Enrollment mới
                var enrollment = new Enrollments
                {
                    EnrollmentsName = "Trial - " + course.CourseName,
                    UserID = (decimal)userId,
                    CourseID = (decimal)courseId,
                    EnrollmentsDate = DateTime.Now,

                    // Logic nghiệp vụ: Gán trạng thái Trial
                    EnrollmentStatusID = StatusConst.ENROLL_TRIAL
                };

                // 4. Lưu xuống DB thông qua Repository
                _enrollmentsRepository.Add(enrollment);
                _enrollmentsRepository.SaveChange();

                return true;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi tại đây nếu cần (VD: _logger.LogError(ex))
                return false;
            }
        }
    }
}
