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
    public class ContasReceberController : ControllerBase
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
                List<Contasareceber> LstContasareceber = new List<Contasareceber>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select CONTRE_ID, CONTRE_NOME,CONTRE_DTLANCAMENTO, CONTRE_DTPAGAMENTO, CONTRE_VALORLANCADO, CONTRE_PAGO from CONTAS_RECEBER";

        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Contasareceber contasreceber = new Contasareceber()
                            {
                                Id = reader["CONTRE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CONTRE_ID"]),
                                Nome = reader["CONTRE_NOME"] == DBNull.Value ? string.Empty : reader["CONTRE_NOME"].ToString(),
                                Dtlancamento = reader["CONTRE_DTLANCAMENTO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CONTRE_DTLANCAMENTO"]),
                                Dtpagamento = reader["CONTRE_DTPAGAMENTO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CONTRE_DTPAGAMENTO"]),
                                Valorlancamento = reader["CONTRE_VALORLANCADO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CONTRE_VALORLANCADO"]),
                                Pago = reader["BoolAprovacao"] == DBNull.Value

                            };

                            LstContasareceber.Add(contasreceber);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstContasareceber.ToArray());
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
        [Route("Pesquisar/{CONTRE_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Contasareceber contasreceber = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select CONTRE_ID, CONTRE_NOME,CONTRE_DTLANCAMENTO, CONTRE_DTPAGAMENTO, CONTRE_VALORLANCADO, CONTRE_PAGO from CONTAS_RECEBER";
                        command.Parameters.AddWithValue("CONTRE_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            contasreceber = new Contasareceber()
                            {
                                Id = reader["CONTRE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CONTRE_ID"]),
                                Nome = reader["CONTRE_NOME"] == DBNull.Value ? string.Empty : reader["CONTRE_NOME"].ToString(),
                                Dtlancamento = reader["CONTRE_DTLANCAMENTO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CONTRE_DTLANCAMENTO"]),
                                Dtpagamento = reader["CONTRE_DTPAGAMENTO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CONTRE_DTPAGAMENTO"]),
                                Valorlancamento = reader["CONTRE_VALORLANCADO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CONTRE_VALORLANCADO"]),
                                Pago = reader["BoolAprovacao"] == DBNull.Value
                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, contasreceber);
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
        [Route("Deletar/{CONTRE_ID:int}")]
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
                        command.CommandText = "delete from CONTAS_RECEBER where CONTRE_ID = @id";
                        command.Parameters.AddWithValue("CONTRE_ID", id);

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
        /// <param name="contasreceber"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Contasareceber contasreceber)
        {
            try
            {
                bool resultado = false;

                if (contasreceber == null) throw new ArgumentNullException("CONTAS_RECEBER");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into CONTAS_RECEBER(CONTRE_NOME,CONTRE_DTLANCAMENTO, CONTRE_DTPAGAMENTO, CONTRE_VALORLANCADO, CONTRE_PAGO ) values(@nome, @dtnascimento, @dtpagamento @valorlancado @pago)";

                        command.Parameters.AddWithValue("nome", contasreceber.Nome);
                        command.Parameters.AddWithValue("cpf", contasreceber.Dtlancamento);
                        command.Parameters.AddWithValue("endereco", contasreceber.Dtpagamento);
                        command.Parameters.AddWithValue("data_nascimento", contasreceber.Valorlancamento);
                        command.Parameters.AddWithValue("email", contasreceber.Pago);

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
        /// <param name="CONTAS_RECEBER"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{CONTRE_ID:int}")]
        public ActionResult Put(int id, Contasareceber contasreceber)
        {
            try
            {
                bool resultado = false;

                if (contasreceber == null) throw new ArgumentNullException("CONTAS_RECEBER");
                if (id == 0) throw new ArgumentNullException("id");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update clientes set CONTRE_NOME = @nome, CONTRE_DTLANCAMENTO = @dtlancamento, CONTRE_DTPAGAMENTO = @dtpagamento, CONTRE_VALORLANCADO = @valorlancado, CONTRE_PAGO = @pago where CONTRE_ID = @id";

                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("nome", contasreceber.Nome);
                        command.Parameters.AddWithValue("dtlancamento", contasreceber.Dtlancamento);
                        command.Parameters.AddWithValue("dtpagamento", contasreceber.Dtpagamento);
                        command.Parameters.AddWithValue("valorlancado", contasreceber.Valorlancamento);
                        command.Parameters.AddWithValue("pago", contasreceber.Pago);

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