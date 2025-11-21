using Data_Oracle.Context;
using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Repositories
{
    public class QuizAttemptRepository : IQuizAttemptRepository
    {

        private readonly OracleDBContext _dbContext;

        public QuizAttemptRepository(OracleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateAttempt(QuizAttempt quizAttempt)
        {
            _dbContext.QuizAttempts.Add(quizAttempt);

            _dbContext.SaveChanges();
        }

        public QuizAttempt GetLatestAttempt(int userId, int quizId)
        {

            QuizAttempt tmp =  null;

            using (var db = new OracleDBContext())
            {
                tmp = db.QuizAttempts
                         .Where(x => x.UserID == userId && x.QuizzesID == quizId)
                         .OrderByDescending(x => x.AttemptDate) // Lấy cái mới nhất
                         .FirstOrDefault();
            }

            return tmp;
        }

        public QuizAttempt GetAttempt(int id)
        {
            return _dbContext.QuizAttempts.FirstOrDefault(x => x.QuizAttemptID == id);
        }
    }
}
