using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class LessonDto
    {

        public int ChapterId { get; set; }
        public string LessonName { get; set; }
        public string Description { get; set; }
        public string LessonCompleted { get; set; }

    }
}
