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
    public class CategoriaController : ControllerBase
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
                List<Categoria> LstCategoria = new List<Categoria>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select CAT_ID, CAT_NOME from CATEGORIAS";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Categoria categoria = new Categoria()
                            {
                                Id = reader["CAT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CAT_ID"]),
                                Nome = reader["CAT_NOME"] == DBNull.Value ? string.Empty : reader["CAT_NOME"].ToString(),

                            };

                            LstCategoria.Add(categoria);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstCategoria.ToArray());
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
        [Route("Pesquisar/{CAT_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Categoria categoria = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select CAT_ID, CAT_NOME from CATEGORIAS";
                        command.Parameters.AddWithValue("CAT_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            categoria = new Categoria()
                            {
                                Id = reader["CAT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CAT_ID"]),
                                Nome = reader["CAT_NOME"] == DBNull.Value ? string.Empty : reader["CAT_NOME"].ToString(),

                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, categoria);
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
        [Route("Deletar/{CAT_ID:int}")]
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
                        command.CommandText = "delete from MEMBROS where CAT_ID = @id";
                        command.Parameters.AddWithValue("CAT_ID", id);

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
        /// <param name="categoria"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Categoria categoria)
        {
            try
            {
                bool resultado = false;

                if (categoria == null) throw new ArgumentNullException("CATEGORIAS");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into CATEGORIAS(CAT_NOME) values(@nome)";

                        command.Parameters.AddWithValue("nome", categoria.Nome);


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
        /// <param name="categoria"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{CAT_ID:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            try
            {
                bool resultado = false;

                if (categoria == null) throw new ArgumentNullException("CATEGORIAS");
                if (id == 0) throw new ArgumentNullException("CAT_ID");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update CATEGORIAS set CAT_NOME = @nome where CAT_ID = @id";

                        command.Parameters.AddWithValue("nome", categoria.Nome);


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
