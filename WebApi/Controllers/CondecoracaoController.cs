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
    public class CondecoracaoController : ControllerBase
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
                List<Condecoracoes> LstCondecoracoes = new List<Condecoracoes>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select COND_ID, COND_NOME, COND_DATA from CONDECORACOES";
                     
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Condecoracoes condecoracoes = new Condecoracoes()
                            {
                                Id = reader["COND_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["COND_ID"]),
                                Nome = reader["COND_NOME"] == DBNull.Value ? string.Empty : reader["COND_NOME"].ToString(),
                                Data = reader["COND_DATA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["COND_DATA"]),


                            };

                            LstCondecoracoes.Add(condecoracoes);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstCondecoracoes.ToArray());
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
        [Route("Pesquisar/{COND_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Condecoracoes condecoracoes = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select COND_ID, COND_NOME, COND_DATA from CONDECORACOES";
                        command.Parameters.AddWithValue("COND_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            condecoracoes = new Condecoracoes()
                            {
                                Id = reader["COND_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["COND_ID"]),
                                Nome = reader["COND_NOME"] == DBNull.Value ? string.Empty : reader["COND_NOME"].ToString(),
                                Data = reader["COND_DATA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["COND_DATA"]),

                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, condecoracoes);
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
        [Route("Deletar/{COND_ID:int}")]
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
                        command.CommandText = "delete from CONDECORACOES where COND_ID = @id, COND_NOME = nome, COND_DATA = data";
                        command.Parameters.AddWithValue("COND_ID", id);

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
        /// <param name="Condecoracoes"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Condecoracoes condecoracoes)
        {
            try
            {
                bool resultado = false;

                if (condecoracoes == null) throw new ArgumentNullException("CONDECORACOES");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into CONDECORACOES(COND_NOME,COND_DATA) values(@nome, @data)";

                        command.Parameters.AddWithValue("nome", condecoracoes.Nome);
                        command.Parameters.AddWithValue("data", condecoracoes.Data);

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
        /// <param name="Condecoracoes"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{COND_ID:int}")]
        public ActionResult Put(int id, Condecoracoes condecoracoes)
        {
            try
            {
                bool resultado = false;

                if (condecoracoes == null) throw new ArgumentNullException("CONDECORACOES");
                if (id == 0) throw new ArgumentNullException("id");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update CONDECORACOES set COND_NOME = @nome, COND_DATA = @data where COND_ID = @id";

                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("nome", condecoracoes.Nome);
                        command.Parameters.AddWithValue("dtevento", condecoracoes.Data);

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
