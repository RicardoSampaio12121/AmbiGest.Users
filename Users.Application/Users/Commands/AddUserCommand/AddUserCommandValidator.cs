using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Users.Application.Users.Commands.AddUserCommand;
public class AddUserCommandValidator: AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(18)
            .MinimumLength(5);

        RuleFor(v => v.Surname)
            .NotEmpty()
            .MaximumLength(18)
            .MinimumLength(5);

        RuleFor(v => v.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email is invalid");


        RuleFor(v => v.BirthDate)
            .NotEmpty()
            .NotNull()
            .WithMessage("Birthdate is invalid");

    }
}
