using FluentValidation;

namespace qrAPI.App.Domain.Validators
{
    public class QrValidator : AbstractValidator<Qr>
    {
        public QrValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
