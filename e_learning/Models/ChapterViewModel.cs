using Data_Oracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_learning.Models
{
    public class ChapterViewModel : Chapter
    {

        public List<Lession> Lessions { get; set; }

        public ChapterViewModel(List<Lession> lessions) {
        
            Lessions = lessions;
        
        }

        public void InitValue(decimal chapterID, decimal courseVideoId, string chapterName, int chapterIndex, string chapterComplate)
        {
            this.CourcesVideoID = courseVideoId;
            this.ChapterID = chapterID;
            this.ChapterName = chapterName;
            this.ChapterIndex = chapterIndex;
            this.ChapterComplated = chapterComplate;

        }

        public ChapterViewModel() { }

    }
}