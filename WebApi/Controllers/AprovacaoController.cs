using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AprovacaoController : ControllerBase
    {
        /// <summary>
        /// Metodo para consultar a lista de cliente
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Todos")]
        public ActionResult GetAll()
        {
            try
            {
                List<Aprovacao> LstAprovacao = new List<Aprovacao>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select APRO_ID, APRO_NOME,APRO_DTEVENTO,APRO_APROVACAO from APROVACOES";
                        //ID, NOME, Dtevento, BoolAprovacao
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Aprovacao aprovacao = new Aprovacao()
                            {
                                Id = reader["APRO_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["APRO_ID"]),
                                Nome = reader["APRO_NOME"] == DBNull.Value ? string.Empty : reader["APRO_NOME"].ToString(),
                                Dtevento = reader["APRO_DTEVENTO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["APRO_DTEVENTO"]),
                                BoolAprovacao = reader["BoolAprovacao"] == DBNull.Value

                            };

                            LstAprovacao.Add(aprovacao);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstAprovacao.ToArray());
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        /// <summary>
        /// Metodo para consultar detalhes de um determinado cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Pesquisar/{APRO_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Aprovacao aprovacao = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select APRO_ID, APRO_NOME,APRO_DTEVENTO,APRO_APROVACAO from APROVACOES";
                        command.Parameters.AddWithValue("APRO_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            aprovacao = new Aprovacao()
                            {
                                Id = reader["APRO_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["APRO_ID"]),
                                Nome = reader["APRO_NOME"] == DBNull.Value ? string.Empty : reader["APRO_NOME"].ToString(),
                                Dtevento = reader["APRO_DTEVENTO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["APRO_DTEVENTO"]),
                                BoolAprovacao = reader["BoolAprovacao"] == DBNull.Value
                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, aprovacao);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        /// <summary>
        /// Metodo para excluir um determinado cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Deletar/{APRO_ID:int}")]
        public ActionResult DeleteById(int id)
        {
            try
            {
                bool resultado = false;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "delete from APROVACOES where APRO_ID = @id, APRO_NOME = nome, APRO_DTEVENTO = dtevento, BoolAprovacao = aprovacao";
                        command.Parameters.AddWithValue("APRO_ID", id);

                        int i = command.ExecuteNonQuery();
                        resultado = i > 0;
                    }

                    connection.Close();
                }

                return StatusCode(200, resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        /// <summary>
        /// Metodo para cadastrar um novo cliente
        /// </summary>
        /// <param name="aprovacao"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Aprovacao aprovacao)
        {
            try
            {
                bool resultado = false;

                if (aprovacao == null) throw new ArgumentNullException("APROVACOES");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into MEMBROS(APRO_NOME,APRO_DTEVENTO,APRO_APROVACAO) values(@nome, @dtevento, @aprovacao)";

                        command.Parameters.AddWithValue("nome", aprovacao.Nome);
                        command.Parameters.AddWithValue("dtevento", aprovacao.Dtevento);
                        command.Parameters.AddWithValue("aprovacao", aprovacao.BoolAprovacao);

                        int i = command.ExecuteNonQuery();
                        resultado = i > 0;
                    }

                    connection.Close();
                }

                return StatusCode(200, resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        /// <summary>
        /// Metodo para atualizar os dados de um determinado cliente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="aprovacao"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{APRO_ID:int}")]
        public ActionResult Put(int id, Aprovacao aprovacao)
        {
            try
            {
                bool resultado = false;

                if (aprovacao == null) throw new ArgumentNullException("APROVACOES");
                if (id == 0) throw new ArgumentNullException("id");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update APROVACOES set APRO_NOME = @nome, APRO_DTEVENTO = @dtevento, APRO_APROVACAO = @aprovacao where APRO_ID = @id";
                        //MEMB_NOME, MEMB_CPF,MEMB_ENDERECO, MEMB_DTNASCIMENTO, MEMB_EMAIL, MEMB_FOTO
                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("nome", aprovacao.Nome);
                        command.Parameters.AddWithValue("dtevento", aprovacao.Dtevento);
                        command.Parameters.AddWithValue("aprovacao", aprovacao.BoolAprovacao);

                        int i = command.ExecuteNonQuery();
                        resultado = i > 0;
                    }

                    connection.Close();
                }

                return StatusCode(200, resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}