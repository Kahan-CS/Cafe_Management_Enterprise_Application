using CustomerClient.Services;

var builder = WebApplication.CreateBuilder(args);



// Register HttpClient for your service with a base address; update the URL when the API is available.
builder.Services.AddHttpClient<IBookingService, BookingService>(client =>
{
    client.BaseAddress = new Uri("https://yourapiurl/"); // Replace with your API base URL
});

builder.Services.AddHttpClient<IOrderService, OrderService>(client =>
{
    client.BaseAddress = new Uri("https://yourapiurl/");
});

builder.Services.AddHttpClient<IUserService, UserService>(client =>
{
    client.BaseAddress = new Uri("https://yourapiurl/");
});


// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.MapDefaultControllerRoute();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
