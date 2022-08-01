using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using BlogApp.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(
    options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    }
);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
builder.Services.AddDbContext<BlogAppDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection"))
);
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<BlogAppDbContext>();
    // context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Comment",
    pattern: "{controller=Comment}/{action=Create}/{id?}");
app.MapControllerRoute(
    name: "Category",
    pattern: "{controller=Category}/{action=Index}/{id?}");

app.Run();
