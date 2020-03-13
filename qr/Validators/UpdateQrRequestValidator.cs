using FluentValidation;
using qr.Contracts.v1.Requests;

namespace qr.Validators
{
    public class UpdateQrRequestValidator : AbstractValidator<UpdateQrRequest>
    {
        public UpdateQrRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
