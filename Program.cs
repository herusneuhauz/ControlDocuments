using ControlDocuments.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.AddControllersWithViews();

// Configura o banco de dados usando SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona o suporte à cache distribuída (necessário para sessões)
builder.Services.AddDistributedMemoryCache();

// Adiciona o serviço de sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configura o pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Habilita a página de erro em desenvolvimento
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Para ambientes de produção
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Adiciona o middleware de sessão ao pipeline
app.UseSession();

app.UseAuthorization();

// Mapeia a rota padrão para o controlador Usuario e a ação Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}"); // Altera para iniciar no Login

app.Run();
