using FluentValidation;
using HHB.Application.Contracts;
using HHB.Application.DTO;
using HHB.Application.Mapper;
using HHB.Application.Services;
using HHB.Application.Validations;
using HHB.Domain.Contracts;
using HHB.Infrastructure.Context;
using HHB.Infrastructure.Repositories;

namespace HHB.API.Configuration
{
    public static class NativeAppInjection
    {
        public static void RegisterService(IServiceCollection services)
        {
            #region DbContext
            services.AddSingleton<MongoDbContext>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetValue<string>("MongoDbSettings:ConnectionString");
                var databaseName = configuration.GetValue<string>("MongoDbSettings:DatabaseName");

                return new MongoDbContext(connectionString, databaseName);
            });
            #endregion

            #region Repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerReviewRepository, CustomerReviewRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IAdditionalServiceRepository, AdditionalServiceRepository>();
            #endregion

            #region Services
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerReviewService, CustomerReviewService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IAdditionalService, AdditionalService>();
            #endregion

            #region Mapper
            services.AddAutoMapper(typeof(MapConfig));
            #endregion

            #region Validator
            services.AddScoped<IValidator<CustomerDto>, CustomerValidator>();
            services.AddScoped<IValidator<CustomerReviewDto>, CustomerReviewValidator>();
            services.AddScoped<IValidator<HotelDto>, HotelValidator>();
            services.AddScoped<IValidator<AddressDto>, AddressValidator>();
            services.AddScoped<IValidator<RoomDto>, RoomValidator>();
            services.AddScoped<IValidator<BookingDto>, BookingValidator>();
            #endregion
        }
    }
}
