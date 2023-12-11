using Business;
using Model;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Web.Mvc;

namespace CriaDigi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult MyContacts()
        {

            return View();
        }

        public ActionResult BuscarTodosContatos()
        {
            var Business = new ContatoBusiness();
            var listaContatos = Business.ObterQuantidadeDeContatos();
            //woto-> retorna os dados como JSON
            return Json(listaContatos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SalvarContato(Contato contato)
        {
            if (ModelState.IsValid)
            {
                var contatoModel = new Contato
                {
                    Nome = contato.Nome,
                    Telefone = contato.Telefone,
                    Data = DateTime.Now  
                };

                //woto-> validação feita no back e front
                if (string.IsNullOrWhiteSpace(contatoModel.Nome) || string.IsNullOrWhiteSpace(contatoModel.Telefone))
                {
                    // Faça algo se o Nome ou Telefone estiverem vazios
                    // Por exemplo, você pode adicionar mensagens de erro ao ModelState
                    ModelState.AddModelError("NomeOuTelefone", "Nome e Telefone são obrigatórios.");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var business = new ContatoBusiness();
                bool sucesso = business.SalvarContato(contatoModel);

                return sucesso ? Json(new { success = true }) : (ActionResult)
                new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult PopularFormContato(int recebendoId) 
        {
            var contatoService = new ContatoBusiness();
            var contato = contatoService.ObterContatoPorId(recebendoId);
            return Json(contato, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ObterContatoPorId(int idContato)
        {
            var contatoBusiness = new ContatoBusiness();
            var contato10 = contatoBusiness.ObterContatoPorId(idContato);
            return Json(contato10);

        }
        

        [HttpPost]
        public ActionResult AtualizarContato(Contato contatoAtualizado)
        {
            if (ModelState.IsValid)
            {
                var contatoBusiness = new ContatoBusiness();

                bool sucesso = contatoBusiness.AtualizarContato(contatoAtualizado);

                if (sucesso)
                {
                    return Json(contatoAtualizado);
                }
                else
                {
                    return HttpNotFound();
                }
            }

            return View("Editar", contatoAtualizado);
        }


        public ActionResult DeletarContato(int? contatoId)
        {
            var contatoBusiness = new ContatoBusiness();
            bool sucesso = contatoBusiness.DeletarContato(contatoId);
            if (sucesso)
            {
                return Json(new { success = true, message = "Contato excluído com sucesso!" });
            }
            else
            {
                return Json(new { success = false, message = "Erro ao excluir o contato" });
            }
        }
    }
}
