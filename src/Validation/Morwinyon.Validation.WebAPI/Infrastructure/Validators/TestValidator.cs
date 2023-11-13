﻿using FluentValidation;
using Morwinyon.Validation.WebAPI.Infrastructure.Models;

namespace Morwinyon.Validation.WebAPI.Infrastructure.Validators;

public class TestValidator : AbstractValidator<TestModel>
{
    public TestValidator()
    {
        RuleFor(i => i.Id).GreaterThan(0).WithMessage("{PropertyName} cannot be zero!");
        RuleFor(i => i.Name).MinimumLength(3).WithMessage("{PropertyName} must be at least {MinLength} character");
    }
}
