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
    public class CourseRepository : ICourseRepository
    {

        private readonly OracleDBContext _dbContext;

        public CourseRepository(OracleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        

        public List<Course> GetAllCourse()
        {

            try
            {

                var b = _dbContext.Courses.ToList();

                return b;
            }
            catch (Exception ex) { 
            

                var a = ex.Message;

            }

            return null;
          
        }

        public List<CourseType> GetAllCourseType()
        {
           return _dbContext.CourseTypes.ToList();
        }

        public Course GetCourseByID(int id)
        {
            return _dbContext.Courses.FirstOrDefault(x => x.CourseID == id);
        }

        public List<Course> GetCoursesNotEnrolled(int userId)
        {
            var enrolledCourseIds = _dbContext.Enrollments
                                      .Where(e => e.UserID == userId)
                                      .Select(e => e.CourseID); // Chỉ lấy ID

            // 2. Lấy các khóa học KHÔNG nằm trong danh sách trên
            // SQL sinh ra sẽ dạng: SELECT * FROM Courses WHERE CourseID NOT IN (...)
            var courses = _dbContext.Courses
                                    .Where(c => !enrolledCourseIds.Contains(c.CourseID))
                                    .ToList();

            return courses;
        }

        public CourseVideo GetCourseVideoById(int id)
        {
            return _dbContext.CoursesVideos.FirstOrDefault(x => x.CourseVideoID == id);
        }
    }
}
