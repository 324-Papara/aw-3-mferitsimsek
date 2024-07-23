using AutoMapper;
using FluentValidation.AspNetCore;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Para.Api.Service;
using Para.Data.Context;
using Para.Data.UnitOfWork;
using System.Text.Json.Serialization;
using Para.Bussiness;
using Para.Bussiness.CQRS.Commands.CustomerCommands;
using System.Reflection;
using Autofac;

namespace Para.Api
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration, ContainerBuilder builder)
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

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            // AutoMapper ayarları
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfig());
            });
            //services.AddSingleton(config.CreateMapper());
            builder.RegisterInstance(config.CreateMapper()).As<IMapper>().SingleInstance();


            //services.AddMediatR(typeof(CreateCustomerCommand).GetTypeInfo().Assembly);
            builder.RegisterAssemblyTypes(typeof(CreateCustomerCommand).Assembly).AsImplementedInterfaces();


            //services.AddTransient<CustomService1>();
            builder.RegisterType<CustomService1>().InstancePerDependency();
            //services.AddScoped<CustomService2>();
            builder.RegisterType<CustomService2>().InstancePerLifetimeScope();
            //services.AddSingleton<CustomService3>();
            builder.RegisterType<CustomService3>().SingleInstance();

            // Otomatik doğrulama yapmak için AutoValidation kullanıyoruz.
            services.AddFluentValidationAutoValidation()
                        .AddFluentValidationClientsideAdapters(); // doğrulama kuralları sadece sunucu tarafında değil istemci tarafındada uygulanır. Örn: JavaScript ile 

            services.AddValidatorsFromAssemblyContaining<Startup>(); //Startup bulunduğu Assembly içindeki Validatörleri otomatik bulup DI ' a ekler..
        }
    }
}
