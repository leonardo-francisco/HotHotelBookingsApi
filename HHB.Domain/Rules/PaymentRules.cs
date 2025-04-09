using HHB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Rules
{
    public static class PaymentRules
    {
        public static bool CanIssueRefund(PaymentStatus paymentStatus)
        {
            return paymentStatus == PaymentStatus.Paid || paymentStatus == PaymentStatus.PartiallyPaid;
        }

        public static bool IsPaymentOverdue(DateTime dueDate)
        {
            return DateTime.UtcNow > dueDate;
        }
    }
}
