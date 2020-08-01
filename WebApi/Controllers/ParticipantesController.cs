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
    public class ParticipantesController : ControllerBase
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
                List<Participantes> LstParticipantes = new List<Participantes>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select PART_ID, PART_NOME, PART_ENDERECO, PART_EMAIL from PARTICIPANTES";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Participantes participantes = new Participantes()
                            {
                                Id = reader["PART_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PART_ID"]),
                                Nome = reader["PART_NOME"] == DBNull.Value ? string.Empty : reader["PART_NOME"].ToString(),
                                Endereco = reader["PART_ENDERECO"] == DBNull.Value ? string.Empty : reader["PART_ENDERECO"].ToString(),
                                Email = reader["PART_EMAIL"] == DBNull.Value ? string.Empty : reader["PART_EMAIL"].ToString()

                            };

                            LstParticipantes.Add(participantes);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstParticipantes.ToArray());
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
        [Route("Pesquisar/{PART_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Participantes participantes = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select PART_ID, PART_NOME, PART_ENDERECO, PART_EMAIL from PARTICIPANTES";
                        command.Parameters.AddWithValue("PART_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            participantes = new Participantes()
                            {
                                Id = reader["PART_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PART_ID"]),
                                Nome = reader["PART_NOME"] == DBNull.Value ? string.Empty : reader["PART_NOME"].ToString(),
                                Endereco = reader["PART_ENDERECO"] == DBNull.Value ? string.Empty : reader["PART_ENDERECO"].ToString(),
                                Email = reader["PART_EMAIL"] == DBNull.Value ? string.Empty : reader["PART_EMAIL"].ToString()

                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, participantes);
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
        [Route("Deletar/{PART_ID:int}")]
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
                        command.CommandText = "delete from PARTICIPANTES where PART_ID = @id";
                        command.Parameters.AddWithValue("PART_ID", id);

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
        /// <param name="participantes"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Participantes participantes)
        {
            try
            {
                bool resultado = false;

                if (participantes == null) throw new ArgumentNullException("PARTICIPANTES");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into PARTICIPANTES(PART_NOME, PART_ENDERECO, PART_EMAIL) values(@nome, @endereco, @email)";

                        command.Parameters.AddWithValue("nome", participantes.Nome);
                        command.Parameters.AddWithValue("email", participantes.Email);
                        command.Parameters.AddWithValue("endereco", participantes.Endereco);


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
        /// <param name="participantes"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{PART_ID:int}")]
        public ActionResult Put(int id, Participantes participantes)
        {
            try
            {
                bool resultado = false;

                if (participantes == null) throw new ArgumentNullException("PARTICIPANTES");
                if (id == 0) throw new ArgumentNullException("PART_ID");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update PARTICIPANTES set PART_NOME = @nome, PART_EMAIL = @email, PART_ENDERECO = @endereco where PART_ID = @id";

                        command.Parameters.AddWithValue("nome", participantes.Nome);
                        command.Parameters.AddWithValue("email", participantes.Email);
                        command.Parameters.AddWithValue("endereco", participantes.Endereco);

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