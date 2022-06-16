using LojaLegos.Models;
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
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Armazem> Armazem { get; set; }
        public DbSet<Artigo> Artigos { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }
        public DbSet<Funcs> Funcionarios { get; set; }
        public DbSet<Gestor> Gestor { get; set; }
    }
}