using Data_Oracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Interfaces
{
    public interface IUserAnswersRepository
    {

        void CreateUserAnswer(UserAnswers userAnswers);

        Dictionary<int, int> GetUserAnswersByAttemptId(int AttemptId);

        

    }
}
