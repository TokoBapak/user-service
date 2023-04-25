using System.Collections.Immutable;
using FluentValidation;
using FluentValidation.Results;
using TokoBapak.Protobuf.AuthenticationSchema;

namespace UserService.Authentication;

public class LoginRequestValidator: AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}

public static class ValidationResultExtensions
{
    public static ImmutableList<LoginErrorResponse.Types.ErrorDescriptor> ToLoginResponse(this ValidationResult result)
    {
        var errorDescriptors = result.Errors.Select(e => new LoginErrorResponse.Types.ErrorDescriptor
        {
            Message = e.ErrorMessage,
            Field = e.PropertyName,
            Rule = e.ErrorCode
        });
        return errorDescriptors.ToImmutableList();
    }
}
