using RestaurantsAPI;
using RestaurantsAPI.Entities;
using System.Reflection;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();       //przy kazdym uzyciu konstruktora tej klasy
//builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();        //przy kazdym zapytaniu do klienta nowy obiekt
//builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();     //tylko jedna statyczna instancja tej klasy
builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDBContext>();
builder.Services.AddScoped<RestaurantSeeder>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IRestaurantService, RestaurantService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Host.UseNLog();

var seeder = new RestaurantSeeder(new RestaurantDBContext());
var app = builder.Build();

// Configure the HTTP request pipeline.;
seeder.Seed();


if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapControllers();

app.Run();