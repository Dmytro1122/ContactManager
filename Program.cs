using ContactManager.DataAccess;
using ContactManager.Models;
using ContactManager.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Налаштування підключення до бази даних
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Реєстрація репозиторію та сервісів у контейнері DI
builder.Services.AddScoped<IRepository<Contact>, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

// Додати MVC сервіс для контролерів та представлень
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Contacts}/{action=Index}/{id?}");

app.Run();
