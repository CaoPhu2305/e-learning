using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implamentatios
{
    public interface IPasswordHasher
    {

        string HashPassword(string password);

        bool VerifyPassword(string password, string hashedPassword);

    }
}
