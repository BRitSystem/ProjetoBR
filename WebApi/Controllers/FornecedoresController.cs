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
    public class FornecedoresController : ControllerBase
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
                List<Fornecedores> LstFornecedores = new List<Fornecedores>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select FORN_ID, FORN_NOME from FORNECEDORES";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Fornecedores fornecedores = new Fornecedores()
                            {
                                Id = reader["FORN_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FORN_ID"]),
                                Nome = reader["FORN_NOME"] == DBNull.Value ? string.Empty : reader["FORN_NOME"].ToString(),

                            };

                            LstFornecedores.Add(fornecedores);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstFornecedores.ToArray());
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
        [Route("Pesquisar/{FORN_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Fornecedores fornecedores = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select FORN_ID, FORN_NOME from FORNECEDORES";
                        command.Parameters.AddWithValue("FORN_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            fornecedores = new Fornecedores()
                            {
                                Id = reader["FORN_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FORN_ID"]),
                                Nome = reader["FORN_NOME"] == DBNull.Value ? string.Empty : reader["FORN_NOME"].ToString(),

                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, fornecedores);
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
        [Route("Deletar/{FORN_ID:int}")]
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
                        command.CommandText = "delete from FORNECEDORES where FORN_ID = @id";
                        command.Parameters.AddWithValue("FORN_ID", id);

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
        /// <param name="fornecedores"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Fornecedores fornecedores)
        {
            try
            {
                bool resultado = false;

                if (fornecedores == null) throw new ArgumentNullException("FORNECEDORES");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into FORNECEDORES(FORN_NOME) values(@nome)";

                        command.Parameters.AddWithValue("nome", fornecedores.Nome);


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
        /// <param name="fornecedores"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{FORN_ID:int}")]
        public ActionResult Put(int id, Fornecedores fornecedores)
        {
            try
            {
                bool resultado = false;

                if (fornecedores == null) throw new ArgumentNullException("FORNECEDORES");
                if (id == 0) throw new ArgumentNullException("FORN_ID");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update FORNECEDORES set FORN_NOME = @nome where FORN_ID = @id";

                        command.Parameters.AddWithValue("nome", fornecedores.Nome);


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
