using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class CourseDto
    {

        public string CourseName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsTrialAvailable { get; set; }

    }
}
