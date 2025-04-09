using FluentValidation;
using HHB.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Validations
{
    public class HotelValidator : AbstractValidator<HotelDto>
    {
        public HotelValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do hotel é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do hotel deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail informado não é válido.");

            RuleFor(x => x.Address)
                .NotNull().WithMessage("O endereço é obrigatório.")
                .SetValidator(new AddressValidator());

            RuleFor(x => x.FoundedYear)
                .GreaterThan(1800).WithMessage("O ano de fundação deve ser maior que 1800.")
                .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage("O ano de fundação não pode ser no futuro.");

            When(x => x.ClosedYear.HasValue, () =>
            {
                RuleFor(x => x.ClosedYear.Value)
                    .GreaterThan(x => x.FoundedYear).WithMessage("O ano de fechamento deve ser maior que o ano de fundação.")
                    .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage("O ano de fechamento não pode ser no futuro.");
            });

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .MaximumLength(1000).WithMessage("A descrição deve ter no máximo 1000 caracteres.");

            
        }
    }
}
