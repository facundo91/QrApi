using FluentValidation;
using qrAPI.Contracts.v1.Requests.Create;

namespace qrAPI.Presentation.Validators
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