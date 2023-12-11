using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuracao
{
    public class Configuracao : DbMigrationsConfiguration<ContatoContext>
    {
            public Configuracao()
            {
                AutomaticMigrationsEnabled = false;
            }
            protected override void Seed(ContatoContext context)
            {
            }
        }
    }

