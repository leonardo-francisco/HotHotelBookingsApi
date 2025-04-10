using HHB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Rules
{
    public static class BookingRules
    {
        public static bool CanCheckIn(BookingStatus bookingStatus, PaymentStatus paymentStatus)
        {
            return bookingStatus == BookingStatus.Confirmed &&
                   (paymentStatus == PaymentStatus.Paid || paymentStatus == PaymentStatus.PartiallyPaid);
        }

        public static bool CanCheckOut(BookingStatus bookingStatus, PaymentStatus paymentStatus)
        {
            return bookingStatus == BookingStatus.CheckedIn &&
                   (paymentStatus == PaymentStatus.Paid || paymentStatus == PaymentStatus.PartiallyPaid);
        }

        public static bool CanCancel(BookingStatus bookingStatus, PaymentStatus paymentStatus)
        {
            return bookingStatus == BookingStatus.Pending ||
                   bookingStatus == BookingStatus.Confirmed ||
                   (bookingStatus == BookingStatus.CheckedIn && paymentStatus != PaymentStatus.Paid);
        }

        public static bool ShouldRefund(PaymentStatus paymentStatus, BookingStatus bookingStatus)
        {
            return (bookingStatus == BookingStatus.Canceled || bookingStatus == BookingStatus.NoShow) &&
                   (paymentStatus == PaymentStatus.Paid || paymentStatus == PaymentStatus.PartiallyPaid);
        }

        public static bool IsValidStateCombination(BookingStatus bookingStatus, PaymentStatus paymentStatus)
        {
            // Exemplo simples de validações combinadas
            if (bookingStatus == BookingStatus.CheckedIn && paymentStatus == PaymentStatus.Unpaid)
                return false;

            if (bookingStatus == BookingStatus.CheckedOut && paymentStatus == PaymentStatus.Unpaid)
                return false;

            if (bookingStatus == BookingStatus.Canceled && paymentStatus == PaymentStatus.Paid)
                return false; // Deveria estar como refunded

            return true;
        }
    }
}
