using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implamentatios
{
    public class OracleConnectionManager
    {

        private static readonly Lazy<OracleConnectionManager> _instance =
            new Lazy<OracleConnectionManager>(() => new OracleConnectionManager());

        private string _username;
        private string _password;
        private string _dataSource = "localhost:1521/FREEPDB1";


        private OracleConnectionManager() { }

        public static OracleConnectionManager Instance => _instance.Value;

        public void SetConnect(string userName, string password)
        {
            _username = userName;
            _password = password;
        }

        public OracleConnection GetConnection()
        {
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password))
            {
                throw new InvalidOperationException("Chưa thiết lập thông tin đăng nhập.");
            }

            var connStr = $"User Id={_username.ToUpper()};Password={_password};Data Source={_dataSource}";
            var conn = new OracleConnection(connStr);
            conn.Open();
            return conn;
        }


    }
}
