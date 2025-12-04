using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using Data_Oracle.Repositories;
using e_learning.Helper;
using Services.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implamentatios
{
    public class CourseService : ICourseService
    {

        private readonly ICourseRepository _courseRepository;
        private readonly ILessionRepository _lessionRepository;
        private readonly IChapterRepository _chapterRepository;
        private readonly IQuizzRepository _quizzRepository;
        private readonly IEnrollmentsRepository _enrollmentsRepository;

        public CourseService(ICourseRepository courseRepository,
            IChapterRepository chapterRepository,
            ILessionRepository lessionRepository,
            IQuizzRepository quizzRepository,
            IEnrollmentsRepository enrollmentsRepository)
        {
            _courseRepository = courseRepository;
            _chapterRepository = chapterRepository;
            _lessionRepository = lessionRepository;
            this._quizzRepository = quizzRepository;
            _enrollmentsRepository = enrollmentsRepository;
        }

        public List<Enrollments> GetEnrollmentsByUserId(int userId)
        {
            return _enrollmentsRepository.GetEnrollmentsByUserId(userId);
        }

        public List<Chapter> GetChapterByCouresID(int CourseId)
        {
            return _chapterRepository.GetChapterByCouresID(CourseId);
        }

        public Course GetCourseByID(int id)
        {
            return _courseRepository.GetCourseByID(id);
        }

        public List<Course> GetCourses()
        {

            return _courseRepository.GetAllCourse();

        }

        public List<CourseType> GetCoursesType()
        {
            return _courseRepository.GetAllCourseType();
        }

        public CourseVideo GetCourseVideoByID(int id)
        {
            return _courseRepository.GetCourseVideoById(id);
        }

        public List<Lession> GetLessionByChapterId(int ChapterId)
        {
            return _lessionRepository.GetAllLessionByChapterId(ChapterId);
        }

        public Lession GetLessionByLessionID(int LessionID)
        {
            return _lessionRepository.GetLessionByLessionID(LessionID);
        }

        public Quizzes getQuizzByChapterID(int ChapterID)
        {
            var x = _quizzRepository.GetQuizzesByChapterID1(ChapterID);

            return x;

        }

        public Enrollments GetEnrollments(int userID, int courseID)
        {
            throw new NotImplementedException();
        }

        public List<Course> GetUnpurchasedCourses(int userId)
        {
            return _courseRepository.GetCoursesNotEnrolled(userId);
        }



        public int CreateFullCourse(CourseDto dto, int lecturerId, string imagePath)
        {
            // Không dùng Transaction hoặc dùng cũng được, nhưng cẩn thận logic query
            // Ở đây tôi viết theo kiểu không Transaction cho đơn giản, 
            // vì Query trong cùng 1 transaction chưa commit đôi khi không thấy dữ liệu nếu cấu hình Isolation Level cao.

            try
            {
                // --- BƯỚC 1: TẠO VÀ LƯU COURSE ---
                var course = new Course
                {
                    // ... các trường cũ ...
                    CourseName = dto.CourseName,
                    Description = dto.Description,
                    Price = (double)dto.Price,
                    IsTrialAvailable = dto.IsTrialAvailable,
                    Image = imagePath,
                    Certificate = "Yes",
                    CourseTypeID = 1,
                    CourseStatusID = StatusConst.COURSE_PENDING,

                    // QUAN TRỌNG: Gán ngày giờ hiện tại
                    CreatedDate = DateTime.Now
                };

                _courseRepository.AddCourse(course);

                // Lưu để Trigger sinh ID và ghi xuống ổ cứng
                _courseRepository.SaveChanges();

                // --- BƯỚC 2: LẤY LẠI ID VỪA TẠO (SELECT BACK) ---
                // Lúc này course.CourseID vẫn là 0 do EF không lấy được.
                // Ta phải tìm lại nó từ DB.
                var savedCourse = _courseRepository.GetLatestCourseByName(dto.CourseName);

                if (savedCourse == null) throw new Exception("Lỗi: Không tìm thấy khóa học vừa tạo.");

                decimal realCourseId = savedCourse.CourseID; // Đây là ID thật (VD: 105)

                // --- BƯỚC 3: TẠO VIDEO (Dùng ID thật) ---
                var video = new CourseVideo
                {
                    CourseVideoID = realCourseId,
                    CourcesVideoName = course.CourseName,
                    CourcesVideoLevel = "Basic",
                    CourcesVideoDuration = "0h",
                    NumberOfLession = 0,
                    NumberOfStudent = 0
                };
                _courseRepository.AddCourseVideo(video);

                // --- BƯỚC 4: GÁN GIẢNG VIÊN (Dùng ID thật) ---
                var lecturerCourse = new LecturerCourse
                {
                    UserID = lecturerId,
                    CourseID = realCourseId,
                    CreateAtTime = DateTime.Now
                };

                _courseRepository.AddLecturerCourse(lecturerCourse);

                // --- BƯỚC 5: LƯU CÁC BẢNG CON ---
                _courseRepository.SaveChanges();

                return (int)realCourseId;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi
                throw ex;
            }
        }

        public List<Course> GetLecturerCourses(int lecturerId)
        {
            return _courseRepository.GetCoursesByLecturer(lecturerId);
        }

        public void AddChapter(ChapterDto dto)
        {
            // 1. Lấy ID Chapter trước (Chủ động)
            decimal newChapterId = _courseRepository.GetNextChapterId();

            decimal videoId = _courseRepository.GetCourseVideoIdByCourseId(dto.CourseId);

            var chapter = new Chapter
            {
                ChapterID = newChapterId, // GÁN CỨNG ID VỪA LẤY
                CourcesVideoID = videoId,
                ChapterName = dto.ChapterName,
                ChapterComplated = dto.ChapterCompleted,
                // ChapterIndex logic cũ...
            };

            _courseRepository.AddChapter(chapter);
            _courseRepository.SaveChanges();

            // 2. Tạo Quiz đi kèm (Bây giờ newChapterId chắc chắn đúng)
            decimal quizId = _quizzRepository.GetNextQuizId();

            var defaultQuiz = new Quizzes
            {
                QuizzesID = quizId,
                ChapterID = newChapterId, // Dùng biến newChapterId chắc ăn hơn dùng chapter.ChapterID
                QuizzesName = "Bài kiểm tra chương: " + dto.ChapterName,
                TimeLimit = 15,
                Pass_Score_Percent = 50,
                NUMBER_QUESTIONS = 0,
                Quizzes_Type = "Quiz", // Sửa lại tên thuộc tính cho khớp Entity (Quizzes_Type hay QuizzesType)
                DueDate = DateTime.Now.AddYears(1)
            };

            _quizzRepository.AddQuiz(defaultQuiz);
            _quizzRepository.SaveChanges();
        }

        // 2. THÊM BÀI HỌC
        public void AddLesson(LessonDto dto, string videoFileName)
        {
            var lesson = new Lession
            {
                ChapterID = dto.ChapterId,
                LessionName = dto.LessonName,
                LessionDecription = dto.Description,
                LessionComplate = dto.LessonCompleted,
                VideoData = videoFileName // Tên file video đã lưu trên server
            };

            _courseRepository.AddLesson(lesson);
            _courseRepository.SaveChanges();
        }

        public void DeleteCourse(int courseId)
        {
            // Có thể thêm logic kiểm tra: Nếu khóa học đã có học viên mua thì không cho xóa
            // var enrollments = _enrollmentRepo.GetByCourse(courseId);
            // if (enrollments.Count > 0) throw new Exception("Khóa học đã có người học, không thể xóa!");

            _courseRepository.DeleteCourse(courseId);
        }

        // Implementation
        public bool IsCourseOwner(int userId, int courseId)
        {
            return _courseRepository.CheckCourseOwner((decimal)userId, (decimal)courseId);
        }

        public int GetTotalStudents(int lecturerId)
        {
            return _courseRepository.CountTotalStudentsByLecturer(lecturerId);
        }

    }
}

