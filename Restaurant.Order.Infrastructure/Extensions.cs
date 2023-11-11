using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Restaurant.Order.Application;
using Restaurant.Order.Application.Services;
using Restaurant.Order.Domain.Repositories;
using Restaurant.Order.Infrastructure.BackgroundJobs;
using Restaurant.Order.Infrastructure.EF;
using Restaurant.Order.Infrastructure.EF.Contexts;
using Restaurant.Order.Infrastructure.EF.Repository;
using Restaurant.Order.Infrastructure.MassTransit;
using Restaurant.Order.Infrastructure.Security;
using Restaurant.Order.Infrastructure.Services;
using Restaurant.SharedKernel.Core;
using System.Reflection;
using System.Text;

namespace Restaurant.Order.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration, bool isDevelopment)
    {
        services.AddApplication();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<IOutboxService, OutboxService>();

        AddDatabase(services, configuration, isDevelopment);

        AddAuthentication(services, configuration);

        AddMassTransitWithRabbitMq(services, configuration);

        AddQuartz(services);

        return services;
    }

    private static void AddDatabase(IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        var connectionString =
                configuration.GetConnectionString("MenuDbConnectionString");
        services.AddDbContext<PersistenceDbContext>(context =>
                context.UseSqlServer(connectionString));
        services.AddDbContext<DomainDbContext>(context =>
            context.UseSqlServer(connectionString));

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        using var scope = services.BuildServiceProvider().CreateScope();
        if (!isDevelopment)
        {
            var context = scope.ServiceProvider.GetRequiredService<PersistenceDbContext>();
            context.Database.Migrate();
        }
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        JwtOptions jwtoptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtOptions =>
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.SecretKey));
            jwtOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = jwtoptions.ValidateIssuer,
                ValidateAudience = jwtoptions.ValidateAudience,
                ValidIssuer = jwtoptions.ValidIssuer,
                ValidAudience = jwtoptions.ValidAudience
            };
        });
    }

    private static IServiceCollection AddMassTransitWithRabbitMq(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBusService, MassTransitBusService>();

        var serviceName = configuration.GetValue<string>("ServiceName");
        var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();

        services.AddMassTransit(configure =>
        {

            configure.UsingRabbitMq((context, configurator) =>
            {

                configurator.Host(rabbitMQSettings.Host);
                configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceName, false));
                configurator.UseMessageRetry(retryConfigurator =>
                {
                    retryConfigurator.Interval(3, TimeSpan.FromSeconds(5));
                });
            });
        });

        return services;
    }

    public static IServiceCollection AddQuartz(IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(OutboxProcessor));

            configure
                .AddJob<OutboxProcessor>(jobKey)
                .AddTrigger(trigger =>
                {
                    trigger.ForJob(jobKey)
                        .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(10).RepeatForever());
                });

            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();

        return services;
    }
}
