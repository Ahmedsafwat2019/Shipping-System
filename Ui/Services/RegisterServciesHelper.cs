using BL.Contract;
using BL.Contract.Shipment;
using BL.Mapping;
using BL.Services;
using BL.Services.Shipment;
using BL.Services.Shipment.ManageStates;
using DAL;
using DAL.Contracts;
using DAL.Repositories;
using DAL.UserModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Serilog;
using System.Net.Http.Headers;
namespace Ui.Services
{
    public class RegisterServciesHelper
    {
        public static void RegisteredServices(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                // Base URL will be configured in GenericApiClient constructor using appsettings.json
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.AccessDeniedPath = "/access-denied";
                options.SlidingExpiration = true;
                options.Cookie.IsEssential = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
            });

         
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ShippingContext>()
    .AddDefaultTokenProviders();

            builder.Services.AddAuthorization();

            // Configure Serilog for logging
            /*
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.MSSqlServer(
                    connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
                    tableName: "Log",
                    autoCreateSqlTable: true)
                .CreateLogger();
            builder.Host.UseSerilog();
            */

            //builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            //builder.Services.AddScoped<IGenericRepository<TbShippingType>, DAL.Repositories.GenericRepository<TbShippingType>>();
            builder.Services.AddScoped<GenericApiClient>();
            builder.Services.AddHttpContextAccessor();         // لو مش ضايفها
          

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IViewRepository<>), typeof(ViewRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IShippingType, ShippingTypeService>();
            builder.Services.AddScoped<ICountry, CountryService>();
            builder.Services.AddScoped<ICity, CityService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPaymentMethods, PaymentMethodsService>();
            builder.Services.AddScoped<IPackgingTypes, ShipingPackgingService>();
            builder.Services.AddScoped<IUserSender, UserSenderService>();
            builder.Services.AddScoped<IUserReceiver, UserReceiverService>();
            builder.Services.AddScoped<ICarriersServices, CarriersServices>();
            builder.Services.AddScoped<IShipmentStatusHandlerFactory, ShipmentStatusHandlerFactory>();
            builder.Services.AddScoped<ApproveShipment>();
            builder.Services.AddScoped<ReadyShipment>();
            builder.Services.AddScoped<RejectShipment>();
            builder.Services.AddScoped<ShippShipment>();
            builder.Services.AddScoped<DeliverShipment>();
            builder.Services.AddScoped<ReturnShipment>();


            builder.Services.AddScoped<IShipment, ShipmentService>();
            builder.Services.AddScoped<ITrackingNumberCreator, TrackingNumberCreatorService>();
            builder.Services.AddScoped<IRateCalculator, RateCalculatorService>();
            builder.Services.AddScoped<IShipmentStatus, ShipmentStatusService>();

            builder.Services.AddScoped<IRefreshTokens, RefreshTokenService>();
        }
    }
}
