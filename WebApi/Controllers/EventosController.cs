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
    public class EventosController : ControllerBase
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
                List<Eventos> LstEventos = new List<Eventos>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select EVE_ID, EVE_NOME,EVE_DTEVENTO,EVE_RESTRITO, EVE_INSCRICAO, EVE_PRESENCA, EVE_APROVACAOMANUAL from EVENTOS";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Eventos eventos = new Eventos()
                            {
                                Id = reader["EVE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["EVE_ID"]),
                                Nome = reader["EVE_NOME"] == DBNull.Value ? string.Empty : reader["EVE_NOME"].ToString(),
                                Dtevento = reader["EVE_DTEVENTO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["EVE_DTEVENTO"]),
                                Restrito = (bool)reader["EVE_RESTRITO"],
                                Inscricao = (bool)reader["EVE_INSCRICAO"],
                                Presenca = (bool)reader["EVE_PRESENCA"],
                                Aprovacaomanual = (bool)reader["EVE_APROVACAOMANUAL"]
                            };

                            LstEventos.Add(eventos);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstEventos.ToArray());
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
        [Route("Pesquisar/{EVE_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Eventos eventos = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select EVE_ID, EVE_NOME,EVE_DTEVENTO,EVE_RESTRITO, EVE_INSCRICAO, EVE_PRESENCA, EVE_APROVACAOMANUAL from EVENTOS";
                        command.Parameters.AddWithValue("EVE_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            eventos = new Eventos()
                            {
                                Id = reader["EVE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["EVE_ID"]),
                                Nome = reader["EVE_NOME"] == DBNull.Value ? string.Empty : reader["EVE_NOME"].ToString(),
                                Dtevento = reader["EVE_DTEVENTO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["EVE_DTEVENTO"]),
                                Restrito = (bool)reader["EVE_RESTRITO"],
                                Inscricao = (bool)reader["EVE_INSCRICAO"],
                                Presenca = (bool)reader["EVE_PRESENCA"],
                                Aprovacaomanual = (bool)reader["EVE_APROVACAOMANUAL"]
                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, eventos);
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
        [Route("Deletar/{EVE_ID:int}")]
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
                        command.CommandText = "delete from EVENTOS where EVE_ID = @id";
                        command.Parameters.AddWithValue("EVE_ID", id);

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
        /// <param name="eventos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Eventos eventos)
        {
            try
            {
                bool resultado = false;

                if (eventos == null) throw new ArgumentNullException("EVENTOS");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into EVENTOS(EVE_NOME,EVE_DTEVENTO,EVE_RESTRITO, EVE_INSCRICAO, EVE_PRESENCA, EVE_APROVACAOMANUAL) values(@nome, @dtevento, @restrito, @inscricao, @presenca, @aprovacaomanual)";

                        command.Parameters.AddWithValue("nome", eventos.Nome);
                        command.Parameters.AddWithValue("dtevento", eventos.Dtevento);
                        command.Parameters.AddWithValue("restrito", eventos.Restrito);
                        command.Parameters.AddWithValue("inscricao", eventos.Inscricao);
                        command.Parameters.AddWithValue("presenca", eventos.Presenca);
                        command.Parameters.AddWithValue("aprovacaomanual", eventos.Aprovacaomanual);

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
        /// <param name="eventos"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{EVE_ID:int}")]
        public ActionResult Put(int id, Eventos eventos)
        {
            try
            {
                bool resultado = false;

                if (eventos == null) throw new ArgumentNullException("EVENTOS");
                if (id == 0) throw new ArgumentNullException("id");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update EVENTOS set EVE_NOME = @nome,EVE_DTEVENTO = @dtevento,EVE_RESTRITO = @restrito, EVE_INSCRICAO = @inscricao, EVE_PRESENCA = @presenca, EVE_APROVACAOMANUAL = @aprovacaomanual where EVE_ID = @id";

                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("nome", eventos.Nome);
                        command.Parameters.AddWithValue("dtevento", eventos.Dtevento);
                        command.Parameters.AddWithValue("restrito", eventos.Restrito);
                        command.Parameters.AddWithValue("inscricao", eventos.Inscricao);
                        command.Parameters.AddWithValue("presenca", eventos.Presenca);
                        command.Parameters.AddWithValue("aprovacaomanual", eventos.Aprovacaomanual);

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