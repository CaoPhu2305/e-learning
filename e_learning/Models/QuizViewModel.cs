using Data_Oracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_learning.Models
{
    public class QuizViewModel : Quizzes
    {

        public List<QuestionViewModel> Questions1 { get; set; }

        public QuizViewModel() { 
        
            Questions1 = new List<QuestionViewModel>();

        }


        public void InitValue(string quizzName, string quizzType, float passScore, int chapterId, DateTime DueDate, int timeLinmit, int numberOfQuession)
        {
            this.QuizzesName = quizzName;
            this.Quizzes_Type = quizzType;
            this.Pass_Score_Percent = passScore;
            this.ChapterID = chapterId;
            this.DueDate = DueDate;
            this.TimeLimit  = timeLinmit;
            this.NUMBER_QUESTIONS = numberOfQuession;
        }


    }
}