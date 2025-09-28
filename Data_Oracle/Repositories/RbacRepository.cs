using Data_Oracle.Context;
using Data_Oracle.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Repositories
{
    public class RbacRepository : IRbacRepository
    {

        private readonly OracleDBContext _dbContext;

        public RbacRepository(OracleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CheckPermission(string username, string resourceName, string permissionName)
        {
            var query = from u in _dbContext.Users.AsNoTracking()
                        where u.Email == username
                        join ur in _dbContext.UserRole.AsNoTracking()
                        on u.UserID equals ur.UserID
                        join r in _dbContext.Roles.AsNoTracking()
                        on ur.RoleID equals r.RoleID
                        join rpr in _dbContext.RolePermissionResourcesList.AsNoTracking()
                        on r.RoleID equals rpr.RoleID
                        join res in _dbContext.Resources.AsNoTracking()
                        on rpr.ResourcesID equals res.ResourcesID
                        join p in _dbContext.Permissions.AsNoTracking()
                        on rpr.PermissionID equals p.PermissionID
                        where res.ResourcesName == resourceName && p.PermissionName == permissionName
                        select rpr;
            return query.Any();
        }
    }
}
