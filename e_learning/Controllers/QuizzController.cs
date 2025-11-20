using Data_Oracle.Entities;
using e_learning.Models;
using Services.Implamentatios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Create()
        {
            return View();
        }
    }
}