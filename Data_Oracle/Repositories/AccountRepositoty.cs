using Data_Oracle.Context;
using Data_Oracle.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Repositories
{
    public class AccountRepositoty : IAccountRepository
    {
        private readonly OracleDBContext _context;

        public AccountRepositoty(OracleDBContext context)
        {
            _context = context;
        }

        public bool AccountExísts(string userName, string password)
        {
            try
            {

                string connStr = $"User Id={userName};Password={password};Data Source=localhost:1521/FREEPDB1";

                using ( var conn = new OracleConnection(connStr) )
                {
                    conn.Open();
                    return true;
                }

                 
            }catch(Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool CreateAccount(string userName, string password)
        {


            try
            {

                string connStr = "User Id=sys;Password=phu232005;Data Source=localhost:1521/FREEPDB1;DBA Privilege=SYSDBA;";

                using ( var conn = new OracleConnection( connStr)) { 

                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {

                         

                        cmd.CommandText = "CREATE_ELEARNING_USER";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_username", OracleDbType.NVarchar2).Value = userName.ToUpper();

                        cmd.Parameters.Add("p_password", OracleDbType.NVarchar2).Value = password;

                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                var erro = ex.Message;

                return false;
            }
        }

        public OracleConnection GetOracleConnection(string userName, string password) 
        {
            string connStr = $"User Id={userName.ToUpper()};Password={password};Data Source=localhost:1521/FREEPDB1";

            using (var conn = new OracleConnection(connStr))
            {
                conn.Open() ;
                return conn;
            }

        }

        public OracleConnection GetUserConnection()
        {
            var connStr = "User Id=E_LEARNING;Password=123;Data Source=localhost:1521/FREEPDB1";

            var conn = new OracleConnection(connStr);
            conn.Open();
            return conn;
        }


    }
}