using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using Data_Oracle.Repositories;
using Oracle.ManagedDataAccess.Client;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace Services.Implamentatios
{
    public class AccountOracleService : IAccountService
    {

        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly PBKDF2PasswordHasher _passwordHasher;
        private readonly RoleRepository _roleRepository;

        public AccountOracleService(IAccountRepository accountRepository
            , IUserRepository userRepository
            , PBKDF2PasswordHasher pBKDF2PasswordHasher
            , RoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _passwordHasher = pBKDF2PasswordHasher;
            _roleRepository = roleRepository;
        }

        public User Login(string eamil, string password)
        {

            User user = _userRepository.GetUserByEmail(eamil);

            if (user == null) 
                return null;

            if (!_passwordHasher.VerifyPassword(password, user.HashPassword)) return null;

            if (!_accountRepository.AccountExísts(user.UserName, password)) return null;

            OracleConnectionManager.Instance.SetConnect(user.UserName, password);

            return user;

        }

        public void Register(string email, string password, string userName)
        {
           
            if(_userRepository.GetUserByEmail(email) != null){
                Console.WriteLine("User is Exists");
                return;
            }

            try
            {
                string hashPassword = _passwordHasher.HashPassword(password);

                bool result = _accountRepository.CreateAccount(userName, password);

                if (result) {

                    User newUser = new User(userName, email, hashPassword);

                    _userRepository.AddUser(newUser);

                    decimal userID = _userRepository.GetUserByEmail(email).UserID;

                    UserRole newUserRole = new UserRole(userID, 3);

                    var tmp = newUserRole;

                    _userRepository.AddUserRole(newUserRole);

                }

                return;
               
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public OracleConnection GetOracleConnection(string userName, string password)
        {

            return _accountRepository.GetOracleConnection(userName, password);

        }

    }
}
