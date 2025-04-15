using AdminClient.Configuration;
using AdminClient.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from `.env` file 
DotNetEnv.Env.Load();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<AuthApiService>();
builder.Services.AddHttpClient<BookingApiService>();
builder.Services.AddHttpClient<OrderApiService>();
builder.Services.AddHttpClient<UserApiService>();

builder.Services.AddSession();

//builder.Configuration
//	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//	.AddEnvironmentVariables();

//builder.Services.Configure<ApiSettings>(
//	builder.Configuration.GetSection("ApiSettings"));

builder.Services.Configure<ApiSettings>(options =>
{
	options.BaseUrl = Environment.GetEnvironmentVariable("API_BASE_URL")
		?? throw new InvalidOperationException("API_BASE_URL is missing in .env");
});

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<JwtTokenHandler>();

builder.Services.AddHttpClient<AuthApiService>((provider, client) =>
{
	var settings = provider.GetRequiredService<IOptions<ApiSettings>>().Value;
	client.BaseAddress = new Uri(settings.BaseUrl);
})
.AddHttpMessageHandler<JwtTokenHandler>();

builder.Services.AddHttpClient<AdminUserApiService>((provider, client) =>
{
	var settings = provider.GetRequiredService<IOptions<ApiSettings>>().Value;
	client.BaseAddress = new Uri(settings.BaseUrl);
})
.AddHttpMessageHandler<JwtTokenHandler>();

builder.Services.AddHttpClient<AdminBookingApiService>((provider, client) =>
{
	var settings = provider.GetRequiredService<IOptions<ApiSettings>>().Value;
	client.BaseAddress = new Uri(settings.BaseUrl);
})
.AddHttpMessageHandler<JwtTokenHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();

app.Run();

