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
                                              .Select(e => e.CourseID);

            var courses = _dbContext.Courses
                                    .Where(c => !enrolledCourseIds.Contains(c.CourseID)
                                             && c.CourseStatusID == StatusConst.COURSE_APPROVED) // <--- THÊM ĐIỀU KIỆN NÀY
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

        public Course GetLatestCourseByName(string courseName)
        {
            return _dbContext.Courses
                .Where(c => c.CourseName == courseName)
                .OrderByDescending(c => c.CreatedDate) // Lấy cái mới nhất vừa tạo
                .FirstOrDefault();
        }

        public void DeleteCourse(int courseId)
        {
            var course = _dbContext.Courses.Find(courseId);
            if (course != null)
            {
                // 1. Xóa LecturerCourse (Bảng trung gian)
                var lecturerCourses = _dbContext.lecturerCourses.Where(lc => lc.CourseID == courseId).ToList();
                _dbContext.lecturerCourses.RemoveRange(lecturerCourses);

                // 2. Xóa Video và các Chapter/Lesson liên quan
                // Nếu DB chưa set Cascade Delete, bạn phải xóa bằng tay từng cấp:
                // Lesson -> Chapter -> CourseVideo

                // Giả sử bạn xóa CourseVideo, EF sẽ tự xóa con nếu config đúng.
                // Nếu không, phải query từng cái ra xóa.
                var video = _dbContext.CoursesVideos.FirstOrDefault(v => v.CourseVideoID == courseId);
                if (video != null)
                {
                    // Xóa Chapter -> Lesson
                    var chapters = _dbContext.Chapters.Where(c => c.CourcesVideoID == video.CourseVideoID).ToList();
                    foreach (var chap in chapters)
                    {
                        var lessons = _dbContext.Lessions.Where(l => l.ChapterID == chap.ChapterID).ToList();
                        _dbContext.Lessions.RemoveRange(lessons);
                    }
                    _dbContext.Chapters.RemoveRange(chapters);
                    _dbContext.CoursesVideos.Remove(video);
                }

                // 3. Xóa Course
                _dbContext.Courses.Remove(course);

                _dbContext.SaveChanges();
            }
        }

        public bool CheckCourseOwner(decimal userId, decimal courseId)
        {
            // Kiểm tra trong bảng LECTURE_COURSE xem có cặp (UserID, CourseID) này không
            return _dbContext.lecturerCourses.Any(lc => lc.UserID == userId && lc.CourseID == courseId);
        }

        public int CountTotalStudentsByLecturer(int lecturerId)
        {
            // Logic: 
            // Bảng LECTURE_COURSE (lấy các khóa của GV này)
            // JOIN bảng ENROLLMENTS (lấy các lượt đăng ký)
            // Đếm số lượng USER_ID khác nhau (Distinct)

            var count = (from lc in _dbContext.lecturerCourses
                         join e in _dbContext.Enrollments on lc.CourseID equals e.CourseID
                         where lc.UserID == lecturerId
                         // Chỉ đếm những người đã mua hoặc đang học thử (tùy bạn chọn)
                         // && (e.EnrollmentStatusID == StatusConst.ENROLL_ACTIVE || e.EnrollmentStatusID == StatusConst.ENROLL_TRIAL)
                         select e.UserID)
                         .Distinct() // Quan trọng: 1 người học 3 khóa thì chỉ tính là 1 học viên
                         .Count();

            return count;
        }

        public decimal GetNextChapterId()
        {
            return _dbContext.Database.SqlQuery<decimal>("SELECT SEQ_CHAPTER.NEXTVAL FROM DUAL").FirstOrDefault();
        }

    }
}
