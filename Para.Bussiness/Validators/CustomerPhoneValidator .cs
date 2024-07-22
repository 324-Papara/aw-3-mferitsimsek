﻿using FluentValidation;
using Para.Data.Domain;

namespace Para.Business.Validatiors
{
    public class CustomerPhoneValidator : AbstractValidator<CustomerPhone>
    {
        public CustomerPhoneValidator()
        {
            RuleFor(x => x.InsertUser).NotEmpty().MaximumLength(50);
            RuleFor(x => x.InsertDate).NotEmpty();
            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.CountyCode).NotEmpty().MaximumLength(3);
            RuleFor(x => x.Phone).NotEmpty().MaximumLength(10).Must(BeNumeric).WithMessage("Telefon numarası sadece rakamlardan oluşmalıdır.");
            RuleFor(x => x.IsDefault).NotNull();
        }

        private bool BeNumeric(string value)
        {
            return value.All(char.IsDigit);
        }
    }
}
