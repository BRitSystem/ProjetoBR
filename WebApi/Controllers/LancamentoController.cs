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
    public class LancamentoController : ControllerBase
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
                List<Tipodelancamento> LstLancamento = new List<Tipodelancamento>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select TPLANC_ID, TPLANC_NOME from TIPO_LANCAMENTOS";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Tipodelancamento lancamento = new Tipodelancamento()
                            {
                                Id = reader["PAIS_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TPLANC_ID"]),
                                Nome = reader["PAIS_NOME"] == DBNull.Value ? string.Empty : reader["TPLANC_NOME"].ToString(),

                            };

                            LstLancamento.Add(lancamento);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstLancamento.ToArray());
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
        [Route("Pesquisar/{TPLANC_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Tipodelancamento lancamento = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select TPLANC_ID, TPLANC_NOME from TIPO_LANCAMENTOS";
                        command.Parameters.AddWithValue("TPLANC_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            lancamento = new Tipodelancamento()
                            {
                                Id = reader["TPLANC_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TPLANC_ID"]),
                                Nome = reader["TPLANC_NOME"] == DBNull.Value ? string.Empty : reader["TPLANC_NOME"].ToString(),

                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, lancamento);
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
        [Route("Deletar/{TPLANC_ID:int}")]
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
                        command.CommandText = "delete from TIPO_LANCAMENTOS where TPLANC_ID = @id";
                        command.Parameters.AddWithValue("TPLANC_ID", id);

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
        /// <param name="TIPO_LANCAMENTOS"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Tipodelancamento lancamento)
        {
            try
            {
                bool resultado = false;

                if (lancamento == null) throw new ArgumentNullException("TIPO_LANCAMENTOS");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into TIPO_LANCAMENTOS(TPLANC_NOME) values(@nome)";

                        command.Parameters.AddWithValue("nome", lancamento.Nome);


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
        /// <param name="TIPO_LANCAMENTOS"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{TPLANC_ID:int}")]
        public ActionResult Put(int id, Tipodelancamento lancamento)
        {
            try
            {
                bool resultado = false;

                if (lancamento == null) throw new ArgumentNullException("TIPO_LANCAMENTOS");
                if (id == 0) throw new ArgumentNullException("TPLANC_ID");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update TIPO_LANCAMENTOS set TPLANC_NOME = @nome where TPLANC_ID = @id";

                        command.Parameters.AddWithValue("nome", lancamento.Nome);


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