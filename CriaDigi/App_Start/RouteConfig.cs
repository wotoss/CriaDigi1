
using System.Web.Mvc;
using System.Web.Routing;

namespace CriaDigi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ObterContatoPorId",
                url: "Home/ObterContatoPorId/{contatoId}",
                defaults: new { controller = "Home", action = "ObterContatoPorId" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            routes.MapRoute(
                name: "DeletarContato",
                url: "Home/DeletarContato/{contatoId}",
                defaults: new { controller = "Home", action = "DeletarContato" },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );

            routes.MapRoute(
                "contato_parametro",
                "contato/{id}/meus-contatos",
                new { controller = "Home", action = "MyContacts", id = 0 }
                );

            routes.MapRoute(
                "contato",
                "MeusContato",
                new { controller = "Home", action = "MyContacts" }
                );
            //woto-> esta é a rota padrão, quando abro o navegador vai para (index,home)
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
