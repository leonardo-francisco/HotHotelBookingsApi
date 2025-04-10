using AutoMapper;
using HHB.Application.Contracts;
using HHB.Application.DTO;
using HHB.Domain.Contracts;
using HHB.Domain.Entities;
using HHB.Domain.Enums;
using HHB.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public BookingService(IBookingRepository bookingRepository, IHotelRepository hotelRepository, IRoomRepository roomRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<string> AddServiceToBooking(BookingDto dto)
        {
            if (string.IsNullOrEmpty(dto.Id))
                throw new InvalidOperationException("BookingId é obrigatório.");

            var booking = await _bookingRepository.GetByIdAsync(dto.Id);
            if (booking == null)
                throw new InvalidOperationException("Reserva não encontrada.");

            if (dto.AdditionalService == null || !dto.AdditionalService.Any())
                throw new InvalidOperationException("Nenhum serviço adicional fornecido.");

            var additional = _mapper.Map<List<Domain.Entities.AdditionalService>>(dto.AdditionalService);

            await UpdateBookingWithAdditionalServicesAsync(booking, additional);
            await UpdateCustomerBookingAsync(booking);         

            return null;
        }    

        public async Task DeleteAsync(string id)
        {
            await _bookingRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookingDto>> GetActiveBookingsAsync()
        {
            var bookings = await _bookingRepository.GetActiveBookingsAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<IEnumerable<BookingDto>> GetAllAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(string hotelId, DateTime checkIn, DateTime checkOut)
        {
            var hotel = await _hotelRepository.GetByIdAsync(hotelId);

            if (hotel == null) return Enumerable.Empty<Room>();

            var bookings = await _bookingRepository.GetByHotelIdAsync(hotelId);

            var availableRooms = hotel.Rooms
                .Where(room => RoomAvailabilityRules.IsRoomAvailable(room, bookings, checkIn, checkOut))
                .ToList();

            return availableRooms;
        }

        public async Task<IEnumerable<BookingDto>> GetByClientIdAsync(string clientId)
        {
            var bookings = await _bookingRepository.GetByClientIdAsync(clientId);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<IEnumerable<BookingDto>> GetByHotelIdAsync(string hotelId)
        {
            var bookings = await _bookingRepository.GetByHotelIdAsync(hotelId);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> GetByIdAsync(string id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<IEnumerable<BookingDto>> GetByRoomIdAsync(string roomId)
        {
            var bookings = await _bookingRepository.GetByRoomIdAsync(roomId);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> InsertAsync(BookingDto entity)
        {
            var bookingStatus = Enum.Parse<BookingStatus>(entity.Status);
            var paymentStatus = Enum.Parse<PaymentStatus>(entity.PaymentStatus);

            if (!BookingRules.IsValidStateCombination(bookingStatus, paymentStatus))
                throw new InvalidOperationException("A combinação de status de reserva e pagamento não é válida.");

            var booking = _mapper.Map<Booking>(entity);
            var result = await _bookingRepository.InsertAsync(booking);
           
            await UpdateAvailabilityRoom(entity.RoomId);
            await UpdateCustomerBookingAsync(result);            

            return _mapper.Map<BookingDto>(result);
        }

       

        public async Task<bool> IsAvailableAsync(string hotelId, string roomId, DateTime startDate, DateTime endDate)
        {
            var bookings = await _bookingRepository.GetByHotelAndRoomAsync(hotelId, roomId);

            return bookings.All(b =>
                b.Status == BookingStatus.Canceled || b.Status == BookingStatus.NoShow ||
                endDate <= b.CheckIn || startDate >= b.CheckOut);
        }

        public async Task<string> TryCancelAsync(string id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
                return "Reserva não encontrada.";

            var status = Enum.Parse<BookingStatus>(booking.Status.ToString());
            var payment = Enum.Parse<PaymentStatus>(booking.PaymentStatus.ToString());

            if (!BookingRules.CanCancel(status, payment))
                return "Esta reserva não pode ser cancelada.";

            booking.Status = BookingStatus.Canceled;
            await _bookingRepository.UpdateAsync(id, booking);

            await UpdateAvailabilityRoom(booking.RoomId);           

            return null;
        }

        public async Task<string> TryCheckInAsync(string id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
                return "Reserva não encontrada.";

            var status = Enum.Parse<BookingStatus>(booking.Status.ToString());
            var payment = Enum.Parse<PaymentStatus>(booking.PaymentStatus.ToString());

            if (!BookingRules.CanCheckIn(status, payment))
                return "Não é possível realizar o check-in nesta reserva.";

            booking.Status = BookingStatus.CheckedIn;
            await _bookingRepository.UpdateAsync(id, booking);

            return null;
        }

        public async Task<string> TryCheckOutAsync(string id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
                return "Reserva não encontrada.";

            var status = Enum.Parse<BookingStatus>(booking.Status.ToString());
            var payment = Enum.Parse<PaymentStatus>(booking.PaymentStatus.ToString());

            if (!BookingRules.CanCheckOut(status, payment))
                return "Não é possível realizar o check-out nesta reserva. Verifique o status da reserva e o pagamento.";

            booking.Status = BookingStatus.CheckedOut;
            await _bookingRepository.UpdateAsync(id, booking);

            await UpdateAvailabilityRoom(booking.RoomId);

            return null;
        }

        public async Task<string> TryRefundAsync(string id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
                return "Reserva não encontrada.";

            var status = Enum.Parse<BookingStatus>(booking.Status.ToString());
            var payment = Enum.Parse<PaymentStatus>(booking.PaymentStatus.ToString());

            if (!BookingRules.ShouldRefund(payment, status))
                return "Reembolso não aplicável para esta reserva.";

            booking.PaymentStatus = PaymentStatus.Refunded;
            await _bookingRepository.UpdateAsync(id, booking);

            return null;
        }

        public async Task<BookingDto> UpdateAsync(string id, BookingDto entity)
        {
            var bookingStatus = Enum.Parse<BookingStatus>(entity.Status);
            var paymentStatus = Enum.Parse<PaymentStatus>(entity.PaymentStatus);

            if (!BookingRules.IsValidStateCombination(bookingStatus, paymentStatus))
                throw new InvalidOperationException("A combinação de status de reserva e pagamento não é válida.");

            var booking = _mapper.Map<Booking>(entity);
            var result = await _bookingRepository.UpdateAsync(id, booking);

            return _mapper.Map<BookingDto>(result);
        }

        #region Private Methods
        private async Task UpdateBookingWithAdditionalServicesAsync(Booking booking, List<Domain.Entities.AdditionalService> additional)
        {
            booking.AdditionalService ??= new List<Domain.Entities.AdditionalService>();
            booking.AdditionalService.AddRange(additional);

            await _bookingRepository.UpdateAsync(booking.Id.ToString()!, booking);
        }

        private async Task UpdateCustomerBookingAsync(Booking booking)
        {
            var client = await _customerRepository.GetByIdAsync(booking.ClientId);          
            client.Bookings.Add(booking);

            await _customerRepository.UpdateAsync(client.Id.ToString(), client);
        }

        private async Task UpdateAvailabilityRoom(string roomId)
        {
            var room = await _roomRepository.GetByIdAsync(roomId);
            room.IsAvailable = false;

            await _roomRepository.UpdateAsync(room.Id.ToString(), room);
        }
        #endregion
    }
}
