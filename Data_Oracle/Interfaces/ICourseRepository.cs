using Data_Oracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Interfaces
{
    public interface ICourseRepository
    {

        List<Course> GetAllCourse();

        List<CourseType> GetAllCourseType();

    }
}
