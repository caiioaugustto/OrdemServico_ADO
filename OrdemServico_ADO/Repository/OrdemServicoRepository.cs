﻿using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Repository
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
                    SqlCommand cmdSql = new SqlCommand("Insert Into Ordem(DataSolicitacao, NumeroOrdemServico, " +
                    "Solicitante, Gerente, Nucleo, DataEnvio, Prazo, DataLiberacao, Status, DescricaoServico, IdFornecedor) " +
                        "Values (@DataSolicitacao, @NumeroOrdemServico, @Solicitante, @Gerente, @Nucleo, @DataEnvio, @Prazo, @DataLiberacao, @Status, @DescricaoServico, @IdFornecedor)", connSql);

                    //Parametros do Insert do SqlCommand
                    //SqlDbType Inicializa uma nova instância da classe de SqlParameter que usa o nome do parâmetro e o tipo de dados.
                    cmdSql.Parameters.Add("@IdFornecedor", SqlDbType.Int).Value = os.IdFornecedor;
                    cmdSql.Parameters.Add("@DataSolicitacao", SqlDbType.DateTime).Value = DateTime.Today;
                    cmdSql.Parameters.Add("@NumeroOrdemServico", SqlDbType.VarChar, 20).Value = os.NumeroOrdemServico;
                    cmdSql.Parameters.Add("@Solicitante", SqlDbType.VarChar, 30).Value = os.Solicitante;
                    cmdSql.Parameters.Add("@Gerente", SqlDbType.VarChar, 30).Value = os.Gerente;
                    cmdSql.Parameters.Add("@Nucleo", SqlDbType.VarChar, 10).Value = os.Nucleo;
                    cmdSql.Parameters.Add("@DataEnvio", SqlDbType.DateTime).Value = DateTime.Today;
                    cmdSql.Parameters.Add("@Prazo", SqlDbType.DateTime).Value = os.Prazo;
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


        //public IList<OrdemServico> Listar()
        //{
        //    var result = new List<OrdemServico>();

        //    SqlConnection connection = null;
        //    SqlCommand command = null;
        //    SqlDataReader reader = null;

        //    try
        //    {
        //        var textCommand = new StringBuilder();

        //        textCommand.AppendLine("SELECT ");
        //        textCommand.AppendLine("    * from ");
        //        textCommand.AppendLine("    Ordem ");
        //        textCommand.AppendLine("    inner join ");
        //        textCommand.AppendLine("    Fornecedor on Fornecedor Id = IdFornecedor ");

        //        var parameters = new List<SqlParameter>();

        //        using (connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            command = new SqlCommand(textCommand.ToString(), connection);
        //            command.CommandTimeout = 3600;

        //            reader = command.ExecuteReader();

        //            if (reader != null && reader.HasRows)
        //                result = PreencherLista(reader);

        //            connection.Close();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        if (reader != null) reader.Dispose();
        //        if (command != null) command.Dispose();

        //        if (connection != null)
        //        {
        //            if (connection.State != ConnectionState.Closed) connection.Close();
        //            connection.Dispose();
        //        }
        //    }
        //    return result;
        //}


        //public List<OrdemServico> PreencherLista(SqlDataReader dr)
        //{
        //    var result = new List<OrdemServico>();

        //    while (dr.Read())
        //    {
        //        string myDescription = dr["DescricaoServico"].ToString();
        //        string myStatus = dr["Status"].ToString();

        //        var relatorio = new OrdemServico
        //        {
        //            Id = Convert.ToInt16(dr["Id"]),
        //            DataSolicitacao = Convert.ToDateTime(dr["DataSolicitacao"]),
        //            NumeroOrdemServico = Convert.ToString(dr["NumeroOrdemServico"]),
        //            Prazo = Convert.ToDateTime(dr["Prazo"]),
        //            Solicitante = Convert.ToString(dr["Solicitante"]),
        //            Status = (Status)Enum.Parse(typeof(Status), myStatus, true),
        //            DescricaoServico = (Descricao)Enum.Parse(typeof(Descricao), myDescription, true),
        //            Nucleo = Convert.ToString(dr["Nucleo"]),
        //            Gerente = Convert.ToString(dr["Gerente"]),
        //            DataEnvio = Convert.ToDateTime(dr["DataEnvio"]),
        //            DataLiberacao = Convert.ToDateTime(dr["DataLiberacao"]),
        //        };

        //        result.Add(relatorio);
        //    }

        //    return result;
        //}

        public List<OrdemServico> Listar()
        {
            using (SqlConnection connSql = new SqlConnection(connectionString))
            {
                connSql.Open();

                SqlCommand cmdSql = new SqlCommand("Select Ordem.Id, Ordem.DataSolicitacao, Ordem.NumeroOrdemServico, " +
                 "Ordem.Solicitante, Ordem.Prazo, Ordem.Status, Ordem.DescricaoServico, Ordem.Nucleo, Ordem.DataEnvio, Ordem.DataLiberacao, " +
                 "Ordem.Gerente, Fornecedor.Nome as NomeFornecedor " +
                     "From Ordem inner join Fornecedor on Fornecedor.Id = IdFornecedor order by Ordem.DataSolicitacao", connSql);

                List<OrdemServico> listarOrdens = new List<OrdemServico>();

                using (SqlDataReader dr = cmdSql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var os = new OrdemServico();

                        string myDescription = dr["DescricaoServico"].ToString();
                        string myStatus = dr["Status"].ToString();

                        os.Id = Convert.ToInt16(dr["Id"]);
                        os.Fornecedor.Nome = Convert.ToString(dr["NomeFornecedor"]);
                        os.DataSolicitacao = Convert.ToDateTime(dr["DataSolicitacao"]);
                        os.NumeroOrdemServico = Convert.ToString(dr["NumeroOrdemServico"]);
                        os.Prazo = Convert.ToDateTime(dr["Prazo"]);
                        os.Solicitante = Convert.ToString(dr["Solicitante"]);
                        os.Status = (Status)Enum.Parse(typeof(Status), myStatus, true);
                        os.DescricaoServico = (Descricao)Enum.Parse(typeof(Descricao), myDescription, true);
                        os.Nucleo = Convert.ToString(dr["Nucleo"]);
                        os.Gerente = Convert.ToString(dr["Gerente"]);
                        os.DataEnvio = Convert.ToDateTime(dr["DataEnvio"]);
                        os.DataLiberacao = Convert.ToDateTime(dr["DataLiberacao"]);

                        listarOrdens.Add(os);
                    }
                }
                connSql.Close();

                return listarOrdens;
            }
        }

        public List<OrdemServico> ListarFiltro(string nome, bool ativo)
        {
            using (SqlConnection connSql = new SqlConnection(connectionString))
            {
                List<OrdemServico> ordensServico = new List<OrdemServico>();

                connSql.Open();

                SqlCommand cmdSql = new SqlCommand();

                var bitAtivo = 0;

                if (!ativo) // Ativo
                    bitAtivo = 1;

                if (nome == "")
                {
                    cmdSql = new SqlCommand("select Ordem.Id, Ordem.DataSolicitacao, Ordem.NumeroOrdemServico, Ordem.Solicitante, Ordem.Prazo, Ordem.Status, " + 
                        "Ordem.DescricaoServico, Ordem.Nucleo, Ordem.DataEnvio, Ordem.Gerente, Fornecedor.Nome as NomeFornecedor " + 
                            " from Ordem inner join Fornecedor on Fornecedor.Id = IdFornecedor where Ordem.Ativo = " + bitAtivo + " order by Ordem.DataSolicitacao", connSql);
                }
                else if (nome != "")
                {
                    cmdSql.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = nome;

                    cmdSql = new SqlCommand("Select Ordem.Id, Ordem.DataSolicitacao, Ordem.NumeroOrdemServico, Ordem.Solicitante, Ordem.Prazo, Ordem.Status, " + 
                        " Ordem.DescricaoServico, Ordem.Nucleo, Ordem.DataEnvio, Ordem.Gerente, Fornecedor.Nome as NomeFornecedor " +
                           " From Ordem inner join Fornecedor on Fornecedor.Id = IdFornecedor where Fornecedor.Nome like'%@Nome%' and Ordem.Ativo = " + bitAtivo + " order by Ordem.DataSolicitacao", connSql);
                }

                using (SqlDataReader dr = cmdSql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var os = new OrdemServico();

                        string myDescription = dr["DescricaoServico"].ToString();
                        string myStatus = dr["Status"].ToString();

                        os.Id = Convert.ToInt16(dr["Id"]);
                        os.Fornecedor.Nome = dr["NomeFornecedor"].ToString();
                        os.DataSolicitacao = Convert.ToDateTime(dr["DataSolicitacao"]);
                        os.NumeroOrdemServico = Convert.ToString(dr["NumeroOrdemServico"]);
                        os.Prazo = Convert.ToDateTime(dr["Prazo"]);
                        os.Solicitante = Convert.ToString(dr["Solicitante"]);
                        os.Status = (Status)Enum.Parse(typeof(Status), myStatus, true);
                        os.DescricaoServico = (Descricao)Enum.Parse(typeof(Descricao), myDescription, true);

                        ordensServico.Add(os);
                    }
                }
                connSql.Close();

                return ordensServico;
            }
        }


        public OrdemServico Detalhes(int id)
        {
            using (SqlConnection connSql = new SqlConnection(connectionString))
            {

                SqlCommand cmdSql = new SqlCommand("Select Ordem.Id, Ordem.DataSolicitacao, Ordem.NumeroOrdemServico, " +
               "Ordem.Solicitante, Ordem.Gerente, Ordem.Nucleo, Ordem.DataEnvio, Ordem.Prazo, Ordem.DataLiberacao, Ordem.Status, Ordem.DescricaoServico, Fornecedor.Nome as NomeFornecedor " +
                   "From Ordem inner join Fornecedor on Fornecedor.Id = IdFornecedor order by Ordem.DataSolicitacao", connSql);

                //SqlDbType Inicializa uma nova instância da classe de SqlParameter que usa o nome do parâmetro e o tipo de dados.
                cmdSql.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                connSql.Open();

                OrdemServico os = new OrdemServico();

                using (SqlDataReader sdr = cmdSql.ExecuteReader())
                {
                    if (sdr.Read())
                    {

                        string myDescription = sdr["DescricaoServico"].ToString();
                        string myStatus = sdr["Status"].ToString();

                        os.Id = Convert.ToInt16(sdr["Id"]);
                        os.Fornecedor.Nome = sdr["NomeFornecedor"].ToString();
                        os.DataSolicitacao = Convert.ToDateTime(sdr["DataSolicitacao"]);
                        os.NumeroOrdemServico = Convert.ToString(sdr["NumeroOrdemServico"]);
                        os.Gerente = sdr["Gerente"].ToString();
                        os.Nucleo = sdr["Nucleo"].ToString();
                        os.DataEnvio = Convert.ToDateTime(sdr["DataEnvio"]);
                        os.Prazo = Convert.ToDateTime(sdr["Prazo"]);
                        os.DataLiberacao = Convert.ToDateTime(sdr["DataLiberacao"]);
                        os.Solicitante = Convert.ToString(sdr["Solicitante"]);
                        os.Status = (Status)Enum.Parse(typeof(Status), myStatus, true);
                        os.DescricaoServico = (Descricao)Enum.Parse(typeof(Descricao), myDescription, true);
                    }
                }

                connSql.Close();

                return os;
            }
        }

        public void Inativar(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection connSql = new SqlConnection(connectionString))
                {
                    connSql.Open();

                    SqlCommand cmdSql = connSql.CreateCommand();

                    cmdSql.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    cmdSql.CommandText = "update Ordem set Ativo = 0 where Id = @id";
                    cmdSql.ExecuteNonQuery();
                }

                scope.Complete();
            }
        }

        public void Ativar(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection connSql = new SqlConnection(connectionString))
                {
                    connSql.Open();

                    SqlCommand cmdSql = connSql.CreateCommand();

                    cmdSql.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    cmdSql.CommandText = "update Ordem set Ativo = 1 where Id = @id";
                    cmdSql.ExecuteNonQuery();
                }

                scope.Complete();
            }
        }

        public OrdemServico PegarOrdem(int id)
        {
            //Using garante o dispose no final
            using (SqlConnection connSql = new SqlConnection(connectionString))
            {
                SqlCommand cmdSql = new SqlCommand("Select * from Ordem where Id = @id; ", connSql);

                //SqlDbType Inicializa uma nova instância da classe de SqlParameter que usa o nome do parâmetro e o tipo de dados.
                cmdSql.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                connSql.Open();

                OrdemServico os = new OrdemServico();

                using (SqlDataReader sdr = cmdSql.ExecuteReader())
                {
                    if (sdr.Read())
                    {
                        string myDescription = sdr["DescricaoServico"].ToString();
                        string myStatus = sdr["Status"].ToString();

                        os.Gerente = (String)sdr["Gerente"];
                        os.Nucleo = (String)sdr["Nucleo"];
                        os.Prazo = Convert.ToDateTime(sdr["Prazo"]);
                        os.DataLiberacao = Convert.ToDateTime(sdr["DataLiberacao"]);
                        os.Status = (Status)Enum.Parse(typeof(Status), myStatus, true);
                        os.DescricaoServico = (Descricao)Enum.Parse(typeof(Descricao), myDescription, true);
                    }
                }

                connSql.Close();

                return os;
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
                    SqlCommand cmdSql = new SqlCommand("update Ordem set Gerente = @Gerente, Nucleo = @Nucleo, " +
                    "Prazo = @Prazo, Status = @Status, DataLiberacao = @DataLiberacao, DescricaoServico = @DescricaoServico " +
                         "where Id = @Id", connSql);

                    //Parametros do Insert do SqlCommand
                    cmdSql.Parameters.Add("@Id", SqlDbType.Int).Value = os.Id;
                    cmdSql.Parameters.Add("@Gerente", SqlDbType.VarChar, 30).Value = os.Gerente;
                    cmdSql.Parameters.Add("@Nucleo", SqlDbType.VarChar, 10).Value = os.Nucleo;
                    cmdSql.Parameters.Add("@Prazo", SqlDbType.DateTime).Value = os.Prazo;
                    cmdSql.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = os.Status;
                    cmdSql.Parameters.Add("@DataLiberacao", SqlDbType.DateTime).Value = os.DataLiberacao;
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