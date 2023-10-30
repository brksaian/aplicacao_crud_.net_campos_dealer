using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Teste.Data;
using Teste.Repositorio.Cliente;
using Teste.Repositorio.DetalheVenda;
using Teste.Repositorio.Produto;
using Teste.Repositorio.Vendas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BancoContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<IVendasRepositorio, VendasRepositorio>();
builder.Services.AddScoped<IDetalheVendaRepositorio, DetalheVendaRepositorio>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
