using CRUD_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_MVC5.Controllers
{
    public class FuncionarioController : Controller
    {
        public ActionResult Login()
        {
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
                    db.SaveChanges();

                    return Json(new { sucess = true });
                }
            }
            return Json(new { sucess = false });
        }
        #endregion

       


      
    }
}