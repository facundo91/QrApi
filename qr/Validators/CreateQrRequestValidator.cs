using FluentValidation;
using qrAPI.Contracts.v1.Requests;

namespace qrAPI.Validators
{
    public class CreateQrRequestValidator : AbstractValidator<CreateQrRequest>
    {
        public CreateQrRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}