using Data_Oracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Interfaces
{
    public interface IQuizzRepository
    {

        Quizzes GetQuizzesByChapterID(int ChapterID);

        List<Questions> GetQuestionsByQuizzID(int QuizzesID);

        decimal GetNextQuizId();

        decimal GetNextQuestionId();

        decimal GetNextAnswerId();

        void AddQuiz(Quizzes quiz);

        void AddQuestion(Questions q);

        void AddAnswer(AnswerOptions a);

        void SaveChanges();
        Quizzes GetQuizById(int quizId);
        void UpdateQuiz(Quizzes quiz);
        void DeleteQuestionsByQuizId(decimal quizId);

        Quizzes GetQuizzesByChapterID1(int ChapterID);

        Quizzes GetQuizzes(int chapterId);

        int CountQuestionsByQuizId(decimal quizId);
    }
}
