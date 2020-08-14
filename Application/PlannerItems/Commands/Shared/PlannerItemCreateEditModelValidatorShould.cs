using AutoFixture;
using FluentValidation.TestHelper;
using Shared.TestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.PlannerItems.Commands.Shared
{
    public class PlannerItemCreateEditModelValidatorShould
    {
        private readonly PlannerItemCreateEditModelValidator sut;
        private readonly Fixture fixture;

        private readonly PlannerItemCreateEditModel testModel;

        public PlannerItemCreateEditModelValidatorShould()
        {
            fixture = TestFixture.Create();
            sut = fixture.Create<PlannerItemCreateEditModelValidator>();
            testModel = fixture.Create<PlannerItemCreateEditModel>();
        }

        [Fact]
        public void ReturnErrorForEmptyDescription()
        {
            // Arrange
            testModel.Description = string.Empty;
            // Act
            var result = sut.TestValidate(testModel);
            // Assert
            result.ShouldHaveValidationErrorFor(m => m.Description);
        }

        [Fact]
        public void ShouldHaveValidationErrorForNullPlannedActionDate()
        {
            // Arrange
            testModel.PlannedActionDate = null;
            // Act
            var result = sut.TestValidate(testModel);
            // Assert
            result.ShouldHaveValidationErrorFor(m => m.PlannedActionDate);
        }


        [Fact]
        public void ShouldHaveValidationErrorForNullPlannedEndTime()
        {
            // Arrange
            testModel.PlannedEndTime = null;
            // Act
            var result = sut.TestValidate(testModel);
            // Assert
            result.ShouldHaveValidationErrorFor(m => m.PlannedEndTime);
        }

        [Fact]
        public void ShouldHaveValidationErrorForPlannedEndTimeIfBeforePlannedStartTime()
        {
            // Arrange
            var startDate = fixture.Create<DateTime>();
            testModel.PlannedActionDate = startDate;
            testModel.PlannedEndTime = startDate.AddMinutes(-1);
            // Act
            var result = sut.TestValidate(testModel);
            // Assert
            result.ShouldHaveValidationErrorFor(m => m.PlannedEndTime);
        }

        [Fact]
        public void ShouldNotHaveAnyValidationErrors()
        {
            // Arrange
            var startDate = fixture.Create<DateTime>();
            testModel.PlannedActionDate = startDate;
            testModel.PlannedEndTime = startDate.AddMinutes(1);
            // Act
            var result = sut.TestValidate(testModel);
            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
