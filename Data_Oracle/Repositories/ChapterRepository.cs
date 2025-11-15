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
    public class ChapterRepository : IChapterRepository
    {

        private readonly OracleDBContext _dbContext;

        public ChapterRepository(OracleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ChapterRepository() { }

        public List<Chapter> GetALLChapterByCourseId(int courseId)
        {
            List<Chapter> chapterList = new List<Chapter>();

            return  chapterList = _dbContext.Chapters.Where(x => x.CourcesVideoID == courseId).ToList();

        }
    }
}
