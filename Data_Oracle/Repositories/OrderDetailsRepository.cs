using Data_Oracle.Context;
using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {

        private readonly OracleDBContext _dbContext;

        public OrderDetailsRepository(OracleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public OrderDetail GetOrderDetailByOderId(int OderID)
        {
            return _dbContext.OrderDetails.FirstOrDefault(x => x.OderID == OderID);
        }
    }
}
