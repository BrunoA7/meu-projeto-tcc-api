using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using webAPI_ASPNET.Data.Map;
using webAPI_ASPNET.Models;

namespace webAPI_ASPNET.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("USERS");
            modelBuilder.Entity<Button>().ToTable("BUTTON");
            modelBuilder.Entity<Department>().ToTable("DEPARTMENT");
            modelBuilder.Entity<DepartmentRelation>().ToTable("DEPARTMENTRELATION");
            modelBuilder.Entity<ButtonRelation>().ToTable("BUTTONRELATION");
            modelBuilder.Entity<Pedido>().ToTable("Pedido");
            modelBuilder.Entity<PedidoItem>().ToTable("PedidoItem");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Button> Buttons { get; set; }
        public DbSet<ButtonRelation> ButtonRelation { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<DepartmentRelation> DepartmentRelation { get; set; }
        public DbSet<User> UserLogin { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }
    }
}
