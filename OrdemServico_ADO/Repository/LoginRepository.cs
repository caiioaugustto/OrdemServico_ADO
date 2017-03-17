using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Uteis;

namespace Repository
{
    public class LoginRepository
    {
        string connectionString = ConnectionContext.Connection();

        private Context context;

        public LoginRepository(Context context)
        {
            this.context = context;
        }

        public Login Buscar(string usuario, string senha)
        {
            using (SqlConnection connSql = new SqlConnection(connectionString))
            {
                connSql.Open();

                SqlCommand cmdSql = new SqlCommand("select Login.Id, Login.Usuario, Login.Senha from Login where Usuario = @usuario and Senha = @senhaCriptografada;", connSql);

                cmdSql.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;
                cmdSql.Parameters.Add("@senhaCriptografada", SqlDbType.VarChar).Value = senha;

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