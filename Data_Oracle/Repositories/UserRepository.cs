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
    public class UserRepository : IUserRepository
    {

        private readonly OracleDBContext _context;

        public UserRepository(OracleDBContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {



            _context.Users.Add(user);
            _context.SaveChanges();
           
        }

        public void AddUserRole(UserRole userRole)
        {
            _context.UserRole.Add(userRole);
            _context.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }



    }
}
