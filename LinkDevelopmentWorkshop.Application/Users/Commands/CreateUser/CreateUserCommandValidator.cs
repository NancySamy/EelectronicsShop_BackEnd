using FluentValidation;
using LinkDevelopmentWorkshop.Application.Localization;
using LinkDevelopmentWorkshop.Application.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(Resources.UserNameIsRequired);

              RuleFor(x => x.BirthDate)
             .NotEmpty()
             .WithMessage(Resources.BirthDateIsRequired);

            RuleFor(x => x.Address)
             .NotEmpty()
             .WithMessage(Resources.AddressIsRequired);

            RuleFor(x => x.PhoneNumber)
           .NotEmpty()
           .WithMessage(Resources.MobileNumberIsRequired);

         

            RuleFor(x => x.Email)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotEmpty()
               .WithMessage(Resources.EmailAddressIsRequird)
               .Must(x=> Utilities.IsValidEmail(x))
               .WithMessage(Resources.InValidEmailAddress);



        }
    }
}
