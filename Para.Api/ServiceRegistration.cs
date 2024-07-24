using Autofac;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Para.Api.Service;
using Para.Bussiness;
using Para.Bussiness.CQRS.Commands.CustomerCommands;
using Para.Data.Context;
using Para.Data.UnitOfWork;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Para.Api
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // JSON ayarları
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            // Swagger ayarları
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Para.Api", Version = "v1" });
            });

            // Veritabanı bağlantı ayarları
            var connectionStringSql = configuration.GetConnectionString("MsSqlConnection");
            services.AddDbContext<ParaDbContext>(options => options.UseSqlServer(connectionStringSql));

            // PostgreSQL bağlantısı eklemek istersek
            //services.AddDbContext<ParaDbContext>(options => options.UseNpgsql(connectionStringPostgre));

            services.AddMediatR(typeof(CreateCustomerCommand).GetTypeInfo().Assembly);

            // Otomatik doğrulama yapmak için AutoValidation kullanıyoruz.
            services.AddFluentValidationAutoValidation()
                        .AddFluentValidationClientsideAdapters(); // doğrulama kuralları sadece sunucu tarafında değil istemci tarafındada uygulanır. Örn: JavaScript ile 

            services.AddValidatorsFromAssemblyContaining<Startup>(); //Startup bulunduğu Assembly içindeki Validatörleri otomatik bulup DI ' a ekler..
        }
        public static Serilog.ILogger ConfigureLogger(IConfiguration configuration)
        {
            var sinkOptions = new MSSqlServerSinkOptions
            {
                TableName = "Logs",
                AutoCreateSqlTable = true
            };

            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(
                    connectionString: configuration.GetConnectionString("MsSqlConnection"),
                    sinkOptions: sinkOptions)
                .CreateLogger();
        }
        public static void ConfigureContainer(ContainerBuilder builder)
        {
            // UnitOfWork
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            //// AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfig());
            });
            builder.RegisterInstance(config.CreateMapper()).As<IMapper>().SingleInstance();

            builder.RegisterAssemblyTypes(typeof(CreateCustomerCommand).Assembly)
                    .AsImplementedInterfaces();

            //// MediatR
            builder.RegisterAssemblyTypes(typeof(CreateCustomerCommand).Assembly).AsImplementedInterfaces();

            //// Custom services
            builder.RegisterType<CustomService1>().InstancePerDependency();
            builder.RegisterType<CustomService2>().InstancePerLifetimeScope();
            builder.RegisterType<CustomService3>().SingleInstance();
        }
    }
}
