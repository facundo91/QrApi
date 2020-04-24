using FluentValidation;
using qrAPI.Contracts.v1.Requests.Create;

namespace qrAPI.Contracts.v1.Validators
{
    public class CreateQrRequestValidator : AbstractValidator<CreateQrRequest>
    {
        public CreateQrRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);
            RuleFor(x => x.PetId)
                .NotNull();
        }
    }
}