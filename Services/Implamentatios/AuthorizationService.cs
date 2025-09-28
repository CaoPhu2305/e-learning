using Data_Oracle.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implamentatios
{
    public class AuthorizationService : IAuthorizationService
    {

        public readonly IRbacRepository _rbacRepository;

        public AuthorizationService(IRbacRepository rbacRepository)
        {
            _rbacRepository = rbacRepository;
        }

        public bool HasPermission(string userName, string resource, string permission)
        {

            if (string.IsNullOrWhiteSpace(userName)) return false;

            if (string.IsNullOrWhiteSpace(resource)) return false;

            if (string.IsNullOrWhiteSpace(permission)) return false;

            return _rbacRepository.CheckPermission(userName, resource, permission);
           
        }
    }
}
