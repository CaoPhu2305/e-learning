using Data_Oracle.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Interfaces
{
    public interface ICourseRepository
    {

        List<Course> GetAllCourse();

        List<CourseType> GetAllCourseType();

        Course GetCourseByID(int id);

        CourseVideo GetCourseVideoById(int id);

        List<Course> GetCoursesNotEnrolled(int userId);

        // Data_Oracle/Repositories/CourseRepository.cs

        void AddCourse(Course course);
            


        void AddCourseVideo(CourseVideo video);



        void AddLecturerCourse(LecturerCourse lecturerCourse);



        void SaveChanges();


        // Hàm lấy khóa học của giảng viên (Join bảng LecturerCourse)
        List<Course> GetCoursesByLecturer(int lecturerId);

        decimal GetCourseVideoIdByCourseId(decimal courseId);

        void AddChapter(Chapter chapter);

        void AddLesson(Lession lesson);
    }
}
