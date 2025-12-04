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
            // 1. Đếm số chương hiện tại của khóa học này
            int count = _dbContext.Chapters
                .Count(c => c.CourcesVideoID == chapter.CourcesVideoID);

            // 2. Gán Index = Số lượng cũ + 1
            // Ví dụ: Đang có 3 chương -> Chương mới là 4
            chapter.ChapterIndex = count + 1;

            // 3. Gán ID (Thủ công như bạn đã làm)
            chapter.ChapterID = GetNextChapterId();

            _dbContext.Chapters.Add(chapter);
            // Lưu ý: Không SaveChanges ở đây nếu Service gọi SaveChanges sau
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
            if (course == null) return;

            // 1. Xóa các liên kết Giảng viên - Khóa học (Bảng LECTURE_COURSE)
            var lecturerCourses = _dbContext.lecturerCourses.Where(lc => lc.CourseID == courseId).ToList();
            _dbContext.lecturerCourses.RemoveRange(lecturerCourses);

            // 2. Xóa Enrollments (Nếu có học viên đã mua - Cẩn thận: Thường thực tế sẽ không cho xóa nếu đã có người mua)
            var enrollments = _dbContext.Enrollments.Where(e => e.CourseID == courseId).ToList();
            _dbContext.Enrollments.RemoveRange(enrollments);

            // 3. Xóa Đơn hàng chi tiết (OrderDetails) - Để tránh lỗi FK
            var orderDetails = _dbContext.OrderDetails.Where(od => od.CourseID == courseId).ToList();
            _dbContext.OrderDetails.RemoveRange(orderDetails);

            // 4. XỬ LÝ NỘI DUNG KHÓA HỌC (Video -> Chapter -> Lesson/Quiz)
            var video = _dbContext.CoursesVideos.FirstOrDefault(v => v.CourseVideoID == courseId);
            if (video != null)
            {
                // Lấy tất cả Chapter của khóa này
                var chapters = _dbContext.Chapters.Where(c => c.CourcesVideoID == video.CourseVideoID).ToList();

                foreach (var chap in chapters)
                {
                    // 4.1. Xóa Bài học (Lessons)
                    var lessons = _dbContext.Lessions.Where(l => l.ChapterID == chap.ChapterID).ToList();
                    _dbContext.Lessions.RemoveRange(lessons);

                    // 4.2. XÓA QUIZ (Đây là phần bạn đang thiếu)
                    var quizzes = _dbContext.Quizzes.Where(q => q.ChapterID == chap.ChapterID).ToList();
                    foreach (var q in quizzes)
                    {
                        // 4.2.1 Xóa Quiz Attempt (Lịch sử thi của SV)
                        var attempts = _dbContext.QuizAttempts.Where(qa => qa.QuizzesID == q.QuizzesID).ToList();
                        foreach (var att in attempts)
                        {
                            // Xóa chi tiết câu trả lời của SV
                            var userAnswers = _dbContext.UserAnswers.Where(ua => ua.QuizAttemptID == att.QuizAttemptID).ToList();
                            _dbContext.UserAnswers.RemoveRange(userAnswers);
                        }
                        _dbContext.QuizAttempts.RemoveRange(attempts);

                        // 4.2.2 Xóa Câu hỏi & Đáp án
                        var questions = _dbContext.Questions.Where(ques => ques.QuizzesID == q.QuizzesID).ToList();
                        foreach (var ques in questions)
                        {
                            var answers = _dbContext.AnswerOptions.Where(a => a.QuestionsID == ques.QuestionsID).ToList();
                            _dbContext.AnswerOptions.RemoveRange(answers);
                        }
                        _dbContext.Questions.RemoveRange(questions);

                        // Xóa Quiz
                        _dbContext.Quizzes.Remove(q);
                    }
                }

                // Xóa danh sách Chapter
                _dbContext.Chapters.RemoveRange(chapters);

                // Xóa Course Video
                _dbContext.CoursesVideos.Remove(video);
            }

            // 5. Cuối cùng: Xóa Course
            _dbContext.Courses.Remove(course);

            // LƯU TẤT CẢ THAY ĐỔI
            _dbContext.SaveChanges();
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
