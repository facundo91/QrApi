using System;
using FluentValidation;

namespace qrAPI.Logic.Domain.Validators
{
    public class PetValidator : AbstractValidator<Pet>
    {
        public PetValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(Pet.MinNameLength)
                .MaximumLength(Pet.MaxNameLength);
            RuleFor(x => x.Birthdate)
                .NotNull()
                .NotEmpty()
                .LessThan(DateTime.Now)
                .WithMessage("The Pet cannot born in the future.")
                .GreaterThan(DateTime.Now.AddYears(-Pet.MaxAge))
                .WithMessage("The Pet cannot be so old.");
        }
    }
}
