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
        /// <summary>
        /// it executes code before the creation of model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // imports the previous execution of this method
            base.OnModelCreating(modelBuilder);
            //*
            // add, at this point, your new code

            // seed the Roles data
            modelBuilder.Entity<IdentityRole>().HasData(
              new IdentityRole { Id = "c", Name = "Cliente", NormalizedName = "CLIENTE" },
              new IdentityRole { Id = "g", Name = "Gestor", NormalizedName = "GESTOR" },
              new IdentityRole { Id = "f", Name = "Funcionario", NormalizedName = "FUNCIONARIO" }
              );


            // create the seed of your tables
            modelBuilder.Entity<Cliente>().HasData(
               new Cliente { Id = 1, PrimeiroNome = "Maria", Apelido = "Gomes", Morada = "Lisboa", CodPostal = "424242424242", Cidade = "Lisboa", País = "Portugal", Email = "mariagomes@blabla.com", NrTelemovel = "941941941", NrContribuinte = "412414141" }

            );
            modelBuilder.Entity<Gestor>().HasData(
               new Gestor { Id = 1, Nome = "Luís Freitas", NrTelemovel = "943943943", Foto = "", Email = "luisfreitas@blabla.com", Cargo = "Gestor" }

            );
            modelBuilder.Entity<Funcs>().HasData(
               new Funcs { Id = 1, Nome = "Beatriz", NrTelemovel = "942942942", Email = "beatrizpatita@blabla.com", Cargo = "Funcionário", ChefeFK = 1 }

            );

        }
        
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Armazem> Armazem { get; set; }
        public DbSet<Artigo> Artigos { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }
        public DbSet<Funcs> Funcionarios { get; set; }
        public DbSet<Gestor> Gestor { get; set; }
    }
}