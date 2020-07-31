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
    public class CidadeController : ControllerBase
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
                List<Cidade> LstCidade = new List<Cidade>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select CID_ID, CID_NOME from CIDADES";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Cidade cidade = new Cidade()
                            {
                                Id = reader["CID_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CID_ID"]),
                                Nome = reader["CID_NOME"] == DBNull.Value ? string.Empty : reader["CID_NOME"].ToString(),

                            };

                            LstCidade.Add(cidade);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstCidade.ToArray());
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
        [Route("Pesquisar/{CID_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Cidade cidade = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select CID_ID, CID_NOME from CIDADES";
                        command.Parameters.AddWithValue("CID_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            cidade = new Cidade()
                            {
                                Id = reader["CID_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CID_ID"]),
                                Nome = reader["CID_NOME"] == DBNull.Value ? string.Empty : reader["CID_NOME"].ToString(),

                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, cidade);
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
        [Route("Deletar/{CID_ID:int}")]
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
                        command.CommandText = "delete from CIDADES where CID_ID = @id";
                        command.Parameters.AddWithValue("CID_ID", id);

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
        /// <param name="cidade"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Cidade cidade)
        {
            try
            {
                bool resultado = false;

                if (cidade == null) throw new ArgumentNullException("CIDADES");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into CIDADES(CID_NOME) values(@nome)";

                        command.Parameters.AddWithValue("nome", cidade.Nome);


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
        /// <param name="cidade"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{CID_ID:int}")]
        public ActionResult Put(int id, Cidade cidade)
        {
            try
            {
                bool resultado = false;

                if (cidade == null) throw new ArgumentNullException("CIDADES");
                if (id == 0) throw new ArgumentNullException("CID_ID");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update CATEGORIAS set CID_NOME = @nome where CID_ID = @id";

                        command.Parameters.AddWithValue("nome", cidade.Nome);


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
