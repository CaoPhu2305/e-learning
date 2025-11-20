using Data_Oracle.Context;
using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Repositories
{
    public class QuizzRepository : IQuizzRepository
    {

        private readonly OracleDBContext _dbContext;

        public QuizzRepository( OracleDBContext oracleDBContext)
        {
            this._dbContext = oracleDBContext;
        }

        public List<Questions> GetQuestionsByQuizzID(int QuizzesID)
        {
            return _dbContext.Questions.Where(x => x.QuizzesID == QuizzesID).ToList();
        }

        public Quizzes GetQuizzesByChapterID(int ChapterID)
        {
            try
            {
                var tmp = _dbContext.Quizzes.FirstOrDefault(x => x.QuizzesID == ChapterID);
                return tmp;

            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }

            return null;

        }
    }
}
