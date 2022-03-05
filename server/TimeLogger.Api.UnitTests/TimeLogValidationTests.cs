using FluentValidation.TestHelper;
using Timelogger.Api.Features.TimeLogs;
using Xunit;

namespace TimeLogger.Api.UnitTests
{
    public class TimeLogValidationTests
    {
        private readonly CreateTimeLogCommandValidator _validator = new CreateTimeLogCommandValidator();

        [Fact]
        public void Should_have_error_when_Description_is_null()
        {
            var model = new CreateTimeLogCommand { Description = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(CreateTimeLogCommand => CreateTimeLogCommand.Description);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(29)]
        public void Should_have_error_when_Duration_is_less_than_30(int duration)
        {
            var model = new CreateTimeLogCommand { Duration = duration, Description = "Testing", ProjectId = 1 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(CreateTimeLogCommand => CreateTimeLogCommand.Duration);
        }
    }
}
