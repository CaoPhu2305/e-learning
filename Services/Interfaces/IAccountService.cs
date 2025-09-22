using Data_Oracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAccountService
    {

        User Login(string eamil, string password);

        void Register(string email, string password, string userName);

    }
}
