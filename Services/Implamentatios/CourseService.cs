using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using Data_Oracle.Repositories;
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
            return _chapterRepository.GetALLChapterByCourseId(CourseId);
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
            var x = _quizzRepository.GetQuizzesByChapterID(ChapterID);

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
            // BƯỚC 1: TẠO VÀ LƯU COURSE
            var course = new Course
            {
                CourseName = dto.CourseName,
                Description = dto.Description,
                Price = (double)dto.Price,
                IsTrialAvailable = dto.IsTrialAvailable,
                Image = imagePath,
                Certificate = "Yes",
                CourseTypeID = 1,
                CourseStatusID = 1,
            };

            _courseRepository.AddCourse(course);

            // Lưu ngay để Trigger sinh ID và EF nhận lại ID đó vào biến 'course'
            _courseRepository.SaveChanges();

            // BƯỚC 2: TẠO VIDEO
            var video = new CourseVideo
            {
                CourseVideoID = course.CourseID, // ID đã có giá trị sau lệnh SaveChanges trên
                CourcesVideoName = course.CourseName,
                CourcesVideoLevel = "Basic",
                CourcesVideoDuration = "0h",
                NumberOfLession = 0,
                NumberOfStudent = 0
            };
            _courseRepository.AddCourseVideo(video);

            // BƯỚC 3: GÁN GIẢNG VIÊN
            var lecturerCourse = new LecturerCourse
            {
                UserID = lecturerId,
                CourseID = course.CourseID,
                CreateAtTime = DateTime.Now
            };
            _courseRepository.AddLecturerCourse(lecturerCourse);

            // BƯỚC 4: LƯU CÁC BẢNG CON
            _courseRepository.SaveChanges();

            // TRẢ VỀ ID MỚI TẠO
            // Vì DB Oracle ID là decimal, ta ép kiểu về int để tiện dùng trong C#
            return (int)course.CourseID;
        }

        public List<Course> GetLecturerCourses(int lecturerId)
        {
            return _courseRepository.GetCoursesByLecturer(lecturerId);
        }

        public void AddChapter(ChapterDto dto)
        {
            // Lấy CourseVideoID từ CourseID
            // (Lưu ý: Ép kiểu int/decimal cho khớp với DB của bạn)
            decimal videoId = _courseRepository.GetCourseVideoIdByCourseId(dto.CourseId);

            var chapter = new Chapter
            {
                CourcesVideoID = videoId,
                ChapterName = dto.ChapterName,
                ChapterComplated = dto.ChapterCompleted,
                // ChapterIndex: Repository sẽ tự tính toán
            };

            _courseRepository.AddChapter(chapter);
            _courseRepository.SaveChanges();
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


    }
}

