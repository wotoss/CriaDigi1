namespace DataBase.Configuracao
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Contatos",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Nome = c.String(),
            //            Telefone = c.String(),
            //            Data = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
           // DropTable("dbo.Contatos");
        }
    }
}
