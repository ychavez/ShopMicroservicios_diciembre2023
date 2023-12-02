
using DependencyInjection.Api.Contracts;

namespace DependencyInjection.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IIDScoped, ID>();
            builder.Services.AddTransient<IIDTransient, ID>();
            builder.Services.AddSingleton<IIDSingleton, ID>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/test", (IIDSingleton singleton, IIDScoped scoped,
                IIDScoped scoped2, IIDTransient transient, IIDTransient transient2)
                =>
            {
                return $"Singleton instance {singleton.Value} \n Scoped instance1 {scoped.Value} \n" +
                    $"Scoped instance2 {scoped2.Value} \n Transient instance 1 {transient.Value}  \n " +
                    $" Transient instance 2 {transient2.Value}";
            });

            app.Run();
        }
    }
}
