﻿using FluentValidation;
using Patlus.Common.UseCase;
using Patlus.Common.UseCase.Validators;
using Patlus.IdentityManagement.UseCase.Services;

namespace Patlus.IdentityManagement.UseCase.Features.Identities.CreateHosted
{
    public class CreateHostedCommandValidator : AbstractValidator<CreateHostedCommand>, IFeatureValidator<CreateHostedCommand>
    {
        public CreateHostedCommandValidator(IMasterDbContext dbService)
        {
            RuleFor(r => r.PoolId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithErrorCode(ValidationErrorCodes.NotEmpty)
                .Exist(dbService.Pools, (value) => x => x.Id == value && x.Archived == false).WithErrorCode(ValidationErrorCodes.Exist);

            RuleFor(r => r.Active)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithErrorCode(ValidationErrorCodes.NotEmpty);

            RuleFor(r => r.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithErrorCode(ValidationErrorCodes.NotEmpty)
                .MinimumLength(4).WithErrorCode(ValidationErrorCodes.MinLength);

            RuleFor(r => r.AccountName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithErrorCode(ValidationErrorCodes.NotEmpty)
                .MinimumLength(4).WithErrorCode(ValidationErrorCodes.MinLength)
                .MaximumLength(10).WithErrorCode(ValidationErrorCodes.MaxLength)
                .Matches(@"^[a-zA-Z][a-zA-Z0-9]*$").WithErrorCode(ValidationErrorCodes.PatternMatch)
                .Unique(dbService.HostedAccounts, (value) => (x => x.Name == value)).WithErrorCode(ValidationErrorCodes.Unique);

            RuleFor(r => r.AccountPassword)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithErrorCode(ValidationErrorCodes.NotEmpty)
                .MinimumLength(6).WithErrorCode(ValidationErrorCodes.MinLength)
                .MaximumLength(20).WithErrorCode(ValidationErrorCodes.MaxLength);

            RuleFor(r => r.RequestorId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithErrorCode(ValidationErrorCodes.NotEmpty);
        }
    }
}
