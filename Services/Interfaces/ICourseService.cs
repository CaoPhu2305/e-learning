using Data_Oracle.Entities;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public  interface ICourseService
    {

        List<Course> GetCourses();

        List<CourseType> GetCoursesType();

        Course GetCourseByID(int id);

        CourseVideo GetCourseVideoByID(int id);

        List<Chapter> GetChapterByCouresID(int CourseId);

        List<Lession> GetLessionByChapterId(int ChapterId);

        Lession GetLessionByLessionID(int LessionID);

        Quizzes getQuizzByChapterID(int ChapterID);

        List<Enrollments> GetEnrollmentsByUserId(int userId);

        Enrollments GetEnrollments(int userID,int courseID);

        List<Course> GetUnpurchasedCourses(int userId);

        int CreateFullCourse(CourseDto dto, int lecturerId, string imagePath);

        List<Course> GetLecturerCourses(int lecturerId);

        void AddChapter(ChapterDto dto);

        void AddLesson(LessonDto dto, string videoFileName);

        void DeleteCourse(int courseId);

        bool IsCourseOwner(int userId, int courseId);

        int GetTotalStudents(int lecturerId);


    }
}
