﻿using FluentValidation;
using Patlus.Common.UseCase;
using Patlus.Common.UseCase.Validators;

namespace Patlus.IdentityManagement.UseCase.Features.Identities.UpdateActiveStatus
{
    public class UpdateActiveStatusCommandValidator : AbstractValidator<UpdateActiveStatusCommand>, IFeatureValidator<UpdateActiveStatusCommand>
    {
        public UpdateActiveStatusCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty().WithErrorCode(ValidationErrorCodes.NotEmpty);

            RuleFor(r => r.Active)
                .NotEmpty().WithErrorCode(ValidationErrorCodes.NotEmpty);

            RuleFor(r => r.RequestorId)
                .NotEmpty().WithErrorCode(ValidationErrorCodes.NotEmpty);
        }
    }
}
