using FluentValidation;
using PayCore.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Service.Validations
{
    public class OfferCreateDtoValidator : AbstractValidator<OfferDto>
    {
        public OfferCreateDtoValidator()
        {
            RuleFor(x => x.ProductId).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.UserAppId).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.BidPrice).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0 ");
        }
    }

    
}
