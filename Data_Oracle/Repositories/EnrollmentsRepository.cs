using Data_Oracle.Context;
using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using e_learning.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Repositories
{
    public class EnrollmentsRepository : IEnrollmentsRepository
    {

        private readonly OracleDBContext _dbContext;

        public EnrollmentsRepository(OracleDBContext oracleDBContext)
        {
            _dbContext = oracleDBContext;
        }

        public Enrollments GetEnrollmentsByID(int ID)
        {
            return _dbContext.Enrollments.FirstOrDefault( x => x.EnrollmentsID == ID);
        }

        public Enrollments GetEnrollmentsByUserID(int Id)
        {
            return _dbContext.Enrollments.FirstOrDefault(x => x.UserID == Id);
        }

        public Enrollments GetEnrollmentsByUserIdAndOderId(int userID, int courseID)
        {
            return _dbContext.Enrollments.FirstOrDefault(x => x.UserID == userID && x.CourseID == courseID);
        }

        public void Save(Enrollments enrollments)
        {

            try
            {

                _dbContext.Enrollments.Add(enrollments);
                _dbContext.SaveChanges();

            }
            catch (Exception ex) { 

                var tmp = ex.Message;

            }

        }

        public List<Enrollments> GetEnrollmentsByUserId(int userId)
        {

            // ID của các trạng thái cần lấy (Trial, Pending, Active)
            // Nếu StatusConst nằm ở Web, bạn không gọi được ở đây -> Dùng số cứng hoặc chuyển StatusConst sang project Data
            decimal[] validStatuses = { 1, 2, 3 };

            return _dbContext.Enrollments
                             .Include("Course") // QUAN TRỌNG: Load kèm bảng Course
                             .Where(e => e.UserID == userId && validStatuses.Contains(e.EnrollmentStatusID))
                             .OrderByDescending(e => e.EnrollmentsDate)
                             .ToList();

        }

        public void SaveChange()
        {
            _dbContext.SaveChanges();
        }

        public Enrollments GetEnrollmentByUserAndCourse(decimal userId, decimal courseId)
        {
            return _dbContext.Enrollments
                             .Where(e => e.UserID == userId && e.CourseID == courseId)
                             .OrderByDescending(e => e.EnrollmentsDate)
                             .FirstOrDefault();
        }

        public void Add(Enrollments enrollment)
        {
            _dbContext.Enrollments.Add(enrollment);
        }
    }
}
