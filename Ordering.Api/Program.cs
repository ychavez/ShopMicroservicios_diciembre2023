using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ordering.Api.Events;
using Ordering.Application;
using Ordering.Application.Contracts;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;
using System.Text;

namespace Ordering.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Tienda en linea DW", Version = "V1" });

                x.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
                {
                    Description = @"Coloca aqui tu token usando el esquema Bearer por ejemplo Bearer asdiajshdfklasjhfklaj",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "JWT"
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                     new OpenApiSecurityScheme
                     {
                        Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "JWT",

                     },

                     Scheme = "oauth2",
                     Name = "JWT",
                     In = ParameterLocation.Header

                     },
                     new List<string>()
                    }
                });
            });


            builder.Services.AddApplicationServiceRegistration();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddDbContext<OrderContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("OrderingConnection")));

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<CheckoutEventConsumer>();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(builder.Configuration["EventBusSettings:HostAddres"]);

                    cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue,
                        x => x.ConfigureConsumer<CheckoutEventConsumer>(ctx));
                });
            });

            builder.Services.AddAuthentication(x =>
            {
                //Microsoft.AspNetCore.Authentication.JwtBearer;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {

                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey
                  (Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Identity:Key")!)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddScoped<CheckoutEventConsumer>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
