using Microsoft.Extensions.Configuration;
using UserService.Infrastructures;

namespace UserService.Tests;

public class SettingsTests
{
    [Fact]
    public void MyAppSettings_LoadsCorrectValues()
    {
        // Arrange
        var rootDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(rootDir)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddKeyPerFile(Path.Combine(rootDir,"Properties/Secrets"), optional: false, reloadOnChange: true);
        var configuration = configurationBuilder.Build();
        var connStr = new ConnectionStrings();

        // Act
        configuration.GetSection(nameof(ConnectionStrings)).Bind(connStr);

        // Assert
        var content = File.ReadLines(Path.Combine(rootDir, "Properties/Secrets/ConnectionStrings__PostgreSql")).First();
            /*.Skip(2)
            .First()
            .Split(":")
            .Last()
            .TrimStart().TrimEnd();*/
        Assert.Equal(content, connStr.PostgreSql);
    }
}
