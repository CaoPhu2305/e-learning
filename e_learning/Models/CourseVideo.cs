using Data_Oracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_learning.Models
{
    public class CourseVideo : Data_Oracle.Entities.CourseVideo
    {
        public CourseVideo() { }

        public List<ChapterViewModel> Chapters { get; set; }

        public CourseVideo(List<ChapterViewModel> chapters)
        {
            Chapters = chapters;
        }

        public void InitValue(decimal CourseVideoID, string CourseVideoName, string CourseVideoLevel, string CourcesVideoDuration,
            decimal NumberOfLession, decimal NumberOfStudent)
        {
            this.CourseVideoID = CourseVideoID;
            this.CourcesVideoLevel = CourseVideoLevel;
            this.CourcesVideoName = CourseVideoName;
            this.CourcesVideoDuration = CourcesVideoDuration;
            this.NumberOfLession = NumberOfLession;
            this.NumberOfStudent = NumberOfStudent;
        }

    }
}