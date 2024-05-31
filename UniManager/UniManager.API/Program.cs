using UniManager.API;
using UniManager.API.Common.SwaggerConfigurations;
using UniManager.Application;
using UniManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddInfrastructure(builder.Configuration)
        .AddApplication();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.AddSwagger();
    app.Run();
}



