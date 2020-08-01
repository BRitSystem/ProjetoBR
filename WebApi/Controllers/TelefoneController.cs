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
    public class TelefoneController : ControllerBase
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
                List<Telefone> LstTelefone = new List<Telefone>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select TEL_ID, TEL_DDD, TEL_NUMERO, TEL_WHATSAPP  from TELEFONES";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Telefone telefone = new Telefone()
                            {
                                Id = reader["TEL_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TEL_ID"]),
                                Ddd = reader["TEL_DDD"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TEL_DDD"]),
                                Numero = reader["TEL_NUMERO"] == DBNull.Value ? string.Empty : reader["TEL_NUMERO"].ToString(),
                                Whatsapp = (bool)reader["TEL_WHATSAPP"]

                            };

                            LstTelefone.Add(telefone);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstTelefone.ToArray());
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
        [Route("Pesquisar/{TEL_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Telefone telefone = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select TEL_ID, TEL_DDD, TEL_NUMERO, TEL_WHATSAPP from TELEFONES";
                        command.Parameters.AddWithValue("TEL_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            telefone = new Telefone()
                            {
                                Id = reader["TEL_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TEL_ID"]),
                                Ddd = reader["TEL_DDD"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TEL_DDD"]),
                                Numero = reader["TEL_NUMERO"] == DBNull.Value ? string.Empty : reader["TEL_NUMERO"].ToString(),
                                Whatsapp = (bool)reader["TEL_WHATSAPP"]

                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, telefone);
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
        [Route("Deletar/{TEL_ID:int}")]
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
                        command.CommandText = "delete from TELEFONES where TEL_ID = @id";
                        command.Parameters.AddWithValue("TEL_ID", id);

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
        /// <param name="telefone"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Telefone telefone)
        {
            try
            {
                bool resultado = false;

                if (telefone == null) throw new ArgumentNullException("TELEFONES");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into TELEFONES(TEL_DDD, TEL_NUMERO, TEL_WHATSAPP) values(@ddd, @numero, @whatsapp)";

                        command.Parameters.AddWithValue("ddd", telefone.Ddd);
                        command.Parameters.AddWithValue("numero", telefone.Numero);
                        command.Parameters.AddWithValue("whatsapp", telefone.Whatsapp);


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
        /// <param name="telefone"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{TEL_ID:int}")]
        public ActionResult Put(int id, Telefone telefone)
        {
            try
            {
                bool resultado = false;

                if (telefone == null) throw new ArgumentNullException("TELEFONES");
                if (id == 0) throw new ArgumentNullException("TEL_ID");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update TELEFONES set TEL_DDD = @ddd, TEL_NUMERO = @numero, TEL_WHATSAPP = @whatsapp where TEL_ID = @id";

                        command.Parameters.AddWithValue("ddd", telefone.Ddd);
                        command.Parameters.AddWithValue("numero", telefone.Numero);
                        command.Parameters.AddWithValue("whatsapp", telefone.Whatsapp);

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