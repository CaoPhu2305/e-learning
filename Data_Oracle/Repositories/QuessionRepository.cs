using Data_Oracle.Context;
using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Repositories
{
    public class QuessionRepository : IQuessionRepository
    {

        private readonly OracleDBContext _dbContext;

        public QuessionRepository(OracleDBContext dbContext )
        {
            _dbContext = dbContext;
        }

        public List<AnswerOptions> GetAnswerOptions(int QuessionID)
        {

            var tmp = _dbContext.AnswerOptions.Where(x => x.QuestionsID == QuessionID).ToList();

            return tmp;
        }
    }
}
