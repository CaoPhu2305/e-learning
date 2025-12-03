using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class QuizDto
    {

        public int ChapterId { get; set; }
        public string QuizName { get; set; }
        public int TimeLimit { get; set; }
        public double PassScore { get; set; }
        public List<QuestionDto> Questions { get; set; }

    }
}
