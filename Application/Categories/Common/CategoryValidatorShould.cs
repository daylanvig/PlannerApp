using Application.Categories.Queries.Common;
using AutoFixture;
using FluentValidation.TestHelper;
using Shared.TestSupport;
using Xunit;

namespace Application.Categories.Common
{
    public class CategoryValidatorShould
    {
        private readonly CategoryValidator sut;
        private readonly Fixture fixture;

        private readonly CategoryModel testModel;

        public CategoryValidatorShould()
        {
            fixture = TestFixture.Create();
            testModel = fixture.Create<CategoryModel>();
            sut = fixture.Create<CategoryValidator>();
        }

        [Fact]
        public void NotHaveAnyErrors()
        {
            sut.TestValidate(testModel).ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void HaveErrorForColour()
        {
            // Arrange
            testModel.Colour = string.Empty;
            // Act
            var result = sut.TestValidate(testModel);
            // Assert
            result.ShouldHaveValidationErrorFor(m => m.Colour);
        }

        [Fact]
        public void HaveErrorForDescription()
        {
            // Arrange
            testModel.Description = string.Empty;
            // Act
            var result = sut.TestValidate(testModel);
            // Assert
            result.ShouldHaveValidationErrorFor(m => m.Description);
        }
    }
}
