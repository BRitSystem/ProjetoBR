using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Administration;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ClientesController : ControllerBase
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
                List<Cliente> lstClientes = new List<Cliente>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select id, nome, data_nascimento, email from clientes";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente()
                            {
                                Id = reader["id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["id"]),
                                Nome = reader["nome"] == DBNull.Value ? string.Empty : reader["nome"].ToString(),
                                DataNascimento = reader["data_nascimento"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["data_nascimento"]),
                                Email = reader["email"] == DBNull.Value ? string.Empty : reader["email"].ToString()
                            };

                            lstClientes.Add(cliente);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, lstClientes.ToArray());
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
        [Route("Pesquisar/{id:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Cliente cliente = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select id, nome, data_nascimento, email from clientes where id = @id";
                        command.Parameters.AddWithValue("id", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            cliente = new Cliente()
                            {
                                Id = reader["id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["id"]),
                                Nome = reader["nome"] == DBNull.Value ? string.Empty : reader["nome"].ToString(),
                                DataNascimento = reader["data_nascimento"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["data_nascimento"]),
                                Email = reader["email"] == DBNull.Value ? string.Empty : reader["email"].ToString()
                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, cliente);
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
        [Route("Deletar/{id:int}")]
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
                        command.CommandText = "delete from clientes where id = @id";
                        command.Parameters.AddWithValue("id", id);

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
        /// <param name="cliente"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Cliente cliente)
        {
            try
            {
                bool resultado = false;

                if (cliente == null) throw new ArgumentNullException("cliente");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into clientes(nome, data_nascimento, email) values(@nome, @data_nascimento, @email)";

                        command.Parameters.AddWithValue("nome", cliente.Nome);
                        command.Parameters.AddWithValue("data_nascimento", cliente.DataNascimento);
                        command.Parameters.AddWithValue("email", cliente.Email);

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
        /// <param name="cliente"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{id:int}")]
        public ActionResult Put(int id, Cliente cliente)
        {
            try
            {
                bool resultado = false;

                if (cliente == null) throw new ArgumentNullException("cliente");
                if (id == 0) throw new ArgumentNullException("id");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update clientes set nome = @nome, data_nascimento = @data_nascimento, email = @email where id = @id";

                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("nome", cliente.Nome);
                        command.Parameters.AddWithValue("data_nascimento", cliente.DataNascimento);
                        command.Parameters.AddWithValue("email", cliente.Email);

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
