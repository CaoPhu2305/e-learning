using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Interfaces
{
    public interface IRbacRepository
    {

        bool CheckPermission(string username, string resourceName, string permissionName);

    }
}
