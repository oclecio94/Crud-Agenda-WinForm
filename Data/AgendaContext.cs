using System.Data.Entity;
using Crud_Agenda_WinForm.Models;

namespace Crud_Agenda_WinForm.Data
{
    public class AgendaContext : DbContext
    {
        public AgendaContext() : base("name=AgendaDB") { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Compromisso> Compromissos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Compromisso>().ToTable("Compromissos");

            base.OnModelCreating(modelBuilder);
        }
    }
}