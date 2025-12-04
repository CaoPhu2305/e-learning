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

        public List<Chapter> GetChapterByCouresID(int courseVideoId) // Hoặc decimal tùy khai báo của bạn
        {
            return _dbContext.Chapters
                // Lọc theo Video ID
                .Where(x => x.CourcesVideoID == (decimal)courseVideoId)

                // QUAN TRỌNG: Sửa thành OrderBy (Tăng dần) thay vì OrderByDescending
                .OrderBy(x => x.ChapterIndex)

                // (Tùy chọn) Nếu ChapterIndex trùng nhau thì sắp xếp theo ID
                .ThenBy(x => x.ChapterID)

                .ToList();
        }
    }
}
