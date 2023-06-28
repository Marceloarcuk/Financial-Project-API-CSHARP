using Entities.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Infra.Configuracao
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions options)  : base(options)
        {
        }
        public DbSet<SistemaFinanceiro> SistemaFinanceiro { set; get; }
        public DbSet<UsuarioSistemaFinanceiro> UsuarioSistemaFinanceiro { set; get; }
        public DbSet<Categoria> Categoria { set; get; }
        public DbSet<Despesa> Despesa { set; get; }

        //-------------------------------------------
        // CONEXAO COM BANCO DE DADOS
        //-------------------------------------------
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            optionsbuilder.UseSqlServer(ObterStringConexao());
            base.OnConfiguring(optionsbuilder);
        }
        //-------------------------------------------

        //-------------------------------------------
        // MAPEAR O ID DA TABELA  PARA TABELA
        //-------------------------------------------
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(x => x.Id);
            base.OnModelCreating(builder);
        }
        //-------------------------------------------


        //-------------------------------------------
        // STRING DE CONEXAO DO BANCO DE DADOS
        //-------------------------------------------
        public string ObterStringConexao()
        {
            return("Data Source=MACSAMSUNGNOTE\\SQLEXPRESS;Initial Catalog=FINANCEIRO_2023;Integrated Security=False;User ID=sa;Password=12345;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
        }
        //-------------------------------------------
    }
}
