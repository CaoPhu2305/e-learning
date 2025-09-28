using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Interfaces
{
    public interface IAccountRepository
    {
        bool CreateAccount(string userName, string password);

        bool AccountExísts(string userName);


    }
}
