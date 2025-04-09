using FluentValidation;
using HHB.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Validations
{
    public class RoomValidator : AbstractValidator<RoomDto>
    {
        public RoomValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do quarto é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do quarto deve ter no máximo 100 caracteres.");

            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("O ID do hotel é obrigatório.");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("O número do quarto é obrigatório.")
                .MaximumLength(10).WithMessage("O número do quarto deve ter no máximo 10 caracteres.");

            RuleFor(x => x.RoomType)
                .NotEmpty().WithMessage("O tipo do quarto é obrigatório.")
                .MaximumLength(50).WithMessage("O tipo do quarto deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A descrição do quarto é obrigatória.")
                .MaximumLength(500).WithMessage("A descrição deve ter no máximo 500 caracteres.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("A capacidade deve ser maior que zero.");

            RuleFor(x => x.PricePerNight)
                .GreaterThan(0).WithMessage("O preço por noite deve ser maior que zero.");
        }
    }
}
