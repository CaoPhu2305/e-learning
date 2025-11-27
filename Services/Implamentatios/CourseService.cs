using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using Data_Oracle.Repositories;
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
            var x =  _quizzRepository.GetQuizzesByChapterID(ChapterID);

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
    }
}
