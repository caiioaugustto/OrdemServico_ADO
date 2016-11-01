using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class OrdemServicoRepository
    {
        string connectionString = ConnectionContext.Connection();

        public void Cadastrar(OrdemServico os)
        {
            using (SqlConnection connSql = new SqlConnection(connectionString))
            {
                try
                {
                    connSql.Open();

                    //Query que executará
                    SqlCommand cmdSql = new SqlCommand("Insert Into Ordem(DataSolicitacao, NumeroOrdemServico, NumeroCondominio, " +
                    "Solicitante, Gerente, Nucleo, DataEnvio, Prazo, DataLiberacao, Status, DescricaoServico, IdFornecedor) " +
                        "Values (@DataSolicitacao, @NumeroOrdemServico, @NumeroCondominio, @Solicitante, @Gerente, @Nucleo, @DataEnvio, @Prazo, @DataLiberacao, @Status, @DescricaoServico, @IdFornecedor)", connSql);

                    //Parametros do Insert do SqlCommand
                    //SqlDbType Inicializa uma nova instância da classe de SqlParameter que usa o nome do parâmetro e o tipo de dados.
                    cmdSql.Parameters.Add("@IdFornecedor", SqlDbType.Int).Value = os.IdFornecedor;
                    cmdSql.Parameters.Add("@DataSolicitacao", SqlDbType.DateTime).Value = DateTime.Today;
                    cmdSql.Parameters.Add("@NumeroOrdemServico", SqlDbType.VarChar, 20).Value = os.NumeroOrdemServico;
                    cmdSql.Parameters.Add("@NumeroCondominio", SqlDbType.Int).Value = os.NumeroCondominio;
                    cmdSql.Parameters.Add("@Solicitante", SqlDbType.VarChar, 30).Value = os.Solicitante;
                    cmdSql.Parameters.Add("@Gerente", SqlDbType.VarChar, 30).Value = os.Gerente;
                    cmdSql.Parameters.Add("@Nucleo", SqlDbType.VarChar, 10).Value = os.Nucleo;
                    cmdSql.Parameters.Add("@DataEnvio", SqlDbType.DateTime).Value = DateTime.Today;
                    cmdSql.Parameters.Add("@Prazo", SqlDbType.VarChar, 10).Value = os.Prazo;
                    cmdSql.Parameters.Add("@DataLiberacao", SqlDbType.DateTime).Value = os.DataLiberacao;
                    cmdSql.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = os.Status;
                    cmdSql.Parameters.Add("@DescricaoServico", SqlDbType.VarChar, 50).Value = os.DescricaoServico;

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

        public List<OrdemServico> Listar()
        {
            using (SqlConnection connSql = new SqlConnection(connectionString))
            {
                connSql.Open();

                SqlCommand cmdSql = new SqlCommand("Select Ordem.Id, Ordem.DataSolicitacao, Ordem.NumeroOrdemServico, Ordem.NumeroCondominio, " +
                 "Ordem.Solicitante, Ordem.Gerente, Ordem.Nucleo, Ordem.DataEnvio, Ordem.Prazo, Ordem.DataLiberacao, Ordem.Status, Ordem.DescricaoServico, Fornecedor.Nome as NomeFornecedor " +
                     "From Ordem inner join Fornecedor on Fornecedor.Id = IdFornecedor order by Ordem.DataSolicitacao", connSql);

                List<OrdemServico> listarOrdens = new List<OrdemServico>();

                using (SqlDataReader dr = cmdSql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var os = new OrdemServico();

                        os.Id = Convert.ToInt16(dr["Id"]);
                        os.Fornecedor.Nome = dr["NomeFornecedor"].ToString();
                        os.DataSolicitacao = Convert.ToDateTime(dr["DataSolicitacao"]);
                        os.NumeroOrdemServico = Convert.ToString(dr["NumeroOrdemServico"]);
                        os.NumeroCondominio = Convert.ToInt16(dr["NumeroCondominio"]);
                        os.Gerente = dr["Gerente"].ToString();
                        os.Nucleo = dr["Nucleo"].ToString();
                        os.DataEnvio = Convert.ToDateTime(dr["DataEnvio"]);
                        //os.Prazo = Convert.ToString(dr["Prazo"]);
                        os.DataLiberacao = Convert.ToDateTime(dr["DataLiberacao"]);
                        //os.Status = dr["Status"].ToString();
                        //os.DescricaoServico = dr["DescricaoServico"].ToString();

                        listarOrdens.Add(os);
                    }
                }
                connSql.Close();

                return listarOrdens;
            }
        }

        public void Exclur(int id)
        {
            using (SqlConnection connSql = new SqlConnection(connectionString))
            {
                try
                {
                    //Abre a conexão com o Banco
                    connSql.Open();

                    //Query que executará
                    SqlCommand cmdSql = new SqlCommand("delete from Ordem where Id = @id", connSql);

                    //SqlDbType Inicializa uma nova instância da classe de SqlParameter que usa o nome do parâmetro e o tipo de dados.
                    cmdSql.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    //Executa a Query
                    cmdSql.ExecuteNonQuery();

                    //Encerra a conexão com o Banco
                    connSql.Close();
                }
                catch
                {
                    connSql.Close();
                }
            }
        }

        public void Editar(OrdemServico os)
        {
            using (SqlConnection connSql = new SqlConnection(connectionString))
            {
                try
                {
                    //Abre a conexão com o Banco
                    connSql.Open();

                    //Query que executará
                    SqlCommand cmdSql = new SqlCommand("update Ordem set Nucleo = @Nucleo, Prazo = @Prazo, " +
                    "DataLiberacao = @DataLiberacao, Status = @Status, DescricaoServico = @DescricaoServico " +
                         "where Id = @id", connSql);

                    //Parametros do Insert do SqlCommand
                    cmdSql.Parameters.Add("@Id", SqlDbType.Int).Value = os.Id;
                    cmdSql.Parameters.Add("@Nucleo", SqlDbType.VarChar, 30).Value = os.Nucleo;
                    cmdSql.Parameters.Add("@DataLiberacao", SqlDbType.DateTime).Value = os.DataLiberacao;
                    cmdSql.Parameters.Add("@Status", SqlDbType.VarChar).Value = os.Status;
                    cmdSql.Parameters.Add("@DescricaoServico", SqlDbType.VarChar, 50).Value = os.DescricaoServico;

                    //Executa a Query
                    cmdSql.ExecuteNonQuery();

                    //Encerra a conexão com o Banco
                    connSql.Close();
                }
                catch
                {
                    connSql.Close();
                }
            }
        }
    }
}