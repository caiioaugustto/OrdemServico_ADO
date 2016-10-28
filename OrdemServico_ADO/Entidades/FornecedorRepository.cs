using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class FornecedorRepository
    {
        string connectionString = ConnectionContext.Connection();

        public void Cadastrar(Fornecedor fornecedor)
        {
            using (SqlConnection connSql = new SqlConnection(connectionString))
            {
                try
                {
                    connSql.Open();

                    //Query que executará
                    SqlCommand cmdSql = new SqlCommand("Insert Into Fornecedor(Nome, Telefone, NomeResponsavel, Email, Descricao) " +
                        "Values (@Nome, @Telefone, @NomeResponsavel, @Email, @Descricao)", connSql);

                    //Parametros do Insert do SqlCommand
                    //SqlDbType Inicializa uma nova instância da classe de SqlParameter que usa o nome do parâmetro e o tipo de dados.
                    cmdSql.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = fornecedor.Nome ;
                    cmdSql.Parameters.Add("@Telefone", SqlDbType.VarChar, 10).Value = fornecedor.Telefone;
                    cmdSql.Parameters.Add("@NomeResponsavel", SqlDbType.VarChar, 25).Value = fornecedor.NomeResponsavel;
                    cmdSql.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = fornecedor.Email;
                    cmdSql.Parameters.Add("@Descricao", SqlDbType.VarChar, 100).Value = fornecedor.Descricao;

                    //Executa a Query
                    cmdSql.ExecuteNonQuery();

                    //Encerra a conexão com o Banco
                    connSql.Close();
                }
                catch (Exception e)
                {
                    connSql.Close();
                }
            }
        }

        public List<Fornecedor> Listar()
        {
            using (SqlConnection connSql = new SqlConnection(connectionString))
            {
                connSql.Open();

                SqlCommand cmdSql = new SqlCommand("Select Fornecedor.Id, Fornecedor.Nome, Fornecedor.Telefone, Fornecedor.NomeResponsavel, Fornecedor.Email," +
                "Fornecedor.Descricao From Fornecedor order by Fornecedor.Nome", connSql);

                List<Fornecedor> listarFornecedores = new List<Fornecedor>();

                using (SqlDataReader dr = cmdSql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var forn = new Fornecedor();

                        forn.Id = Convert.ToInt16(dr["Id"]);
                        forn.Nome = dr["Nome"].ToString();
                        forn.Telefone = dr["Telefone"].ToString();
                        forn.NomeResponsavel = dr["NomeResponsavel"].ToString();
                        forn.Email = dr["Email"].ToString();
                        forn.Descricao = dr["Descricao"].ToString();

                        listarFornecedores.Add(forn);
                    }
                }
                connSql.Close();

                return listarFornecedores;
            }
        }
    }
}