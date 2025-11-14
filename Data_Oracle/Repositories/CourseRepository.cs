using Data_Oracle.Context;
using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
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
    }
}
