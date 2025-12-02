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



        // Data_Oracle/Repositories/CourseRepository.cs

        public void AddCourse(Course course)
        {
            // Nếu ID dùng Identity thì bỏ dòng này, nếu dùng None thì phải lấy Sequence
            // course.CourseID = GetNextSequence("SEQ_COURSE"); 
            _dbContext.Courses.Add(course);
        }

        public void AddCourseVideo(CourseVideo video)
        {
            _dbContext.CoursesVideos.Add(video);
        }

        public void AddLecturerCourse(LecturerCourse lecturerCourse)
        {
            _dbContext.lecturerCourses.Add(lecturerCourse);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        // Hàm lấy khóa học của giảng viên (Join bảng LecturerCourse)
        public List<Course> GetCoursesByLecturer(int lecturerId)
        {
            // Logic: Tìm trong bảng LECTURE_COURSE có UserID = lecturerId -> Lấy list CourseID -> Lấy Course
            return _dbContext.lecturerCourses
                             .Where(lc => lc.UserID == lecturerId)
                             .Select(lc => lc.Course)
                             .OrderByDescending(c => c.CourseID) // Mới nhất lên đầu
                             .ToList();
        }

        public decimal GetCourseVideoIdByCourseId(decimal courseId)
        {
            var cv = _dbContext.CoursesVideos.FirstOrDefault(x => x.CourseVideoID == courseId);
            // Nếu chưa có thì tạo mới (Logic fallback)
            return cv != null ? cv.CourseVideoID : 0;
        }

        // 2. Thêm Chapter
        public void AddChapter(Chapter chapter)
        {
            // Tự tính Index (Thứ tự chương)
            int count = _dbContext.Chapters.Count(c => c.CourcesVideoID == chapter.CourcesVideoID);
            chapter.ChapterIndex = count + 1;

            // ID tự tăng theo Trigger DB, nên gán = 0 (hoặc None nếu đã config)
            _dbContext.Chapters.Add(chapter);
        }

        // 3. Thêm Lesson
        public void AddLesson(Lession lesson)
        {
            _dbContext.Lessions.Add(lesson);
        }


    }
}
