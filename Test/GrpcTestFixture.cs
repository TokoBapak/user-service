namespace UserService.Tests;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UserService.Authentication;

public class GrpcTestFixture : IDisposable
{
    public TestServer Server { get; }
    public GrpcChannel Channel { get; }

    public GrpcTestFixture()
    {
        Server = new TestServer(new WebHostBuilder().ConfigureServices(services =>
        {
            services.AddGrpc();
            services.AddSingleton<AuthenticationService>();
            services.AddSingleton<LoginRequestValidator>();
            // Add other required services here.
        }).Configure(app =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<AuthenticationService>();
            });
        }));

        Channel = GrpcChannel.ForAddress(Server.BaseAddress, new GrpcChannelOptions { HttpClient = Server.CreateClient() });
    }

    public void Dispose()
    {
        Channel.Dispose();
        Server.Dispose();
        GC.SuppressFinalize(this);
    }
}