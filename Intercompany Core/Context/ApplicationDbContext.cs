using Microsoft.EntityFrameworkCore;
using IntercompanyCore;
using IntercompanyCore.Entities;
using Microsoft.EntityFrameworkCore.Design;

namespace IntercompanyCore
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public ApplicationDbContext() 
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //En caso de que el contexto no este configurado, lo configuramos mediante la cadena de conexion
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb;Initial Catalog = Intercompany; Integrated Security = True;");
            }
        }
        public DbSet<Transaccion> Transaccion { get; set; }
        public DbSet<CentrosCosto> CentrosCosto { get; set; }
        public DbSet<Cuentas> Cuentas { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<SocioNegocios> SocioNegocios { get; set; }
    }

    public class ApplicationDbContextFactory: IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("defaultConnection");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
