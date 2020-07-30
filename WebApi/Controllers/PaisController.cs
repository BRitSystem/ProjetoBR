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
    public class PaisController : ControllerBase
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
                List<Pais> LstPais = new List<Pais>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select PAIS_ID, PAIS_NOME from PAISES";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Pais pais = new Pais()
                            {
                                Id = reader["PAIS_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PAIS_ID"]),
                                Nome = reader["PAIS_NOME"] == DBNull.Value ? string.Empty : reader["PAIS_NOME"].ToString(),

                            };

                            LstPais.Add(pais);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstPais.ToArray());
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
        [Route("Pesquisar/{PAIS_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Pais pais = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select PAIS_ID, PAIS_NOME from PAISES";
                        command.Parameters.AddWithValue("PAIS_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            pais = new Pais()
                            {
                                Id = reader["PAIS_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PAIS_ID"]),
                                Nome = reader["PAIS_NOME"] == DBNull.Value ? string.Empty : reader["PAIS_NOME"].ToString(),

                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, pais);
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
        [Route("Deletar/{PAIS_ID:int}")]
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
                        command.CommandText = "delete from PAISES where PAIS_ID = @id";
                        command.Parameters.AddWithValue("PAIS_ID", id);

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
        /// <param name="pais"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Pais pais)
        {
            try
            {
                bool resultado = false;

                if (pais == null) throw new ArgumentNullException("PAISES");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into PAISES(PAIS_NOME) values(@nome)";

                        command.Parameters.AddWithValue("nome", pais.Nome);


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
        /// <param name="pais"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{PAIS_ID:int}")]
        public ActionResult Put(int id, Pais pais)
        {
            try
            {
                bool resultado = false;

                if (pais == null) throw new ArgumentNullException("PAISES");
                if (id == 0) throw new ArgumentNullException("PAIS_ID");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update PAISES set PAIS_NOME = @nome where PAIS_ID = @id";

                        command.Parameters.AddWithValue("nome", pais.Nome);


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