using Data_Oracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_learning.Models
{
    public class QuestionViewModel : Questions
    {


       public QuestionViewModel() { }

        public List<AnswerOptions> Options { get; set; }

        public int? UserSelectedAnswerID { get; set; } // ID đáp án user đã chọn
        public int CorrectAnswerID { get; set; }       // ID đáp án đúng của câu này

        public QuestionViewModel(List<AnswerOptions> options)
        {
            this.Options = options;
        }
    
        public void InitValue(int questions, string questionsContent)
        {
            this.QuestionsID = questions;
            this.QuestionsContent = questionsContent; 
        }

    }
}