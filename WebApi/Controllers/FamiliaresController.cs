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
    public class FamiliaresController : ControllerBase
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
                List<Familiares> LstFamiliares = new List<Familiares>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select FAMIL_ID, FAMIL_NOME from FAMILIARES";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Familiares familiares = new Familiares()
                            {
                                Id = reader["FAMIL_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FAMIL_ID"]),
                                Nome = reader["FAMIL_NOME"] == DBNull.Value ? string.Empty : reader["FAMIL_NOME"].ToString(),

                            };

                            LstFamiliares.Add(familiares);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstFamiliares.ToArray());
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
        [Route("Pesquisar/{FAMIL_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Familiares familiares = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select FAMIL_ID, FAMIL_NOME from FAMILIARES";
                        command.Parameters.AddWithValue("FAMIL_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            familiares = new Familiares()
                            {
                                Id = reader["FAMIL_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FAMIL_ID"]),
                                Nome = reader["FAMIL_NOME"] == DBNull.Value ? string.Empty : reader["FAMIL_NOME"].ToString(),

                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, familiares);
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
        [Route("Deletar/{FAMIL_ID:int}")]
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
                        command.CommandText = "delete from FAMILIARES where FAMIL_ID = @id";
                        command.Parameters.AddWithValue("FAMIL_ID", id);

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
        /// <param name="familiares"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Familiares familiares)
        {
            try
            {
                bool resultado = false;

                if (familiares == null) throw new ArgumentNullException("FAMILIARES");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into FAMILIARES(FAMIL_NOME) values(@nome)";

                        command.Parameters.AddWithValue("nome", familiares.Nome);


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
        /// <param name="familiares"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{FAMIL_ID:int}")]
        public ActionResult Put(int id, Familiares familiares)
        {
            try
            {
                bool resultado = false;

                if (familiares == null) throw new ArgumentNullException("FAMILIARES");
                if (id == 0) throw new ArgumentNullException("FAMIL_ID");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update FAMILIARES set FAMIL_NOME = @nome where FAMIL_ID = @id";

                        command.Parameters.AddWithValue("nome", familiares.Nome);


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