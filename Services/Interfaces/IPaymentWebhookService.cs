using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPaymentWebhookService
    {

        bool ProcessPaymentWebhook(string content, decimal amount);
        int ExtractOrderId(string content);


    }
}

