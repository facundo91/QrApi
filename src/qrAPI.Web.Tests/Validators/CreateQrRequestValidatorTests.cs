using FluentValidation.TestHelper;
using qrAPI.Contracts.v1.Validators.Create;
using System;
using Xunit;

namespace qrAPI.Web.Tests.Validators
{
    public class CreateQrRequestValidatorTests : CreateQrRequestValidatorFixture
    {
        [Fact]
        public void Should_have_error_when_Name_is_null()
        {
            CreateQrRequestValidator.ShouldHaveValidationErrorFor(createQrRequest => createQrRequest.Name, null as string);
        }

        [Fact]
        public void Should_not_have_error_when_name_is_specified()
        {
            CreateQrRequestValidator.ShouldNotHaveValidationErrorFor(createQrRequest => createQrRequest.Name, "Jeremy");
        }
    }

    public class CreateQrRequestValidatorFixture : IDisposable
    {
        protected readonly CreateQrRequestValidator CreateQrRequestValidator;

        protected CreateQrRequestValidatorFixture()
        {
            CreateQrRequestValidator = new CreateQrRequestValidator();
        }

        public void Dispose()
        {
        }
    }
}
