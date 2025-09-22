using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using Data_Oracle.Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implamentatios
{
    public class AccountService : IAccountService
    {

        public readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRoleRepository _roleRepository;

        public AccountService(IUserRepository userRepository
            , IPasswordHasher passwordHasher
            , IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
        }

        public User Login(string eamil, string password)
        {
            //var user = _userRepository.GetUserByEmail(eamil);

            var user = _userRepository.GetUserByEmail(eamil);

            if (user == null) return null;

            bool isValid = _passwordHasher.VerifyPassword(password, user.HashPassword);

            return isValid ? user : null;

        }

        public void Register(string email, string password, string userName)
        {
            string hashPasword = _passwordHasher.HashPassword(password);

            User newUser = new User(userName, email, hashPasword);

            Role defaultRole = _roleRepository.GetRoleByID(1);
          
            try
            {
                _userRepository.AddUser(newUser);

                User tmp = _userRepository.GetUserByEmail(newUser.Email);

                UserRole userRole = new UserRole(tmp.UserID, defaultRole.RoleID);  

                _userRepository.AddUserRole(userRole);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
