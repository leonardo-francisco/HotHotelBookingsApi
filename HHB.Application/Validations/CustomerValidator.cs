using FluentValidation;
using HHB.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Validations
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome do cliente é obrigatório.")
            .MaximumLength(100).WithMessage("Nome do cliente não pode exceder 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Formato de email inválido.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefone é obrigatório.")
                .Matches(@"^\+?\d{7,15}$").WithMessage("Número de telefone inválido. Deve conter apenas digitos e opcionalmente começar com '+'.");
        }
    }
}
