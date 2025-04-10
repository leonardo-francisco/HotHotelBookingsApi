using FluentValidation;
using HHB.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Validations
{
    public class BookingValidator : AbstractValidator<BookingDto>
    {
        public BookingValidator()
        {
            RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("O ID do cliente deve ser maior que zero.");

            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("O ID do hotel é obrigatório.");

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("O ID do quarto é obrigatório.");

            RuleFor(x => x.CheckIn)
                .NotEmpty().WithMessage("A data de check-in é obrigatória.")
                .Must(checkIn => checkIn.Date >= DateTime.UtcNow.Date)
                .WithMessage("A data de check-in deve ser hoje ou uma data futura.");

            RuleFor(x => x.CheckOut)
                .NotEmpty().WithMessage("A data de check-out é obrigatória.")
                .GreaterThan(x => x.CheckIn)
                .WithMessage("A data de check-out deve ser posterior à data de check-in.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("O status da reserva é obrigatório.");

            RuleFor(x => x.PaymentStatus)
                .NotEmpty().WithMessage("O status do pagamento é obrigatório.");
        }
    }
}
