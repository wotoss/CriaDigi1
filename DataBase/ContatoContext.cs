using DataBase.Configuracao;
using Model;
using System;

using System.Data.Entity;


namespace DataBase
{

    public class ContatoContext : DbContext
    {
        public ContatoContext() : base("sqlConn")
        {
        }
        
        //ele cria uma tabela na base de dados se ela não existir
        public DbSet<Contato> Contatos { get; set;  }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contato>().ToTable("Contatos");
            modelBuilder.Configurations.Add(new ContatoConfiguracao());
           
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
