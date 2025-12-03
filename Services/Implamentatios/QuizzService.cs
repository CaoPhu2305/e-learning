using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using Services.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implamentatios
{
    public class QuizzService : IQuizzService
    {

        private readonly IQuessionRepository _quessionRepository;
        private readonly IQuizzRepository _quizzRepository;
        private readonly IQuizAttemptRepository _quizAttemptRepository;
        private readonly IUserAnswersRepository  _userAnswersRepository;

        public QuizzService(IQuessionRepository quessionRepository, 
            IQuizzRepository quizzRepository,
            IQuizAttemptRepository quizAttemptRepository,
            IUserAnswersRepository userAnswersRepository)
        {
            _quessionRepository = quessionRepository;
            this._quizzRepository = quizzRepository;
            _quizAttemptRepository = quizAttemptRepository;
            _userAnswersRepository = userAnswersRepository;
        }

        public void CreateAttempt(QuizAttempt quizAttempt)
        {
            _quizAttemptRepository.CreateAttempt(quizAttempt);
        }

        public void CreateUserAnswer(UserAnswers userAnswers)
        {
            _userAnswersRepository.CreateUserAnswer(userAnswers);
        }

        public List<AnswerOptions> GetAnswerOptions(int QuestionID)
        {
            return _quessionRepository.GetAnswerOptions(QuestionID);
        }

        public Dictionary<int, int> GetCorrectAnswersDictionary(int QuizzID)
        {
            var tmp = _quessionRepository.GetCorrectAnswersDictionary(QuizzID);
            return tmp;
        }

        public QuizAttempt GetLatestAttempt(int UserID, int QuizzID)
        {
            return _quizAttemptRepository.GetLatestAttempt(UserID, QuizzID);
        }

        public List<Questions> GetQuestions(int QuizzID)
        {
            return _quizzRepository.GetQuestionsByQuizzID(QuizzID);
        }

        public QuizAttempt GetQuizAttempt(int QuizAttempt)
        {
            return _quizAttemptRepository.GetAttempt(QuizAttempt);
        }

        public Quizzes GetQuizzes(int ChapterID)
        {
            return _quizzRepository.GetQuizzesByChapterID(ChapterID);
        }

        public Dictionary<int, int> GetUserAnswersByAttemptId(int AtemptID)
        {
            return _userAnswersRepository.GetUserAnswersByAttemptId(AtemptID);
        }

        public bool CreateFullQuiz(QuizDto dto)
        {
            // Sử dụng Transaction nếu Repository hỗ trợ, hoặc chạy tuần tự
            try
            {
                // 1. TẠO QUIZ HEADER
                // Lấy ID thủ công (vì Oracle hay lỗi vụ này)
                decimal quizId = _quizzRepository.GetNextQuizId();

                var quiz = new Quizzes
                {
                    QuizzesID = quizId,
                    ChapterID = dto.ChapterId,
                    QuizzesName = dto.QuizName,
                    TimeLimit = dto.TimeLimit,
                    Pass_Score_Percent = (float)dto.PassScore,
                    NUMBER_QUESTIONS = dto.Questions.Count,
                    Quizzes_Type = "Quiz",
                    DueDate = DateTime.Now.AddMonths(6)
                };

                _quizzRepository.AddQuiz(quiz);
                _quizzRepository.SaveChanges(); // Lưu Header trước

                // 2. TẠO CÂU HỎI (QUESTIONS)
                foreach (var qDto in dto.Questions)
                {
                    decimal questionId = _quizzRepository.GetNextQuestionId();

                    var question = new Questions
                    {
                        QuestionsID = questionId,
                        QuizzesID = quizId, // Link với Quiz vừa tạo
                        QuestionsContent = qDto.Content
                    };

                    _quizzRepository.AddQuestion(question);
                    _quizzRepository.SaveChanges(); // Lưu câu hỏi để lấy ID dùng cho đáp án

                    // 3. TẠO ĐÁP ÁN (ANSWERS)
                    foreach (var aDto in qDto.Answers)
                    {
                        decimal answerId = _quizzRepository.GetNextAnswerId();

                        var answer = new AnswerOptions
                        {
                            AnswerOptionsID = answerId,
                            QuestionsID = questionId, // Link với Question vừa tạo
                            AnswerOptionsName = aDto.Content,

                            // Kiểm tra kỹ kiểu dữ liệu IsCorrect trong Entity của bạn là bool hay string
                            // Nếu là bool:
                            IsCorrect = aDto.IsCorrect
                            // Nếu là string/int: = aDto.IsCorrect ? "1" : "0";
                        };

                        _quizzRepository.AddAnswer(answer);
                    }
                    // Lưu danh sách đáp án của câu này
                    _quizzRepository.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi: System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool UpdateFullQuiz(int quizId, QuizDto dto)
        {
            // Sử dụng Transaction để đảm bảo an toàn (Xóa cũ mà Thêm mới lỗi thì phải hoàn tác)
            // Nếu Repository của bạn chưa hỗ trợ Transaction, hãy chạy tuần tự nhưng cẩn thận.
            try
            {
                // 1. CẬP NHẬT THÔNG TIN CHUNG (Header)
                // Lấy Quiz cũ lên
                var quiz = _quizzRepository.GetQuizById(quizId);
                if (quiz == null) return false;

                // Gán thông tin mới
                quiz.QuizzesName = dto.QuizName;
                quiz.TimeLimit = dto.TimeLimit;
                quiz.Pass_Score_Percent = (float)dto.PassScore;
                quiz.NUMBER_QUESTIONS = dto.Questions.Count; // Cập nhật lại số câu hỏi

                // Gọi Repo lưu thay đổi Header
                _quizzRepository.UpdateQuiz(quiz);


                // 2. XÓA SẠCH CÂU HỎI CŨ (VÀ ĐÁP ÁN CŨ)
                // Bạn cần viết hàm này trong Repository (xem bước 4)
                _quizzRepository.DeleteQuestionsByQuizId(quizId);


                // 3. THÊM LẠI DANH SÁCH CÂU HỎI MỚI (Giống hệt Logic Create)
                foreach (var qDto in dto.Questions)
                {
                    // Lấy ID mới cho câu hỏi
                    decimal questionId = _quizzRepository.GetNextQuestionId();

                    var question = new Questions
                    {
                        QuestionsID = questionId,
                        QuizzesID = (decimal)quizId, // Gán vào Quiz hiện tại
                        QuestionsContent = qDto.Content
                    };
                    _quizzRepository.AddQuestion(question);
                    _quizzRepository.SaveChanges(); // Lưu câu hỏi

                    // Thêm đáp án
                    foreach (var aDto in qDto.Answers)
                    {
                        decimal answerId = _quizzRepository.GetNextAnswerId();
                        var answer = new AnswerOptions
                        {
                            AnswerOptionsID = answerId,
                            QuestionsID = questionId,
                            AnswerOptionsName = aDto.Content,
                            IsCorrect = aDto.IsCorrect // (Lưu ý kiểu bool/string như đã bàn trước đó)
                        };
                        _quizzRepository.AddAnswer(answer);
                    }
                    _quizzRepository.SaveChanges(); // Lưu đáp án
                }

                return true;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi
                return false;
            }
        }

    }
}
