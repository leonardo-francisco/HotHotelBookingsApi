using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Enums
{
    public enum BookingStatus
    {
        Pending = 0,        // Reservation created, awaiting confirmation/payment
        Confirmed = 1,      // Reservation confirmed (paid or guaranteed)
        CheckedIn = 2,      // Guest has checked in
        InProgress = 3,     // Guest is currently staying (optional, can be same as CheckedIn)
        CheckedOut = 4,     // Guest has checked out
        Canceled = 5,       // Reservation was canceled before check-in
        NoShow = 6,         // Guest did not show up
        Refunded = 7        // Reservation was refunded (fully or partially)
    }
}
