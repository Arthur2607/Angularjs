using CRUD_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_MVC5.Controllers
{
    public class ContatoController : Controller
    {
        #region Metodo para imprimir ou ler - READ
        public JsonResult GetContatos()
        {
            using (var db = new ListaTelefonicaEntities())
            {
                List<Contato> listarContatos = db.Contatos.ToList();
                return Json(listarContatos, JsonRequestBehavior.AllowGet);

            }
        }
        #endregion

        #region Metodo para inserir dados - CREATE

        //POST
        [HttpPost]
        public JsonResult adcionarContato(Contato contato)
        {
            if (contato != null)
            {
                using (var db = new ListaTelefonicaEntities())
                {
                    db.Contatos.Add(contato);
                    db.SaveChanges();

                    return Json(new { sucess = true });
                }
            }
            return Json(new { sucess = false });
        }

        #endregion

        #region Metodo para atualizar - UPDATE
        [HttpPost]
        public JsonResult atualizarContatos(Contato contato)
        {
            using (var db = new ListaTelefonicaEntities())
            {
                var contatoAtualizado = db.Contatos.Find(contato.contatosId);
                if (contatoAtualizado == null)
                {
                    return Json(new { sucess = false });
                }
                else
                {
                    contatoAtualizado.nome = contato.nome;
                    contatoAtualizado.telefone = contato.telefone;
                    contatoAtualizado.operadora = contato.operadora;

                    db.SaveChanges();
                    return Json(new { sucess = true });
                }
            }




        }
        #endregion

        #region Metodo para deletar dados - DELETE 
        [HttpPost]
        public JsonResult excluirContato(int id)
        {
            using (var db = new ListaTelefonicaEntities())
            {
                var contato = db.Contatos.Find(id);
                if (contato == null)
                {
                    return Json(new { sucess = false });
                }

                db.Contatos.Remove(contato);
                db.SaveChanges();

                return Json(new { sucess = true });
            }


        }
        #endregion

    }
}