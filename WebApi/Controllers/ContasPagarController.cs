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
    public class ContasPagarController : ControllerBase
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
                List<Contaspagar> LstContaspagar = new List<Contaspagar>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select CONTPA_ID, CONTPA_NOME, CONTPA_DTPAGAMENTO, CONTPA_VLPAGAMENTO, CONTRE_PAGO from CONTAS_A_PAGAR";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Contaspagar contaspagar = new Contaspagar()
                            {
                                Id = reader["CONTPA_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CONTPA_ID"]),
                                Nome = reader["CONTPA_NOME"] == DBNull.Value ? string.Empty : reader["CONTPA_NOME"].ToString(),
                                Dtpagamento = reader["CONTPA_DTPAGAMENTO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CONTPA_DTPAGAMENTO"]),
                                Valorpagamento = reader["CONTPA_VLPAGAMENTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CONTPA_VLPAGAMENTO"]),
                                Pago = reader["CONTRE_PAGO"] == DBNull.Value

                            };

                            LstContaspagar.Add(contaspagar);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstContaspagar.ToArray());
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
        [Route("Pesquisar/{CONTPA_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Contaspagar contaspagar = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select CONTPA_ID, CONTPA_NOME, CONTPA_DTPAGAMENTO, CONTPA_VLPAGAMENTO, CONTRE_PAGO from CONTAS_A_PAGAR";
                        command.Parameters.AddWithValue("CONTPA_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            contaspagar = new Contaspagar()
                            {
                                Id = reader["CONTPA_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CONTPA_ID"]),
                                Nome = reader["CONTPA_NOME"] == DBNull.Value ? string.Empty : reader["CONTPA_NOME"].ToString(),
                                Dtpagamento = reader["CONTPA_DTPAGAMENTO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CONTPA_DTPAGAMENTO"]),
                                Valorpagamento = reader["CONTPA_VLPAGAMENTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CONTPA_VLPAGAMENTO"]),
                                Pago = reader["CONTRE_PAGO"] == DBNull.Value
                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, contaspagar);
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
        [Route("Deletar/{CONTPA_ID:int}")]
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
                        command.CommandText = "delete from CONTAS_A_PAGAR where CONTPA_ID = @id";
                        command.Parameters.AddWithValue("CONTPA_ID", id);

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
        /// <param name="Contaspagar"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Contaspagar contaspagar)
        {
            try
            {
                bool resultado = false;

                if (contaspagar == null) throw new ArgumentNullException("CONTAS_A_PAGAR");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into CONTAS_A_PAGAR(CONTPA_NOME, CONTPA_DTPAGAMENTO, CONTPA_VLPAGAMENTO, CONTPA_PAGO) values(@nome, @dtpagamento @vlpagamento @pago)";

                        command.Parameters.AddWithValue("nome", contaspagar.Nome);
                        command.Parameters.AddWithValue("dtpagamento", contaspagar.Dtpagamento);
                        command.Parameters.AddWithValue("vlpagamento", contaspagar.Valorpagamento);
                        command.Parameters.AddWithValue("pago", contaspagar.Pago);

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
        /// <param name="CONTAS_A_PAGAR"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{CONTPA_ID:int}")]
        public ActionResult Put(int id, Contasareceber contaspagar)
        {
            try
            {
                bool resultado = false;

                if (contaspagar == null) throw new ArgumentNullException("CONTAS_A_PAGAR");
                if (id == 0) throw new ArgumentNullException("id");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update clientes set CONTPA_NOME = @nome, CONTPA_DTPAGAMENTO = @dtpagamento, CONTPA_VLPAGAMENTO = @vlpagamento, CONTPA_PAGO = @pago where CONTPA_ID = @id";
                        
                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("nome", contaspagar.Nome);
                        command.Parameters.AddWithValue("dtpagamento", contaspagar.Dtpagamento);
                        command.Parameters.AddWithValue("vlpagamento", contaspagar.Valorlancamento);
                        command.Parameters.AddWithValue("pago", contaspagar.Pago);

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
