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
    public class UserAnswersRepository : IUserAnswersRepository
    {


        private readonly OracleDBContext _dbContext;

        public UserAnswersRepository(OracleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateUserAnswer(UserAnswers userAnswers)
        {

            using (var context = new OracleDBContext())
            {
                try
                {
                    // Đảm bảo ID là 0 để Trigger Oracle hoạt động
                    userAnswers.UserAnswersID = 0;

                    context.UserAnswers.Add(userAnswers);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    // Log lỗi hoặc throw tùy bạn
                    throw ex;
                }
            } //


        }

        public Dictionary<int, int> GetUserAnswersByAttemptId(int AttemptId)
        {
            using (var db = new OracleDBContext ()) // Thay YourDbContext bằng tên DbContext của bạn
            {
                // Cách 1: Dùng LINQ Method Syntax (Ngắn gọn)
                var result = db.UserAnswers
                    .Where(u => u.QuizAttemptID == AttemptId)
                    .Select(u => new
                    {
                        QuestionID = u.QuestionsID,
                        AnswerID = u.AnswerOptionsID,
                    })
                    .ToDictionary(
                        k => (int)k.QuestionID,      // Key: ID Câu hỏi
                        v => (int)v.AnswerID         // Value: ID Đáp án user chọn
                    );

                return result;
            }
        }
    }
}
