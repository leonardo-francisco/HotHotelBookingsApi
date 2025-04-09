using HHB.Domain.Entities;
using HHB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Rules
{
    public static class CustomerReviewRules
    {
        public static bool CanSubmitReview(Booking booking)
        {
            // Permite avaliação somente se o hóspede já saiu e a reserva não foi cancelada
            return booking.Status == BookingStatus.CheckedOut;
        }
    }
}
