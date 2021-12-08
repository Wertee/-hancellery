using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetProjectMVC.EF;
using PetProjectMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CancelarryDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
string identityCon = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UserDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
builder.Services.AddDbContext<EFDBContext>(options => options.UseSqlServer(connection));
builder.Services.AddDbContext<GameStoreIdentityContext>(options => options.UseSqlServer(identityCon));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<GameStoreIdentityContext>().AddDefaultTokenProviders();
var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}");

app.Run();
