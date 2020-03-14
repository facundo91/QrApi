using FluentValidation;
using qrAPI.Contracts.v1.Requests;

namespace qrAPI.Validators
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