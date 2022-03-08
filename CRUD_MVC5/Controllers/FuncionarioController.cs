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
                    db.Funcionarios.Add(funcionario);
                    funcionario.funcionarioSenha = Md5Controller.CriarHash(funcionario.funcionarioSenha);
                    db.SaveChanges();

                    return Json(new { sucess = true });
                }
            }
            return Json(new { sucess = false });
        }
        #endregion


        #region Metodo para Checar um login 

        [HttpPost]
        public JsonResult ChecarLogin(string Login, string password)
        {
            ListaTelefonicaEntities db = new ListaTelefonicaEntities();
            string md5StringPassword = Md5Controller.CriarHash(password);
            var dataItem = db.Funcionarios.Where(x => x.funcionarioLogin == Login && x.funcionarioSenha == md5StringPassword).SingleOrDefault();
            bool isLogged = true;
            if (dataItem != null)
            {
                Session["Login"] = dataItem.funcionarioLogin;
                isLogged = true;
                Response.Redirect("/Home/Index");
                
            }
            else
            {
                isLogged = false;
                Response.Redirect("/Home/Login");
            }
            return Json(isLogged, JsonRequestBehavior.AllowGet);
        }

        #endregion
        


    }
}