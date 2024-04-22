using Soliton.Shared;
using Soliton.CleanTemplate.Adapters.Mongo;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Soliton.CleanTemplate.Core;

namespace Soliton.CleanTemplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(
            options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy =
                    SnakeCaseNamingPolicy.Instance;
            }); ;
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            builder.Services.RegisterDataAdapter();
            builder.Services.AddTransient<ISolitonService, SolitonService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
