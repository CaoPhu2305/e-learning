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

        public QuizzService(IQuessionRepository quessionRepository, IQuizzRepository quizzRepository)
        {
            _quessionRepository = quessionRepository;
            this._quizzRepository = quizzRepository;
        }

        public List<AnswerOptions> GetAnswerOptions(int QuestionID)
        {
            return _quessionRepository.GetAnswerOptions(QuestionID);
        }

        public List<Questions> GetQuestions(int QuizzID)
        {
            return _quizzRepository.GetQuestionsByQuizzID(QuizzID);
        }

        public Quizzes GetQuizzes(int ChapterID)
        {
            return _quizzRepository.GetQuizzesByChapterID(ChapterID);
        }
    }
}
