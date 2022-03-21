using CRUD_MVC5.Models;
using Newtonsoft.Json;
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
        public string GetContatos()
        {
            using (var db = new ListaTelefonicaEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                List<Contato> listarContatos = db.Contatos.Include("Profissoes").ToList();
                var st = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                return JsonConvert.SerializeObject(listarContatos, st);
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
                    contatoAtualizado.genero = contato.genero;
                    contatoAtualizado.fk_profissoes = contato.fk_profissoes;

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

        #region Metodo para imprimir ou ler Profissoes - READ
        public JsonResult GetProfissoes()
        {
            List<Profissoes> listarProf = new List<Profissoes>();
            using (var db = new ListaTelefonicaEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                listarProf = db.Profissoes1.ToList();
            }
            return Json(listarProf, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Metodo para criar uma profissão - CREATE

        [HttpPost]
        public JsonResult adcionarProfissao(Profissoes profissoes)
        {
            if (profissoes != null)
            {
                using (var db = new ListaTelefonicaEntities())
                {
                    db.Profissoes1.Add(profissoes);

                    db.SaveChanges();

                    return Json(new { sucess = true });
                }
            }
            return Json(new { sucess = false });
        }

        #endregion


    }
}