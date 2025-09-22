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
    public class RoleRepository : IRoleRepository
    {
        private readonly OracleDBContext _dbContext;

        public RoleRepository(OracleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Role GetRoleByID(int id)
        {
            return _dbContext.Roles.FirstOrDefault(x => x.RoleID == id);
        }

        public Role GetRoleByName(string Name)
        {
            return _dbContext.Roles.FirstOrDefault(x => x.RoleName == Name);
        }
    }
}
