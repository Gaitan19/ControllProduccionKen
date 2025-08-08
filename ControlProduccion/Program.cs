using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Infrastructure.Data;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Application.Profiles;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using ControlProduccion.Helpers;


var builder = WebApplication.CreateBuilder(args);


// 1) Registrar el ApplicationIdentityDbContext
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection") 
    ));



// 2) Agregar Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = false;
    })
    .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
    .AddErrorDescriber<SpanishIdentityErrorDescriber>()// Usar tu contexto de Identity
    .AddDefaultTokenProviders();

// Opcional: Configurar la cookie de autenticaci�n
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
});

//simbolo # directiva para compilar condicionalmente
#if DEBUG
builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();   // <-- habilita runtime compilation
#else
builder.Services
    .AddControllersWithViews();
#endif

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



// Si deseas registrar repositorios espec�ficos directamente (no siempre necesario)
//builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
//servicios

builder.Services.AddScoped<ITipoFabricacionService, TipoFabricacionService>();
builder.Services.AddScoped<IPrdPanelesCovintecService, PrdPanelesCovintecService>();
builder.Services.AddScoped<IPrdCerchaCovintecService, PrdCerchaCovintecService>();
builder.Services.AddScoped<IGestionCatalogosService, GestionCatalogosService>();
builder.Services.AddScoped<IPrdMallasCovintecService, PrdMallasCovintecService>();
builder.Services.AddScoped<IPrdIlKwangService, PrdIlKwangService>();
builder.Services.AddScoped<IPrdNeveraService, PrdNeveraService>();
builder.Services.AddScoped<IPrdOtroService, PrdOtroService>();
builder.Services.AddScoped<IPrdBloquesService, PrdBloquesService>();
builder.Services.AddScoped<IPrdCorteTService, PrdCorteTService>();
builder.Services.AddRazorPages();
var app = builder.Build();

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


app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
