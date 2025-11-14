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
        private readonly RoleRepository _roleRepository;

        public CourseService(ICourseRepository courseRepository, RoleRepository roleRepository)
        {
            _courseRepository = courseRepository;
            _roleRepository = roleRepository;
        }

        public List<Course> GetCourses()
        {

            return _courseRepository.GetAllCourse();

        }

        public List<CourseType> GetCoursesType()
        {
            return _courseRepository.GetAllCourseType();
        }

    
    }
}
