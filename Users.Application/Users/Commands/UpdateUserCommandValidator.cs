using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Users.Application.Users.Commands;
public class UpdateUserCommandValidator: AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("Email is invalid.");

        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is invalid.");

        RuleFor(v => v.Surname)
            .NotEmpty()
            .NotNull()
            .WithMessage("Surname is invalid.");

        RuleFor(v => v.BirthDate)
            .NotEmpty()
            .NotNull()
            .WithMessage("Birthdate is invalid.");
    }
}
