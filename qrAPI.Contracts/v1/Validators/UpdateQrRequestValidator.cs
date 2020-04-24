using FluentValidation;
using qrAPI.Contracts.v1.Requests.Update;

namespace qrAPI.Contracts.v1.Validators
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