﻿using Application.PlannerItems.Commands.Shared;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace PlannerApp.UnitTests.Validators
{
    public class PlannerItemDTOValidatorShould
    {
        private readonly PlannerItemCreateEditModelValidator sut;
        private readonly PlannerItemCreateEditModel testDTO;
        public PlannerItemDTOValidatorShould()
        {
            sut = new PlannerItemCreateEditModelValidator();
            testDTO = new PlannerItemCreateEditModel
            {
                Description = "desc",
                ID = 1,
                PlannedActionDate = new DateTime(2019, 2, 2, 2, 20, 0),
                PlannedEndTime = new DateTime(2019, 2, 2, 3, 20, 0),
            };
        }

        [Fact]
        public void NotAddValidationErrors()
        {
            // Model above is valid

            // Act
            var result = sut.TestValidate(testDTO);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void AddErrorIfActionDateNull()
        {
            // Arrange
            testDTO.PlannedActionDate = null;

            // Act
            var result = sut.TestValidate(testDTO);

            // Assert
            result.ShouldHaveValidationErrorFor(m => m.PlannedActionDate);
        }

        [Fact]
        public void AddErrorIfEndDateNull()
        {
            // Arrange
            testDTO.PlannedEndTime = null;

            // Act
            var result = sut.TestValidate(testDTO);

            // Assert
            result.ShouldHaveValidationErrorFor(m => m.PlannedEndTime);
        }

        [Fact]
        public void AddErrorMessageWhenEndDateIsBeforeStartDate()
        {
            // Arrange
            testDTO.PlannedEndTime = new DateTime(2019, 2, 2, 2, 19, 0);

            // Act
            var result = sut.TestValidate(testDTO);

            // Assert
            result.ShouldHaveValidationErrorFor(i => i.PlannedEndTime);
        }

        [Fact]
        public void AddErrorIfNoDescription()
        {
            // Arrange
            testDTO.Description = string.Empty;

            // Act
            var result = sut.TestValidate(testDTO);

            // Assert
            result.ShouldHaveValidationErrorFor(i => i.Description);
        }
    }
}
