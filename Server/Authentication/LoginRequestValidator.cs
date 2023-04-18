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
    public static LoginResponse ToLoginResponse(this ValidationResult result, int attemptsRemaining)
    {
        var errorDescriptors = result.Errors.Select(e => new LoginErrorResponse.Types.ErrorDescriptor
        {
            Message = e.ErrorMessage,
            Field = e.PropertyName,
            Rule = e.ErrorCode
        });

        var errorResponse = new LoginErrorResponse
        {
            Descriptors = { errorDescriptors },
            AttemptsRemaining = attemptsRemaining
        };

        var response = new LoginResponse { LoginErrorResponse = errorResponse };
        return response;
    }
}
