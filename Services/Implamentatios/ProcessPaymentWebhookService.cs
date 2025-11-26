using Data_Oracle.Context;
using Data_Oracle.Interfaces;
using e_learning.Helper;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Implamentatios
{
    public class ProcessPaymentWebhookService : IPaymentWebhookService
    {

        private readonly IOrdersRepository _ordersRepository;

   

        public ProcessPaymentWebhookService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public bool ProcessPaymentWebhook(string content, decimal amount)
        {
            int orderId = ExtractOrderId(content);

            if (orderId <= 0) return false;

            // 2. Gọi xuống Repository để thực hiện Transaction DB
            return _ordersRepository.ConfirmPayment(orderId, amount);
        }

        private int ExtractOrderId(string content)
        {
            if (string.IsNullOrEmpty(content)) return 0;
            var match = Regex.Match(content, @"\d+");
            return match.Success ? int.Parse(match.Value) : 0;
        }

        int IPaymentWebhookService.ExtractOrderId(string content)
        {
            if (string.IsNullOrEmpty(content)) return 0;
            var match = Regex.Match(content, @"\d+");
            return match.Success ? int.Parse(match.Value) : 0;
        }
    }
}
