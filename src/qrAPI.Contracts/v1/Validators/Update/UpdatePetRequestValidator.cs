using FluentValidation;
using qrAPI.Contracts.v1.Requests.Update;
using System;

namespace qrAPI.Contracts.v1.Validators.Update
{
    public class UpdatePetRequestValidator : AbstractValidator<UpdatePetRequest>
    {
        public UpdatePetRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(UpdatePetRequest.MinNameLength)
                .MaximumLength(UpdatePetRequest.MaxNameLength);
            RuleFor(x => x.Birthdate)
                .NotNull()
                .NotEmpty()
                .LessThan(DateTime.Now)
                .WithMessage("The Pet cannot born in the future.")
                .GreaterThan(DateTime.Now.AddYears(-UpdatePetRequest.MaxAge))
                .WithMessage("The Pet cannot be so old.");
        }
    }
}
