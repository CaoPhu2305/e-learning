using Data_Oracle.Entities;
using e_learning.Models;
using e_learning.Util;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_learning.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course

        private ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public ActionResult CourseDetail(int courseID)
        {

            Course course = _courseService.GetCourseByID(courseID);

            

            if (course.CourseType.CourcesTypeID == 1)
            {

                Data_Oracle.Entities.CourseVideo courseVideo = _courseService.GetCourseVideoByID(courseID);

                List<Chapter> chapters = _courseService.GetChapterByCouresID((int)courseVideo.CourseVideoID);

                List<ChapterViewModel> chapterViewModel = new List<ChapterViewModel>();

                foreach (Chapter chapter in chapters)
                {

                    ChapterViewModel chapterViewModelTmp = new ChapterViewModel();    

                    List<Lession> lessions = _courseService.GetLessionByChapterId((int)chapter.ChapterID);

                    chapterViewModelTmp.InitValue(chapter.ChapterID, chapter.CourcesVideoID, chapter.ChapterName, chapter.ChapterIndex, chapter.ChapterComplated);

                    chapterViewModelTmp.Lessions = lessions;

                    chapterViewModel.Add(chapterViewModelTmp);

                }

                Models.CourseVideo courseVideo1 = new Models.CourseVideo(chapterViewModel);

                courseVideo1.InitValue(courseVideo.CourseVideoID, courseVideo.CourcesVideoName, courseVideo.CourcesVideoLevel, courseVideo.CourcesVideoDuration, courseVideo.NumberOfLession, courseVideo.NumberOfStudent);


                var tmp1 = courseVideo1;

                ViewBag.CourseVideo = courseVideo1;
                
            }

            if (course != null)
            {
               
                return View(course);
            }

            return View();
        }

        public ActionResult CourseLearn(int courseID)
        {

            Course course = _courseService.GetCourseByID(courseID);

            if (course.CourseType.CourcesTypeID == 1)
            {

                Data_Oracle.Entities.CourseVideo courseVideo = _courseService.GetCourseVideoByID(courseID);

                List<Chapter> chapters = _courseService.GetChapterByCouresID((int)courseVideo.CourseVideoID);

                List<ChapterViewModel> chapterViewModel = new List<ChapterViewModel>();

                foreach (Chapter chapter in chapters)
                {

                    ChapterViewModel chapterViewModelTmp = new ChapterViewModel();

                    List<Lession> lessions = _courseService.GetLessionByChapterId((int)chapter.ChapterID);

                    chapterViewModelTmp.InitValue(chapter.ChapterID, chapter.CourcesVideoID, chapter.ChapterName, chapter.ChapterIndex, chapter.ChapterComplated);

                    chapterViewModelTmp.Lessions = lessions;

                    chapterViewModel.Add(chapterViewModelTmp);

                }

                Models.CourseVideo courseVideo1 = new Models.CourseVideo(chapterViewModel);

                courseVideo1.InitValue(courseVideo.CourseVideoID, courseVideo.CourcesVideoName, courseVideo.CourcesVideoLevel, courseVideo.CourcesVideoDuration, courseVideo.NumberOfLession, courseVideo.NumberOfStudent);


                var tmp1 = courseVideo1;

                ViewBag.CourseVideo = courseVideo1;
               

            }

            if (course != null)
            {

                
                return View(course);
            }

            return View();
        }

        [HttpGet]
        public ActionResult LoadQuizPartial(int ChapterID)
        {

            Quizzes quizze = _courseService.getQuizzByChapterID(ChapterID);

            ViewBag.Quizzes = quizze;

            return PartialView("_QuizPartial");
        }

        public ActionResult LoadVideoPartial(int lessionID)
        {
            if (lessionID == 0)
            {
                return PartialView("_VideoPlayer", null);
            }

            var lesson = _courseService.GetLessionByLessionID(lessionID);

            if (lesson == null)
            {
                return HttpNotFound();
            }

            // 2. Trả về PartialView CHỈ chứa video
            return PartialView("_VideoPlayer", lesson);
        }

        [HttpGet]
        public ActionResult _Sidebar(int CourseId)
        {
            

            return PartialView();
        }       

    }
}