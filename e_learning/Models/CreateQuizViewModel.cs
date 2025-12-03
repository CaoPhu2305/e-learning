using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_learning.Models
{
    public class CreateQuizViewModel
    {

       
         public int ChapterId { get; set; }
        public string QuizName { get; set; }
        public int TimeLimit { get; set; } // Phút
        public double PassScore { get; set; } // %
        public List<QuestionItem> Questions { get; set; }
    }
}