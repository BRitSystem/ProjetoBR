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
    public class TpPagamentoController : ControllerBase
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
                List<Tipodepagamento> LstTpPagamento = new List<Tipodepagamento>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select TPPAG_ID, TPPAG_NOME from TIPO_DE_PAGAMENTOS";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Tipodepagamento tppagamento = new Tipodepagamento()
                            {
                                Id = reader["TPPAG_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TPPAG_ID"]),
                                Nome = reader["TPPAG_NOME"] == DBNull.Value ? string.Empty : reader["TPPAG_NOME"].ToString(),

                            };

                            LstTpPagamento.Add(tppagamento);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstTpPagamento.ToArray());
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
        [Route("Pesquisar/{TPPAG_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Tipodepagamento tppagamento = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select TPPAG_ID, TPPAG_NOME from TIPO_DE_PAGAMENTOS";
                        command.Parameters.AddWithValue("TPPAG_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            tppagamento = new Tipodepagamento()
                            {
                                Id = reader["TPPAG_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TPPAG_ID"]),
                                Nome = reader["TPPAG_NOME"] == DBNull.Value ? string.Empty : reader["TPPAG_NOME"].ToString(),

                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, tppagamento);
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
        [Route("Deletar/{TPPAG_ID:int}")]
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
                        command.CommandText = "delete from TIPO_DE_PAGAMENTOS where TPPAG_ID = @id";
                        command.Parameters.AddWithValue("TPPAG_ID", id);

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
        /// <param name="TIPO_DE_PAGAMENTOS"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Tipodepagamento tppagamento)
        {
            try
            {
                bool resultado = false;

                if (tppagamento == null) throw new ArgumentNullException("TIPO_DE_PAGAMENTOS");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into TIPO_DE_PAGAMENTOS(TPPAG_NOME) values(@nome)";

                        command.Parameters.AddWithValue("nome", tppagamento.Nome);


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
        /// <param name="TIPO_DE_PAGAMENTOS"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{TPPAG_ID:int}")]
        public ActionResult Put(int id, Tipodepagamento tppagamento)
        {
            try
            {
                bool resultado = false;

                if (tppagamento == null) throw new ArgumentNullException("TIPO_DE_PAGAMENTOS");
                if (id == 0) throw new ArgumentNullException("TPPAG_ID");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update TIPO_DE_PAGAMENTOS set TPPAG_NOME = @nome where TPPAG_ID = @id";

                        command.Parameters.AddWithValue("nome", tppagamento.Nome);


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
