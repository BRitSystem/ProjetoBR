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
    public class ProdutoController : ControllerBase
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
                List<Produto> LstProduto = new List<Produto>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select PROD_ID, PROD_NOME, PROD_QTPRODUTO, PROD_VALOR  from PRODUTOS";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Produto produto = new Produto()
                            {
                                Id = reader["PROD_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_ID"]),
                                Nome = reader["PROD_NOME"] == DBNull.Value ? string.Empty : reader["PROD_NOME"].ToString(),
                                Qtproduto = reader["PROD_QTPRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_QTPRODUTO"]),
                                Valor = reader["PROD_VALOR"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PROD_VALOR"])

                            };

                            LstProduto.Add(produto);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstProduto.ToArray());
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
        [Route("Pesquisar/{PROD_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Produto produto = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select PROD_ID, PROD_NOME, PROD_QTPRODUTO, PROD_VALOR from PRODUTOS";
                        command.Parameters.AddWithValue("PROD_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            produto = new Produto()
                            {
                                Id = reader["PROD_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_ID"]),
                                Nome = reader["PROD_NOME"] == DBNull.Value ? string.Empty : reader["PROD_NOME"].ToString(),
                                Qtproduto = reader["PROD_QTPRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PROD_QTPRODUTO"]),
                                Valor = reader["PROD_VALOR"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PROD_VALOR"])

                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, produto);
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
        [Route("Deletar/{PROD_ID:int}")]
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
                        command.CommandText = "delete from PRODUTOS where PROD_ID = @id";
                        command.Parameters.AddWithValue("PROD_ID", id);

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
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Produto produto)
        {
            try
            {
                bool resultado = false;

                if (produto == null) throw new ArgumentNullException("PRODUTOS");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into PRODUTOS(PROD_NOME, PROD_QTPRODUTO, PROD_VALOR) values(@nome, @qtproduto, @valor)";

                        command.Parameters.AddWithValue("nome", produto.Nome);
                        command.Parameters.AddWithValue("qtproduto", produto.Qtproduto);
                        command.Parameters.AddWithValue("valor", produto.Valor);


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
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{PROD_ID:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            try
            {
                bool resultado = false;

                if (produto == null) throw new ArgumentNullException("PRODUTOS");
                if (id == 0) throw new ArgumentNullException("PROD_ID");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update PRODUTOS set PROD_NOME = @nome, PROD_QTPRODUTO = @qtproduto, PROD_VALOR = @valor where PROD_ID = @id";

                        command.Parameters.AddWithValue("nome", produto.Nome);
                        command.Parameters.AddWithValue("qtproduto", produto.Qtproduto);
                        command.Parameters.AddWithValue("valor", produto.Valor);

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