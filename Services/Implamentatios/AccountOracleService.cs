using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using Data_Oracle.Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            string userName = _userRepository.GetUserByEmail(eamil).UserName;

            if (userName == null) // có thể là chuổi rỗng
                return null;

            var connString = $"User Id={userName};Password={password};Data Source=FREEPDB1;";

            try
            {
                using (var conn = new Oracle.ManagedDataAccess.Client.OracleConnection(connString))
                {
                    conn.Open();
                    conn.Close();
                }

                message = "Đăng nhập thành công (Oracle)";
                return true;
            }
            catch
            {
                message = "Sai username hoặc password (Oracle)";
                return false;
            }


            return null;
        }

        public void Register(string email, string password, string userName)
        {
           
            if(_userRepository.GetUserByEmail(email) != null || _accountRepository.AccountExísts(userName)){
                Console.WriteLine("User is Exists");
                return;
            }

            try
            {

                if(!_accountRepository.CreateAccount(userName, password))
                {
                    Console.WriteLine("Đăng Ký Thất Bại");
                    return;
                }

                string hashPassword = _passwordHasher.HashPassword(password);

                User newUser = new User(userName, email, hashPassword);

                _userRepository.AddUser(newUser);

                decimal userID = _userRepository.GetUserByEmail(email).UserID;

                UserRole newUserRole = new UserRole(userID, 3);

                _userRepository.AddUserRole(newUserRole);



            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
