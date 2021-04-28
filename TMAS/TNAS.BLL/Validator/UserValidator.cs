using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.DTO;
using TMAS.DAL.DTO.Created;

namespace TMAS.BLL.Validator
{
    public class UserValidator:AbstractValidator<UserCreatedDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30)
                .WithMessage("Incorrect Name");

            RuleFor(x => x.LastName).NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30)
                .WithMessage("Incorrect LastNameName");

            RuleFor(x => x.UserName).NotEmpty()
                .WithMessage("Empty Username");

            RuleFor(x => x.Email).NotEmpty()
                .EmailAddress()
                .WithMessage("Bad email");

            RuleFor(x => x.Password).NotEmpty()
                .MinimumLength(6)
                .WithMessage("Passwords must be at least 6 characters");

            RuleFor(x => x.Password).NotEmpty()
                .Matches(@".*\d{1}")
                .WithMessage("Password should contain at least 1 digit");

            RuleFor(x => x.Password).NotEmpty()
                .Matches(@".*a-z{1}")
                .WithMessage("Passwords must have at least one lowercase ('a'-'z')");

            RuleFor(x => x.Password).NotEmpty()
                .Matches(@".*A-Z{1}")
                .WithMessage("Passwords must have at least one uppercase ('A'-'Z')");

            //RuleFor(x => x.Password).NotEmpty()
            //    .Matches(@"[\.\!\@\#\$\%\^\&\_\/]")
            //    .WithMessage("Passwords must have at least one non alphanumeric character ");
        }
    }
}
