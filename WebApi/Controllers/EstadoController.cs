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
    public class EstadoController : ControllerBase
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
                List<Estado> LstEstado = new List<Estado>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select ESTADO_ID, ESTADO_NOME from ESTADOS";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Estado estado = new Estado()
                            {
                                Id = reader["ESTADO_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ESTADO_ID"]),
                                Nome = reader["ESTADO_NOME"] == DBNull.Value ? string.Empty : reader["ESTADO_NOME"].ToString(),

                            };

                            LstEstado.Add(estado);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstEstado.ToArray());
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
        [Route("Pesquisar/{ESTADO_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Estado estado = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select ESTADO_ID, ESTADO_NOME from ESTADOS";
                        command.Parameters.AddWithValue("ESTADO_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            estado = new Estado()
                            {
                                Id = reader["ESTADO_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ESTADO_ID"]),
                                Nome = reader["ESTADO_NOME"] == DBNull.Value ? string.Empty : reader["ESTADO_NOME"].ToString(),

                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, estado);
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
        [Route("Deletar/{ESTADO_ID:int}")]
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
                        command.CommandText = "delete from ESTADOS where ESTADO_ID = @id";
                        command.Parameters.AddWithValue("ESTADO_ID", id);

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
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Estado estado)
        {
            try
            {
                bool resultado = false;

                if (estado == null) throw new ArgumentNullException("ESTADOS");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into ESTADOS(ESTADO_NOME) values(@nome)";

                        command.Parameters.AddWithValue("nome", estado.Nome);


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
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{ESTADO_ID:int}")]
        public ActionResult Put(int id, Estado estado)
        {
            try
            {
                bool resultado = false;

                if (estado == null) throw new ArgumentNullException("ESTADOS");
                if (id == 0) throw new ArgumentNullException("ESTADO_ID");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update ESTADOS set ESTADO_NOME = @nome where ESTADO_ID = @id";

                        command.Parameters.AddWithValue("nome", estado.Nome);


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
