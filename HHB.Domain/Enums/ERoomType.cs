using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Enums
{
    public enum RoomType
    {
        Single = 0,        // One single bed, for one person
        Double = 1,        // One double bed, for two people
        Twin = 2,          // Two single beds, for two people
        Triple = 3,        // Three single beds or a combination for three guests
        Suite = 4,         // Larger room, may include a living area and luxury amenities
        Family = 5,        // Room designed for families (multiple beds, more space)
        Deluxe = 6,        // Higher-end room with premium features
        Studio = 7,        // Open space with bed, kitchenette, and sometimes a sofa
        Apartment = 8,     // Self-contained unit with kitchen, bathroom, living room
        Penthouse = 9      // Top-floor luxury unit, often with great views and premium features
    }
}
