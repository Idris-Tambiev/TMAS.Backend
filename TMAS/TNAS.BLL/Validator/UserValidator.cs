using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.DTO;

namespace TMAS.BLL.Validator
{
    public class UserValidator:AbstractValidator<RegistrateUserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Lastname).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        }
    }
}
