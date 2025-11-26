using Data_Oracle.Entities;
using Data_Oracle.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implamentatios
{

   

    public class OrderService : IOderService
    {

        private readonly IOrdersRepository _ordersRepository;

        public OrderService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

       public Order GetOrderByOrderID(int orderID)
        {
            return _ordersRepository.GetOrderByID(orderID);
        }

       

    }
}
