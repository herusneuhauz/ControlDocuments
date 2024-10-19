namespace ControlDocuments.Data
{
    using Microsoft.EntityFrameworkCore;
    using ControlDocuments.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<LojaModel> Lojas { get; set; }
        public DbSet<DocumentoModel> Documentos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}
