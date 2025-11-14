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

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public List<Course> GetAll()
        {

            return _courseRepository.GetAllCourse();

        }
    }
}
