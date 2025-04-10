using FluentValidation;
using HHB.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Validations
{
    public class AdditionalServiceValidator : AbstractValidator<AdditionalServiceDto>
    {
        public AdditionalServiceValidator()
        {
            RuleFor(x => x.HotelId)
            .NotEmpty().WithMessage("O ID do hotel é obrigatório.");

            RuleFor(x => x.ServiceName)
                .NotEmpty().WithMessage("O nome do serviço é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do serviço deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A descrição do serviço é obrigatória.")
                .MaximumLength(250).WithMessage("A descrição deve ter no máximo 250 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("O preço deve ser maior ou igual a zero.");

            RuleFor(x => x.IsAvailable)
                .NotNull().WithMessage("É necessário informar se o serviço está disponível.");
        }
    }
}
