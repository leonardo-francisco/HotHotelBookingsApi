using FluentValidation;
using HHB.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Validations
{
    public class AddressValidator : AbstractValidator<AddressDto>
    {
        public AddressValidator()
        {
            RuleFor(e => e.ZipCode)
                .NotEmpty().WithMessage("O ZipCode é obrigatório.")
                .Matches(@"^\d{8}$").WithMessage("O ZipCode deve conter exatamente 8 dígitos numéricos.");

            RuleFor(e => e.Street)
                .NotEmpty().WithMessage("O Street é obrigatório.");

            RuleFor(e => e.Number)
                .NotEmpty().WithMessage("O número é obrigatório.");

            RuleFor(e => e.Neighborhood)
                .NotEmpty().WithMessage("O Neighborhood é obrigatório.");

            RuleFor(e => e.City)
                .NotEmpty().WithMessage("A City é obrigatória.");

            RuleFor(e => e.State)
                .NotEmpty().WithMessage("O State é obrigatório.")
                .Length(2).WithMessage("O State deve ter exatamente 2 caracteres.");
        }
    }
}
