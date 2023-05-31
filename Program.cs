using Microsoft.EntityFrameworkCore;
using MoviesManagment.Data;
using MoviesManagment.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("MovieConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var dbContext = services.GetRequiredService<MovieContext>();
        dbContext.Database.EnsureCreated(); // Upewnij siê, ¿e baza danych istnieje

        // SprawdŸ, czy baza danych jest pusta (nie ma jeszcze filmów)
        if (!dbContext.Movies.Any())
        {
            dbContext.Movies.Add(new Movie { Title = "Inception", YearProduction = "2010" });
            dbContext.Movies.Add(new Movie { Title = "Joker", YearProduction = "2019" });

            dbContext.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        // Obs³uga b³êdów
    }
}

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    try
//    {
//        var dbContext = services.GetRequiredService<MovieContext>();
//        dbContext.Database.Migrate();

//        dbContext.Movies.Add(new Movie { Title = "Film 1", Director = "Re¿yser 1", Genre = "Gatunek 1", YearProduction = "2022" });
//        dbContext.Movies.Add(new Movie { Title = "Film 2", Director = "Re¿yser 2", Genre = "Gatunek 2", YearProduction = "2023" });

//        dbContext.SaveChanges();
//    }
//    catch (Exception ex)
//    {
//        // Obs³uga b³êdów
//    }
//}


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
