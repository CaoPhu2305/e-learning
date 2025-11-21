using Data_Oracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Interfaces
{
    public interface IQuizAttemptRepository
    {

        void CreateAttempt(QuizAttempt quizAttempt);


        QuizAttempt GetAttempt(int id);

        QuizAttempt GetLatestAttempt(int userId, int quizId);

    }
}
