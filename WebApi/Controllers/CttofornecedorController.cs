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
    public class CttofornecedorController : ControllerBase
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
                List<Contatofornecedor> LstCttofornecedor = new List<Contatofornecedor>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select CTTOFORN_ID, CTTOFORN_NOME, CTTOFORN_EMAIL from CONTATOS_FORNECEDORES";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Contatofornecedor cttofornecedor = new Contatofornecedor()
                            {
                                Id = reader["CTTOFORN_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CTTOFORN_ID"]),
                                Nome = reader["CTTOFORN_NOME"] == DBNull.Value ? string.Empty : reader["CTTOFORN_NOME"].ToString(),
                                Email = reader["CTTOFORN_EMAIL"] == DBNull.Value ? string.Empty : reader["CTTOFORN_EMAIL"].ToString()

                            };

                            LstCttofornecedor.Add(cttofornecedor);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstCttofornecedor.ToArray());
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
        [Route("Pesquisar/{CTTOFORN_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Contatofornecedor cttofornecedor = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select CTTOFORN_ID, CTTOFORN_NOME, CTTOFORN_EMAIL from CONTATOS_FORNECEDORES";
                        command.Parameters.AddWithValue("CTTOFORN_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            cttofornecedor = new Contatofornecedor()
                            {
                                Id = reader["CTTOFORN_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CTTOFORN_ID"]),
                                Nome = reader["CTTOFORN_NOME"] == DBNull.Value ? string.Empty : reader["CTTOFORN_NOME"].ToString(),
                                Email = reader["CTTOFORN_EMAIL"] == DBNull.Value ? string.Empty : reader["CTTOFORN_EMAIL"].ToString()

                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, cttofornecedor);
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
        [Route("Deletar/{CTTOFORN_ID:int}")]
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
                        command.CommandText = "delete from CONTATOS_FORNECEDORES where CTTOFORN_ID = @id";
                        command.Parameters.AddWithValue("CTTOFORN_ID", id);

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
        /// <param name="cttofornecedor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Contatofornecedor cttofornecedor)
        {
            try
            {
                bool resultado = false;

                if (cttofornecedor == null) throw new ArgumentNullException("CONTATOS_FORNECEDORES");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into CONTATOS_FORNECEDORES(CTTOFORN_NOME,CTTOFORN_EMAIL) values(@nome, @email)";

                        command.Parameters.AddWithValue("nome", cttofornecedor.Nome);
                        command.Parameters.AddWithValue("email", cttofornecedor.Email);


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
        /// <param name="cttofornecedor"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{CTTOFORN_ID:int}")]
        public ActionResult Put(int id, Contatofornecedor cttofornecedor)
        {
            try
            {
                bool resultado = false;

                if (cttofornecedor == null) throw new ArgumentNullException("CONTATOS_FORNECEDORES");
                if (id == 0) throw new ArgumentNullException("CTTOFORN_ID");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update CONTATOS_FORNECEDORES set CTTOFORN_NOME = @nome, CTTOFORN_EMAIL = @email where CTTOFORN_ID = @id";

                        command.Parameters.AddWithValue("nome", cttofornecedor.Nome);
                        command.Parameters.AddWithValue("email", cttofornecedor.Email);


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
