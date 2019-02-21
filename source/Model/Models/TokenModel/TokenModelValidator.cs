using DotNetCore.Validation;
using FluentValidation;

namespace DotNetCoreArchitecture.Model
{
    public sealed class TokenModelValidator : Validator<TokenModel>
    {
        public TokenModelValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Token).NotNull().NotEmpty();
        }
    }
}
