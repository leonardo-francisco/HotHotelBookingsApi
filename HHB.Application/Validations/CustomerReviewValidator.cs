using FluentValidation;
using HHB.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Application.Validations
{
    public class CustomerReviewValidator : AbstractValidator<CustomerReviewDto>
    {
        public CustomerReviewValidator()
        {
            RuleFor(x => x.HotelId)
            .NotEmpty().WithMessage("ID do hotel é obrigatório.");

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("ID do quarto é obrigatório.");

            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Nome do cliente é obrigatório.")
                .MaximumLength(100).WithMessage("Nome do cliente não pode exceder 100 caracteres.");

            RuleFor(x => x.ReviewDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Data de avalação não pode ser no futuro.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Nota deve ser de 1 a 5.");

            RuleFor(x => x.ServiceRating)
                .InclusiveBetween(1, 5).WithMessage("Nota de serviço deve ser de 1 a 5.");

            RuleFor(x => x.CleanlinessRating)
                .InclusiveBetween(1, 5).WithMessage("Nota de limpeza deve ser de 1 a 5.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Titulo da avaliação é obrigatório.")
                .MaximumLength(100).WithMessage("Titulo não pode exceder 100 caracteres.");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Comentário é obrigatório.")
                .MaximumLength(1000).WithMessage("Comentário não pode exceder 100 caracteres.");
        }
    }
}
