using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Enums
{
    public enum PaymentStatus
    {
        Unpaid = 0,         // No payment has been made
        Pending = 1,        // Payment is in process (e.g., waiting for confirmation)
        Paid = 2,           // Full payment received
        PartiallyPaid = 3,  // Partial payment received
        Refunded = 4,       // Full refund issued
        PartiallyRefunded = 5, // Partial refund issued
        Failed = 6,         // Payment attempt failed
        Cancelled = 7       // Payment was canceled before completion
    }
}
