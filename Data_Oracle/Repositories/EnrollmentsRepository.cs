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

            try { 

            // Dùng Include để Eager Load bảng Course
                var tmp =  _dbContext.Enrollments
                               .Include("Course") // Hoặc .Include(x => x.Course) nếu có using System.Data.Entity;
                               .Where(e => e.UserID == userId
                                        && (e.EnrollmentStatusID == StatusConst.ENROLL_ACTIVE
                                         || e.EnrollmentStatusID == StatusConst.ENROLL_TRIAL))
                               .OrderByDescending(e => e.EnrollmentsDate)
                               .ToList();


                return tmp;
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }

            return null;

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
    }
}
