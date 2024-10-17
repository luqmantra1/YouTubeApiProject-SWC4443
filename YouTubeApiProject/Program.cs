using YouTubeApiProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add YouTubeApiService and read from configuration
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<YouTubeApiService>();

// To ensure configuration is accessible in services
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddEnvironmentVariables();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
