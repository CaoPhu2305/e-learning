using Data_Oracle.Context;
using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Repositories
{
    public class LessionRepository : ILessionRepository
    {

        private readonly OracleDBContext _dbContext;

        public LessionRepository( OracleDBContext oracleDBContext)
        {
            _dbContext = oracleDBContext;
        }

        public List<Lession> GetAllLessionByChapterId(int chappterID)
        {
           return _dbContext.Lessions.Where(x => x.ChapterID == chappterID).ToList();
        }

        public Lession GetLessionByLessionID(int chapterID)
        {
            return _dbContext.Lessions.FirstOrDefault(x => x.LessionID == chapterID);
        }
    }
}
