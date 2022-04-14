using CRUD_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace CRUD_MVC5.Controllers
{
    public class FuncionarioController : Controller
    {
        public ActionResult Login()
        {
            if (Session["Erro"] != null)
                ViewBag.Erro = Session["Erro"].ToString();

            return View();
        }

        
           

        #region Metodo para criar um funcionario - CREATE
        [HttpPost]
        public JsonResult adcionarFuncionario(Funcionario funcionario)
        {
            if (funcionario != null)
            {
                using(var db = new ListaTelefonicaEntities())
                {
                    if (verificarCPF(funcionario.funcionarioCPF))
                    {

                        return Json(new { sucess = false, message = "CPF ja cadastrado." });

                        
                    }
                    if (verificarUsuario(funcionario.funcionarioLogin))
                    {
                        return Json(new { sucess = false, message = "Usuario indisponivel." });
                    }

                    db.Funcionarios.Add(funcionario);
                    funcionario.funcionarioSenha = Md5Controller.CriarHash(funcionario.funcionarioSenha);
                    db.SaveChanges();

                    return Json(new { sucess = true, message = "CPF cadastrado com sucesso." });
                }
            }
                return Json(new { sucess = false, message = "Funcionario precisa ser cadastrado." });
        }
        #endregion

        #region Metodo para Checar Login

        [HttpPost]
        public void ChecarLogin(string Login, string password)
        {
            var usuario = new Usuarios();
            ListaTelefonicaEntities db = new ListaTelefonicaEntities();
            string md5StringPassword = Md5Controller.CriarHash(password);
            var dataItem = db.Funcionarios.Where(x => x.funcionarioLogin == Login && x.funcionarioSenha == md5StringPassword).SingleOrDefault();
            usuario.funcionarioLogin = Request["Login"];
            usuario.funcionarioSenha = Request["PassWord"];

            if (usuario.Login())
            {
                Session["Autorizado"] = "OK";
                Session.Remove("Erro");
                Response.Redirect("/Home/Index");
            }
            else
            {
                Session["Erro"] = "Senha ou usuário inválidos";
                Response.Redirect("/Home/Login");
                
            }

            
        }

        #endregion

        #region Metodo para verificar se o CPF ja existe - SELECT

        public bool verificarCPF (string funcionarioCPF)
        { 
            using(var db = new ListaTelefonicaEntities())
            {
              var pesquisaCPF =  db.Funcionarios.FirstOrDefault(_ => _.funcionarioCPF == funcionarioCPF);
               if(pesquisaCPF != null)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Metodo para verificar se o Login é existente - SELECT
        public bool verificarUsuario(string funcionarioLogin)
        {
            using (var db = new ListaTelefonicaEntities())
            {
                var pesquisaCPF = db.Funcionarios.FirstOrDefault(_ => _.funcionarioLogin == funcionarioLogin);
                if (pesquisaCPF != null)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion


    }
}