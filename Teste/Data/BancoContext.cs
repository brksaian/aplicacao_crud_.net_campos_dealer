using Microsoft.EntityFrameworkCore;
using Teste.Models;

namespace Teste.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<ProdutoModel> Produtos{ get; set; }
        public DbSet<VendaModel> Vendas { get; set; }
        public DbSet<DetalheVendaModel> DetalheVendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendaModel>()
                .HasMany(v => v.DetalhesVenda)
                .WithOne(d => d.Venda)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
