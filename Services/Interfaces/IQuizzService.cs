using Data_Oracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IQuizzService
    {

        List<Questions> GetQuestions(int QuizzID);

        List<AnswerOptions> GetAnswerOptions(int QuestionID);

        Quizzes GetQuizzes(int ChapterID);

        Dictionary<int , int> GetCorrectAnswersDictionary(int QuizzID);

        void CreateAttempt(QuizAttempt quizAttempt);

        void CreateUserAnswer(UserAnswers userAnswers);

        QuizAttempt GetQuizAttempt(int QuizAttempt);

        Dictionary<int, int> GetUserAnswersByAttemptId(int AtemptID);

        QuizAttempt GetLatestAttempt(int UserID, int QuizzID);

    }
}
