using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
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
                    SqlCommand cmdSql = new SqlCommand("Insert Into Fornecedor(Nome, Telefone, NomeResponsavel, Email, Descricao, NumeroFornecedor) " +
                        "Values (@Nome, @Telefone, @NomeResponsavel, @Email, @Descricao, @NumeroFornecedor)", connSql);

                    //Parametros do Insert do SqlCommand
                    //SqlDbType Inicializa uma nova instância da classe de SqlParameter que usa o nome do parâmetro e o tipo de dados.
                    cmdSql.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = fornecedor.Nome;
                    cmdSql.Parameters.Add("@Telefone", SqlDbType.VarChar, 10).Value = fornecedor.Telefone;
                    cmdSql.Parameters.Add("@NomeResponsavel", SqlDbType.VarChar, 25).Value = fornecedor.NomeResponsavel;
                    cmdSql.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = fornecedor.Email;
                    cmdSql.Parameters.Add("@Descricao", SqlDbType.VarChar, 100).Value = fornecedor.Descricao;
                    cmdSql.Parameters.Add("@NumeroFornecedor", SqlDbType.Int).Value = fornecedor.NumeroFornecedor;

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

        //public List<Fornecedor> Listar()
        //{
        //    using (SqlConnection connSql = new SqlConnection(connectionString))
        //    {
        //        connSql.Open();

        //        SqlCommand cmdSql = new SqlCommand("Select Fornecedor.Id, Fornecedor.Nome, Fornecedor.Telefone, Fornecedor.NomeResponsavel, Fornecedor.Email, Fornecedor.NumeroFornecedor, " +
        //        "Fornecedor.Descricao From Fornecedor order by Fornecedor.Nome", connSql);

        //        List<Fornecedor> listarFornecedores = new List<Fornecedor>();

        //        using (SqlDataReader dr = cmdSql.ExecuteReader())
        //        {
        //            while (dr.Read())
        //            {
        //                var forn = new Fornecedor();

        //                forn.Id = Convert.ToInt16(dr["Id"]);
        //                forn.Nome = dr["Nome"].ToString();
        //                forn.Telefone = dr["Telefone"].ToString();
        //                forn.NomeResponsavel = dr["NomeResponsavel"].ToString();
        //                forn.Email = dr["Email"].ToString();
        //                forn.Descricao = dr["Descricao"].ToString();
        //                forn.NumeroFornecedor = Convert.ToInt32(dr["NumeroFornecedor"]);

        //                listarFornecedores.Add(forn);
        //            }
        //        }
        //        connSql.Close();

        //        return listarFornecedores;
        //    }
        //}

        public void Excluir(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection connSql = new SqlConnection(connectionString))
                {
                    connSql.Open();

                    SqlCommand cmdSql = connSql.CreateCommand();

                    cmdSql.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    cmdSql.CommandText = "delete from Ordem where IdFornecedor = @id";
                    cmdSql.ExecuteNonQuery();

                    cmdSql.CommandText = "delete from Fornecedor where Id = @id ";
                    cmdSql.ExecuteNonQuery();
                }

                scope.Complete();
            }
        }

        public Fornecedor PegarFornecedor(int id)
        {
            //Using garante o dispose no final
            using (SqlConnection connSql = new SqlConnection(connectionString))
            {
                SqlCommand cmdSql = new SqlCommand("Select * from Fornecedor where Id = @id; ", connSql);

                //SqlDbType Inicializa uma nova instância da classe de SqlParameter que usa o nome do parâmetro e o tipo de dados.
                cmdSql.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                connSql.Open();

                Fornecedor fornecedor = new Fornecedor();

                using (SqlDataReader sdr = cmdSql.ExecuteReader())
                {
                    if (sdr.Read())
                    {
                        fornecedor.Telefone = (String)sdr["Telefone"];
                        fornecedor.NomeResponsavel = (String)sdr["NomeResponsavel"];
                        fornecedor.Email = (String)sdr["Email"];
                        fornecedor.Descricao = (String)sdr["Descricao"];
                    }
                }

                connSql.Close();

                return fornecedor;
            }
        }

        public void Editar(Fornecedor fornecedor)
        {
            using (SqlConnection connSql = new SqlConnection(connectionString))
            {
                try
                {
                    //Abre a conexão com o Banco
                    connSql.Open();

                    //Query que executará
                    SqlCommand cmdSql = new SqlCommand("update Fornecedor set Telefone = @Telefone, NomeResponsavel = @NomeResponsavel, " +
                    "Email = @Email, Descricao = @Descricao " +
                         "where Id = @Id", connSql);

                    //Parametros do Insert do SqlCommand
                    cmdSql.Parameters.Add("@Id", SqlDbType.Int).Value = fornecedor.Id;
                    cmdSql.Parameters.Add("@Telefone", SqlDbType.VarChar, 10).Value = fornecedor.Telefone;
                    cmdSql.Parameters.Add("@NomeResponsavel", SqlDbType.VarChar, 25).Value = fornecedor.NomeResponsavel;
                    cmdSql.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = fornecedor.Email;
                    cmdSql.Parameters.Add("@Descricao", SqlDbType.VarChar, 100).Value = fornecedor.Descricao;

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

        public IList<Fornecedor> ListarFiltro(string nome)
        {
            using (SqlConnection connSql = new SqlConnection(connectionString))
            {
                connSql.Open();

                SqlCommand cmdSql = new SqlCommand();

                if (nome == "")
                    cmdSql = new SqlCommand("select * from Fornecedor", connSql);

                else if (nome != "")
                {
                    cmdSql = new SqlCommand("Select Fornecedor.Id, Fornecedor.Nome, Fornecedor.Telefone, Fornecedor.NomeResponsavel, Fornecedor.Email, Fornecedor.NumeroFornecedor, " +
                 "Fornecedor.Descricao From Fornecedor where Fornecedor.Nome like @nome order by Fornecedor.Nome", connSql);

                    cmdSql.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = "%" + nome + "%";
                }

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
                        forn.NumeroFornecedor = Convert.ToInt32(dr["NumeroFornecedor"]);

                        listarFornecedores.Add(forn);
                    }
                }
                connSql.Close();

                return listarFornecedores;
            }
        }
    }
}