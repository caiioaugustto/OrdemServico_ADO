using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Uteis;

namespace Entidades
{
    public class LoginRepository
    {
        //public Login Buscar(string login, string senha)
        //{
        //    string senhaCriptografada = Criptografia.CriptografaMd5(senha);
        //    //var logar = context.LoginAdmin.Where(b => b.Usuario == login && b.Senha == senhaCriptografada).FirstOrDefault();
        //    return logar;

        //}

        string connectionString = ConnectionContext.Connection();

        public Login Buscar(string usuario, string senha)
        {
            string senhaCriptografada = Criptografia.CriptografaMd5(senha); 

            using (SqlConnection connSql = new SqlConnection(connectionString))
            {
                connSql.Open();

                SqlCommand cmdSql = new SqlCommand("select Login.Id, Login.Usuario, Login.Senha from Login where Usuario = @usuario and Senha = @senhaCriptografada;", connSql);

                cmdSql.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;
                cmdSql.Parameters.Add("@senhaCriptografada", SqlDbType.VarChar).Value = senhaCriptografada;

                Login autenticacao = new Login();

                using (SqlDataReader sdr = cmdSql.ExecuteReader())
                {
                    if (sdr.Read())
                    {
                        autenticacao.Id = Convert.ToInt16(sdr["Id"]);
                        autenticacao.Usuario = (String)sdr["Usuario"];
                        autenticacao.Senha = (String)sdr["Senha"];
                    }
                }
                connSql.Close();

                return autenticacao;
            }
        }
    }
}