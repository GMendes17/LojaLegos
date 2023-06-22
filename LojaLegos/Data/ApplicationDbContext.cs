using LojaLegos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LojaLegos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "c", Name = "Cliente", NormalizedName = "CLIENTE" },
                new IdentityRole { Id = "g", Name = "Gestor", NormalizedName = "GESTOR" },
                new IdentityRole { Id = "f", Name = "Funcionario", NormalizedName = "FUNCIONARIO" }
            );

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { Id = 1, PrimeiroNome = "Gonçalo", Apelido = "Mendes", Morada = "Rua...", CodPostal = "2660-284", Cidade = "Santo António dos Cavaleiros", País = "Portugal", Email = "goncalomendes@sapo.pt", NrTelemovel = "941941941", NrContribuinte = "412414141" }
            );

            modelBuilder.Entity<Gestor>().HasData(
                new Gestor { Id = 1, Nome = "Luís Freitas", NrTelemovel = "943943943", Foto = "", Email = "luisfreitas@blabla.com", Cargo = "Gestor" }
            );

            modelBuilder.Entity<Funcs>().HasData(
                new Funcs { Id = 1, Nome = "Beatriz", NrTelemovel = "942942942", Email = "beatrizpatita@blabla.com", Cargo = "Funcionário", ChefeFK = 1 }
            );
            modelBuilder.Entity<Artigo>().HasData(
                 new Artigo { Id = 1, Nr = "42004", Tipo = "Technic", Nome = "Mini blackhoe Loader", Preco = (Decimal)1222.00, Foto = "42004.jpg", NrPecas = "123", Detalhes = "gosfdnaiudsf", Stock = "5", ArmazemFK = 1 }
                );
            modelBuilder.Entity<Armazem>().HasData(
                new Armazem { Id =1 ,Local = "Tomar" , ResponsavelFK =1}
                );

            modelBuilder.Entity<ArtigoEncomenda>()
                 .HasKey(ae => new { ae.ArtigoId, ae.EncomendaId });

            modelBuilder.Entity<ArtigoEncomenda>()
                .HasOne(ae => ae.Artigo)
                .WithMany(a => a.ArtigoEncomendas)
                .HasForeignKey(ae => ae.ArtigoId);

            modelBuilder.Entity<ArtigoEncomenda>()
                .HasOne(ae => ae.Encomenda)
                .WithMany(e => e.ArtigoEncomendas)
                .HasForeignKey(ae => ae.EncomendaId);
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Armazem> Armazem { get; set; }
        public DbSet<Artigo> Artigos { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }
        public DbSet<Funcs> Funcionarios { get; set; }
        public DbSet<Gestor> Gestor { get; set; }
        public DbSet<ArtigoEncomenda> ArtigoEncomendas { get; set; }


    }
}