using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_learning.Models
{
    public class MyCourseViewModel
    {

        public decimal CourseID { get; set; }
        public string CourseName { get; set; }
        public string Image { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string StatusName { get; set; }
        public bool IsTrial { get; set; }

    }
}