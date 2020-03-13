using FluentValidation;
using qr.Contracts.v1.Requests;

namespace qr.Validators
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
