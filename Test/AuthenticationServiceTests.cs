using TokoBapak.Protobuf.AuthenticationSchema;

namespace UserService.Tests;
using UserService;

public class AuthenticationServiceTests : IClassFixture<GrpcTestFixture>
{
    private readonly GrpcTestFixture _fixture;

    public AuthenticationServiceTests(GrpcTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory]
    [InlineData("gagal", "gagal", LoginResponse.ResponseOneofCase.LoginErrorResponse)]
    [InlineData("oke@oke.com", "berhasil", LoginResponse.ResponseOneofCase.TokenSetReply)]
    public async Task AuthenticateUserTest(string email, string password, LoginResponse.ResponseOneofCase expectedResponseCase)
    {
        // Arrange
        var client = new TokoBapak.Protobuf.AuthenticationSchema.Authentication.AuthenticationClient(_fixture.Channel);

        // Act
        var response = await client.LoginAsync(new LoginRequest
        {
            Email = email,
            Password = password
        });

        // Assert
        Assert.NotNull(response);
        Assert.Equal(expectedResponseCase, response.ResponseCase);
    }
}