using HHB.Domain.Entities;
using HHB.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Rules
{
    public static class RoomAvailabilityRules
    {
        public static bool IsRoomAvailable(Room room, IEnumerable<Booking> existingBookings, DateTime checkIn, DateTime checkOut)
        {
            // Considera indisponível se a flag já estiver false
            if (!room.IsAvailable) return false;

            // Verifica conflitos de datas com outras reservas ativas
            return !existingBookings.Any(b =>
                b.RoomId == room.Id.ToString() &&
                b.Status != BookingStatus.Canceled &&
                checkIn < b.CheckOut &&
                checkOut > b.CheckIn
            );
        }
    }
}
