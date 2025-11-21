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
    public class QuessionRepository : IQuessionRepository
    {

        private readonly OracleDBContext _dbContext;

        public QuessionRepository(OracleDBContext dbContext )
        {
            _dbContext = dbContext;
        }

        public List<AnswerOptions> GetAnswerOptions(int QuessionID)
        {

            var tmp = _dbContext.AnswerOptions.Where(x => x.QuestionsID == QuessionID).ToList();

            return tmp;
        }

        public Dictionary<int, int> GetCorrectAnswersDictionary(int QuizzID)
        {

            using (var db = new OracleDBContext())
            {
                // LINQ chuẩn cho Entity Framework với bool
                var query = from a in db.AnswerOptions
                            join q in db.Questions on a.QuestionsID equals q.QuestionsID
                            where q.QuizzesID == QuizzID
                            // SỬA TẠI ĐÂY: So sánh bool trực tiếp
                            && a.IsCorrect == true
                            select new { q.QuestionsID, a.AnswerOptionsID };

                // Chuyển sang Dictionary
                // GroupBy để an toàn (đề phòng 1 câu có 2 đáp án đúng do lỗi nhập liệu)
                return query.ToList()
                            .GroupBy(x => x.QuestionsID)
                            .ToDictionary(g => (int)g.Key, g => (int)g.First().AnswerOptionsID);
            }
        }
    }
}
