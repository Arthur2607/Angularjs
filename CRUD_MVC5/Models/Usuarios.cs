using CRUD_MVC5.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD_MVC5.Models
{
    public class Usuarios
    {
        private readonly static string _conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USUARIO\Desktop\Angularjs\Emp_Desen\CRUD_MVC5\App_Data\ListaTelefonica.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

        public int funcionarioId { get; set; }
        public string funcionarioNome { get; set; }
        public string funcionarioCPF { get; set; }
        public string funcionarioLogin { get; set; }
        public string funcionarioSenha { get; set; }

        public bool Login()
        {
            var result = false;
            var sql = "SELECT funcionarioId, funcionarioLogin, funcionarioSenha FROM Funcionarios WHERE funcionarioLogin ='" + this.funcionarioLogin + "'";
            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    string md5StringPassword = Md5Controller.CriarHash(funcionarioSenha);

                                    if (md5StringPassword == dr["funcionarioSenha"].ToString())
                                    {
                                        funcionarioId = Convert.ToInt32(dr["funcionarioId"]);
                                        funcionarioLogin = dr["funcionarioLogin"].ToString();
                                        result = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                return false;
            }

            return result;
            
        }
    }
}