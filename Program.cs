using Microsoft.EntityFrameworkCore;
using MoviesManagment.Data;
using MoviesManagment.Models;
using MoviesManagment.Repositories;
using MoviesManagment.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add framework services.
builder.Services
	.AddControllersWithViews();
	builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Add Kendo UI services to the services container
builder.Services.AddKendo();

// Add services to the container.
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("MovieConnection")),ServiceLifetime.Transient);

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var dbContext = services.GetRequiredService<MovieContext>();
        dbContext.Database.EnsureCreated();

        if (!dbContext.Movies.Any())
        {
            dbContext.Movies.Add(new Movie { Title = "Inception", ReleaseYear = "2010" });
            dbContext.Movies.Add(new Movie { Title = "Joker", ReleaseYear = "2019" });

            dbContext.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        throw;
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
