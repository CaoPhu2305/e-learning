using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implamentatios
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Role GetUserRole(decimal userID)
        {
           return _userRepository.GetUserRole(userID);
        }
    }
}
