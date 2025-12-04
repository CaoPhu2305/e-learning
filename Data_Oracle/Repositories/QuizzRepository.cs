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

        public Quizzes GetQuizzes(int chapterId)
        {
            // ĐÚNG: So sánh cột ChapterID
            return _dbContext.Quizzes.FirstOrDefault(q => q.ChapterID == (decimal)chapterId);

            // SAI (Lỗi thường gặp): So sánh q.QuizzesID == chapterId
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

        public Quizzes GetQuizzesByChapterID1(int ChapterID)
        {
            try
            {
                // SỬA LẠI: So sánh x.ChapterID (Cột khóa ngoại) chứ không phải x.QuizzesID (Khóa chính)
                // Ép kiểu (decimal) cho chắc ăn với Oracle
                var tmp = _dbContext.Quizzes
                                    .FirstOrDefault(x => x.ChapterID == (decimal)ChapterID);
                return tmp;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                var x = ex.Message;
            }

            return null;
        }

        public decimal GetNextQuizId()
        {
            return _dbContext.Database.SqlQuery<decimal>("SELECT SEQ_QUIZZES.NEXTVAL FROM DUAL").FirstOrDefault();
        }
        public decimal GetNextQuestionId()
        {
            return _dbContext.Database.SqlQuery<decimal>("SELECT SEQ_QUESTIONS.NEXTVAL FROM DUAL").FirstOrDefault();
        }
        public decimal GetNextAnswerId()
        {
            return _dbContext.Database.SqlQuery<decimal>("SELECT SEQ_ANSWER_OPTIONS.NEXTVAL FROM DUAL").FirstOrDefault();
        }

        // Các hàm Add thông thường (Không SaveChanges)
        public void AddQuiz(Quizzes quiz) { _dbContext.Quizzes.Add(quiz); }
        public void AddQuestion(Questions q) { _dbContext.Questions.Add(q); }
        public void AddAnswer(AnswerOptions a) { _dbContext.AnswerOptions.Add(a); }
        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                // BƯỚC 1: Đặt Breakpoint (dấu chấm đỏ) tại dòng 'var msg = ...' dưới đây
                // BƯỚC 2: Chạy Debug, khi dừng lại, rê chuột vào biến 'innerMsg' để xem

                var innerMsg = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;

                // Hoặc in ra cửa sổ Output của Visual Studio
                System.Diagnostics.Debug.WriteLine("LỖI DB CHI TIẾT: " + innerMsg);

                throw new Exception("Lỗi lưu DB: " + innerMsg); // Ném lỗi ra ngoài để hiện lên web
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                // Lỗi này xảy ra nếu dữ liệu không thỏa mãn DataAnnotation (VD: Required, MaxLength)
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public Quizzes GetQuizById(int quizId)
        {
            return _dbContext.Quizzes.FirstOrDefault(x => x.QuizzesID == quizId);
        }

        public void UpdateQuiz(Quizzes quiz)
        {
            // Đánh dấu đối tượng là Modified để EF biết mà update
            _dbContext.Entry(quiz).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteQuestionsByQuizId(decimal quizId)
        {
            // 1. Tìm tất cả câu hỏi của Quiz này
            var questions = _dbContext.Questions.Where(q => q.QuizzesID == quizId).ToList();

            foreach (var q in questions)
            {
                // 2. Tìm và xóa tất cả đáp án của câu hỏi này trước
                var answers = _dbContext.AnswerOptions.Where(a => a.QuestionsID == q.QuestionsID).ToList();
                _dbContext.AnswerOptions.RemoveRange(answers);

                // 3. Xóa câu hỏi
                _dbContext.Questions.Remove(q);
            }

            // Lưu thay đổi
            _dbContext.SaveChanges();
        }

        public int CountQuestionsByQuizId(decimal quizId)
        {
            return _dbContext.Questions.Count(q => q.QuizzesID == quizId);
        }
    }
}
