﻿using FluentValidation;
using Para.Data.Domain;

namespace Para.Business.Validatiors
{
    public class CustomerValidator : AbstractValidator<Customer>
    {

        public CustomerValidator()
        {
            RuleFor(x => x.InsertUser).NotEmpty().MaximumLength(50);
            RuleFor(x => x.InsertDate).NotEmpty();
            RuleFor(x => x.IsActive).NotNull();
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.IdentityNumber).NotEmpty().Length(11).Must(BeNumeric).WithMessage("Kimlik numarası sadece rakamlardan oluşmalıdır.");
            RuleFor(x => x.Email).NotEmpty().MaximumLength(100).EmailAddress();
            RuleFor(x => x.CustomerNumber).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.Now);
        }
        // custom bi validasyon gerekli tüm karakterler rakam olması için.
        private bool BeNumeric(string value)
        {
            return value.All(char.IsDigit);
        }
    }
}
