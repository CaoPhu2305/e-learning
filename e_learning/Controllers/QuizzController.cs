using Data_Oracle.Entities;
using e_learning.Models;
using Services.Implamentatios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Formats.Asn1.AsnWriter;

namespace e_learning.Controllers
{
    public class QuizzController : Controller
    {
        // GET: Quizz

        private readonly QuizzService _quizzService;
       

        public QuizzController(QuizzService quizzService)
        {
            _quizzService = quizzService;
        }

        public ActionResult Index(int ChapterID)
        {

            Quizzes quizzes = _quizzService.GetQuizzes(ChapterID);

            List<Questions> questions = _quizzService.GetQuestions((int)quizzes.QuizzesID);

            QuizViewModel viewModel = new QuizViewModel();

            viewModel.InitValue(quizzes.QuizzesName, quizzes.Quizzes_Type, quizzes.Pass_Score_Percent,(int) quizzes.ChapterID, quizzes.DueDate,(int)quizzes.TimeLimit ,(int)quizzes.NUMBER_QUESTIONS);
            viewModel.QuizzesID = quizzes.QuizzesID;
            foreach (var x in questions) {

                List<AnswerOptions> answerOptions = _quizzService.GetAnswerOptions((int)x.QuestionsID);

                QuestionViewModel questionViewModel = new QuestionViewModel(answerOptions);

                questionViewModel.InitValue((int)x.QuestionsID, x.QuestionsContent);

                var w = questionViewModel;

                viewModel.Questions1.Add(questionViewModel);

            }

            var tmp = viewModel;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SubmitQuiz(FormCollection form)
        {
            // --- PHẦN 1: HỨNG DỮ LIỆU TỪ FORM (CHỐNG NULL) ---
            int QuizID = 0;
            if (!string.IsNullOrEmpty(form["QuizID"]))
            {
                QuizID = int.Parse(form["QuizID"]);
            }

            Dictionary<int, int> UserAnswers = new Dictionary<int, int>();
            foreach (string key in form.AllKeys)
            {
                if (key.StartsWith("UserAnswers["))
                {
                    // Lấy QuestionID: "UserAnswers[101]" -> 101
                    int qId = int.Parse(key.Replace("UserAnswers[", "").Replace("]", ""));

                    // Lấy AnswerID: Xử lý trường hợp thừa dấu phẩy "500,false" do hidden input
                    string val = form[key];
                    if (val.Contains(",")) val = val.Split(',')[0];
                    int aId = int.Parse(val);

                    if (!UserAnswers.ContainsKey(qId)) UserAnswers.Add(qId, aId);
                }
            }


            var a = QuizID;

            // --- PHẦN 2: CHẤM ĐIỂM ---
            var quiz = _quizzService.GetQuizzes(QuizID);

            // Lấy đáp án đúng (Đã sửa logic bool ở trên)
            var correctAnswers = _quizzService.GetCorrectAnswersDictionary(QuizID);

            var tmp = correctAnswers;

            int correctCount = 0;
            foreach (var item in UserAnswers)
            {
                // item.Key = QuestionID, item.Value = AnswerID user chọn
                if (correctAnswers.ContainsKey(item.Key))
                {
                    if (correctAnswers[item.Key] == item.Value)
                    {
                        correctCount++;
                    }
                }
            }

            int totalQ = 0;
            if (quiz.NUMBER_QUESTIONS != null) int.TryParse(quiz.NUMBER_QUESTIONS.ToString(), out totalQ);

            float score = (totalQ > 0) ? ((float)correctCount / totalQ) * 100 : 0;
            bool isPass = score >= quiz.Pass_Score_Percent;

  
            bool passString = isPass;

            var b = passString;

            var attempt = new QuizAttempt(QuizID, 23, DateTime.Now, score, passString);



            _quizzService.CreateAttempt(attempt);

            QuizAttempt quizAttempt = _quizzService.GetLatestAttempt((int)attempt.UserID, (int)attempt.QuizzesID);

            int attemptId = (int)quizAttempt.QuizAttemptID;

            foreach (var item in UserAnswers)
            {
                // Khởi tạo đối tượng rỗng
                var detail = new Data_Oracle.Entities.UserAnswers();

                // Gán thuộc tính thủ công để tránh nhầm lẫn
                detail.UserAnswersID = 0; // QUAN TRỌNG: Luôn để 0 để DB tự sinh
                detail.QuizAttemptID = attemptId;
                detail.QuestionsID = item.Key;
                detail.AnswerOptionsID = item.Value;

                _quizzService.CreateUserAnswer(detail);
            }

            return RedirectToAction("ReviewQuiz", new { attemptId = attemptId });
        }

        public ActionResult ReviewQuiz(int attemptId)
        {
            // 1. Lấy lại thông tin lượt thi
            var attempt = _quizzService.GetQuizAttempt(attemptId);
            var quiz = _quizzService.GetQuizzes((int)attempt.QuizzesID);

            // 2. Lấy danh sách câu hỏi gốc
            var questions = _quizzService.GetQuestions((int)quiz.QuizzesID);

            // 3. Lấy danh sách câu trả lời của User trong lượt thi này
            // Dictionary<QuestionID, AnswerID>
            var userChoices = _quizzService.GetUserAnswersByAttemptId(attemptId);

            // 4. Khởi tạo ViewModel
            QuizViewModel viewModel = new QuizViewModel();
            viewModel.InitValue(quiz.QuizzesName, quiz.Quizzes_Type, quiz.Pass_Score_Percent, (int)quiz.ChapterID, quiz.DueDate, (int)quiz.TimeLimit, (int)quiz.NUMBER_QUESTIONS);
            viewModel.Questions1 = new List<QuestionViewModel>();

            // --- SETTINGS CHO CHẾ ĐỘ REVIEW ---
            viewModel.IsReviewMode = true;
            viewModel.FinalScore = (float)attempt.Score;
            viewModel.IsPassResult = attempt.isPass;

            foreach (var x in questions)
            {
                // Lấy options
                List<AnswerOptions> answerOptions = _quizzService.GetAnswerOptions((int)x.QuestionsID);

                // Tìm đáp án đúng của câu này
                var correctAnswer = answerOptions.FirstOrDefault(a => a.IsCorrect == true);

                QuestionViewModel qVM = new QuestionViewModel(answerOptions);
                qVM.InitValue((int)x.QuestionsID, x.QuestionsContent);

                // --- MAP DỮ LIỆU REVIEW ---
                qVM.CorrectAnswerID = correctAnswer != null ? (int)correctAnswer.AnswerOptionsID : 0;

                if (userChoices.ContainsKey((int)x.QuestionsID))
                {
                    qVM.UserSelectedAnswerID = userChoices[(int)x.QuestionsID];
                }

                viewModel.Questions1.Add(qVM);
            }

            // Trả về cùng View "Index" để tái sử dụng giao diện
            return View("Index", viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}