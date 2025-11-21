using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
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
    }
}
