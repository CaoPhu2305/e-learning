using Data_Oracle.Entities;
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
     
    }
}
