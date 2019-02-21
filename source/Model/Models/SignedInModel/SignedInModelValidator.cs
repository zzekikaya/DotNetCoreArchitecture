using DotNetCore.Validation;
using DotNetCoreArchitecture.CrossCutting.Resources;
using FluentValidation;

namespace DotNetCoreArchitecture.Model
{
    public sealed class SignedInModelValidator : Validator<SignedInModel>
    {
        public SignedInModelValidator() : base(Texts.AuthenticationInvalid)
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.UserId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Roles).NotNull().NotEmpty().NotEqual(Roles.None);
        }
    }
}
