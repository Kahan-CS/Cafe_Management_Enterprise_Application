using AdminClient.Configuration;
using AdminClient.Services;
using Microsoft.Extensions.Options;

// Load environment variables from `.env` file 
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables();

builder.Services.Configure<ApiSettings>(
	builder.Configuration.GetSection("ApiSettings"));

builder.Services.AddHttpClient<ApiService>((provider, client) =>
{
	var settings = provider.GetRequiredService<IOptions<ApiSettings>>().Value;
	client.BaseAddress = new Uri(settings.BaseUrl);
});

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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();


app.Run();
