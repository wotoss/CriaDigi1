
using System.Data.Entity.ModelConfiguration;


namespace DataBase.Configuracao
{
    public class ContatoConfiguracao : EntityTypeConfiguration<Model.Contato>
    {
        public ContatoConfiguracao()
        {         
            HasKey(c => c.Id);
            ToTable("Contatos", "dbo");
        }
    }
}
