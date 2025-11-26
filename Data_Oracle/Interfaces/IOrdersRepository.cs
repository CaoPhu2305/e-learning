using Data_Oracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Interfaces
{
    public interface IOrdersRepository
    {

        int CreateBuyOrder(int userId, int courseId);

        Order GetOrderByID(int orderID);

        bool ConfirmPayment(int orderId, decimal amount);
    }
}
