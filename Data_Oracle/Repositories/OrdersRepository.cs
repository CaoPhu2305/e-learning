using Data_Oracle.Context;
using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using e_learning.Helper;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly OracleDBContext _dbContext;

        public OrdersRepository(OracleDBContext oracleDBContext)
        {
            _dbContext = oracleDBContext;
        }

        public bool ConfirmPayment(int orderId, decimal amount)
        {
            // Bắt đầu Transaction tại tầng Repository
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // 1. Tìm Order
                    var order = _dbContext.Orders.Find(orderId);

                    // Kiểm tra null hoặc đã thanh toán rồi
                    if (order == null) return false;
                    if (order.OrderStatusID == StatusConst.ORDER_SUCCESS) return true;

                    // 2. Tìm OrderDetail để lấy giá và CourseID
                    var orderDetail = _dbContext.OrderDetails.FirstOrDefault(d => d.OderID == orderId);
                    if (orderDetail == null) return false;

                    // 3. Kiểm tra số tiền (Quan trọng)
                    if (amount < (decimal)orderDetail.PriceAtPurchase) return false;

                    // --- CẬP NHẬT DỮ LIỆU ---

                    // A. Cập nhật trạng thái Đơn hàng
                    order.OrderStatusID = StatusConst.ORDER_SUCCESS;

                    // B. Cập nhật trạng thái Ghi danh (Enrollment)
                    // Lưu ý: Repository này có thể truy cập bảng Enrollments thông qua _dbContext
                    var enrollment = _dbContext.Enrollments
                                               .FirstOrDefault(e => e.UserID == order.UserID
                                                                 && e.CourseID == orderDetail.CourseID);

                    if (enrollment != null)
                    {
                        enrollment.EnrollmentStatusID = StatusConst.ENROLL_ACTIVE;
                    }

                    // 4. Lưu và Commit
                    _dbContext.SaveChanges();
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Log lỗi: System.Diagnostics.Debug.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public int CreateBuyOrder(int userId, int courseId)
        {
            // Sử dụng Transaction
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var course = _dbContext.Courses.Find(courseId);
                    if (course == null) throw new Exception("Khóa học không tồn tại");

                    // --- BƯỚC 1: TẠO ORDER ---
                    var order = new Order
                    {
                        UserID = userId,
                        OrderStatusID = StatusConst.ORDER_PENDING
                    };
                    _dbContext.Orders.Add(order);
                    _dbContext.SaveChanges(); // Lưu lần 1 để lấy OrderID

                    // --- BƯỚC 2: TẠO ORDER DETAIL ---
                    // Sửa lỗi 1: Dùng Object Initializer cho an toàn, không dùng Constructor nữa
                    var orderDetail = new OrderDetail
                    {
                        OderID = order.OrderID,
                        CourseID = courseId,
                        PriceAtPurchase = (double)course.Price,
                        PromotionID = null // Sửa lỗi 2: Truyền null thay vì 0
                    };
                    _dbContext.OrderDetails.Add(orderDetail);

                    // --- BƯỚC 3: TẠO HOẶC UPDATE ENROLLMENT ---
                    var existingEnrollment = _dbContext.Enrollments
                        .FirstOrDefault(e => e.UserID == userId && e.CourseID == courseId);

                    if (existingEnrollment != null)
                    {
                        // Nếu đã có -> Update trạng thái
                        existingEnrollment.EnrollmentStatusID = StatusConst.ENROLL_PENDING;
                    }
                    else
                    {
                        // Nếu chưa có -> Tạo mới
                        // Sửa lỗi 3: Dùng Object Initializer để tránh nhầm lẫn thứ tự tham số
                        var newEnrollment = new Enrollments
                        {
                            EnrollmentsName = "Paid - " + course.CourseName,
                            UserID = userId,           // Gán đúng UserID
                            CourseID = courseId,       // Gán đúng CourseID
                            EnrollmentStatusID = StatusConst.ENROLL_PENDING, // Gán đúng Status
                            EnrollmentsDate = DateTime.Now
                        };
                        _dbContext.Enrollments.Add(newEnrollment);
                    }

                    // Lưu lần cuối cho tất cả thay đổi (Detail + Enrollment)
                    _dbContext.SaveChanges();

                    transaction.Commit();
                    return (int)order.OrderID;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    // Debug lỗi chi tiết
                    var msg = ex.Message;
                    if (ex.InnerException != null)
                    {
                        msg += "\nInner: " + ex.InnerException.Message;
                        if (ex.InnerException.InnerException != null)
                        {
                            msg += "\nInner 2: " + ex.InnerException.InnerException.Message;
                        }
                    }
                    System.Diagnostics.Debug.WriteLine(msg); // Xem lỗi ở cửa sổ Output

                    return 0;
                }
            }
        }

        public Order GetOrderByID(int orderID)
        {
            return _dbContext.Orders.FirstOrDefault(x => x.OrderID == orderID);
        }
    }
}
