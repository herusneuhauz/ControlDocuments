using ControlDocuments.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao cont�iner.
builder.Services.AddControllersWithViews();

// Configura o banco de dados usando SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona o suporte � cache distribu�da (necess�rio para sess�es)
builder.Services.AddDistributedMemoryCache();

// Adiciona o servi�o de sess�o
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configura o pipeline de requisi��o HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Habilita a p�gina de erro em desenvolvimento
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Para ambientes de produ��o
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Adiciona o middleware de sess�o ao pipeline
app.UseSession();

app.UseAuthorization();

// Mapeia a rota padr�o para o controlador Usuario e a a��o Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}"); // Altera para iniciar no Login

app.Run();
