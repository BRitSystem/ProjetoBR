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
    public class MembrosController : ControllerBase
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
                List<Membros> LstMembros = new List<Membros>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select MEMB_ID, MEMB_NOME,MEMB_CPF,MEMB_ENDERECO, MEMB_DTNASCIMENTO, MEMB_EMAIL, MEMB_FOTO from MEMBROS";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Membros membro = new Membros()
                            {
                                Id = reader["MEMB_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MEMB_ID"]),
                                Nome = reader["MEMB_NOME"] == DBNull.Value ? string.Empty : reader["MEMB_NOME"].ToString(),
                                CPF = reader["MEMB_CPF"] == DBNull.Value ? string.Empty : reader["MEMB_CPF"].ToString(),
                                Endereco = reader["MEMB_ENDERECO"] == DBNull.Value ? string.Empty : reader["MEMB_ENDERECO"].ToString(),
                                Dtnascimento = reader["MEMB_DTNASCIMENTO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["MEMB_DTNASCIMENTO"]),
                                Email = reader["MEMB_EMAIL"] == DBNull.Value ? string.Empty : reader["MEMB_EMAIL"].ToString(),
                                Foto = reader["MEMB_FOTO"] == DBNull.Value ? string.Empty :reader["MEMB_FOTO"].ToString()
                            };

                            LstMembros.Add(membro);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstMembros.ToArray());
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
                Membros membro = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select MEMB_ID, MEMB_NOME,MEMB_CPF,MEMB_ENDERECO, MEMB_DTNASCIMENTO, MEMB_EMAIL, MEMB_FOTO from MEMBROS";
                        command.Parameters.AddWithValue("id", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            membro = new Membros()
                            {
                                Id = reader["MEMB_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MEMB_ID"]),
                                Nome = reader["MEMB_NOME"] == DBNull.Value ? string.Empty : reader["MEMB_NOME"].ToString(),
                                CPF = reader["MEMB_CPF"] == DBNull.Value ? string.Empty : reader["MEMB_CPF"].ToString(),
                                Endereco = reader["MEMB_ENDERECO"] == DBNull.Value ? string.Empty : reader["MEMB_ENDERECO"].ToString(),
                                Dtnascimento = reader["MEMB_DTNASCIMENTO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["MEMB_DTNASCIMENTO"]),
                                Email = reader["MEMB_EMAIL"] == DBNull.Value ? string.Empty : reader["MEMB_EMAIL"].ToString(),
                                Foto = reader["MEMB_FOTO"] == DBNull.Value ? string.Empty : reader["MEMB_FOTO"].ToString()
                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, membro);
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
                        command.CommandText = "delete from MEMBROS where MEMB_ID = @id";
                        command.Parameters.AddWithValue("MEMB_ID", id);

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
        /// <param name="membro"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Membros membro)
        {
            try
            {
                bool resultado = false;

                if (membro == null) throw new ArgumentNullException("MEMBROS");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into MEMBROS(MEMB_NOME, MEMB_CPF,MEMB_ENDERECO, MEMB_DTNASCIMENTO, MEMB_EMAIL, MEMB_FOTO) values(@nome, @cpf, @endereco, @data_nascimento, @email, @foto)";

                        command.Parameters.AddWithValue("nome", membro.Nome);
                        command.Parameters.AddWithValue("cpf", membro.CPF);
                        command.Parameters.AddWithValue("endereco", membro.Endereco);
                        command.Parameters.AddWithValue("data_nascimento", membro.Dtnascimento);
                        command.Parameters.AddWithValue("email", membro.Email);
                        command.Parameters.AddWithValue("foto", membro.Foto);

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
        /// <param name="membro"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{id:int}")]
        public ActionResult Put(int id, Membros membro)
        {
            try
            {
                bool resultado = false;

                if (membro == null) throw new ArgumentNullException("cliente");
                if (id == 0) throw new ArgumentNullException("id");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update clientes set MEMB_NOME = @nome, MEMB_CPF = @cpf, MEMB_ENDERECO = @endereco, MEMB_DTNASCIMENTO = @data_nascimento, MEMB_EMAIL = @email, MEMB_FOTO = @foto where MEMB_ID = @id";
                        //MEMB_NOME, MEMB_CPF,MEMB_ENDERECO, MEMB_DTNASCIMENTO, MEMB_EMAIL, MEMB_FOTO
                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("nome", membro.Nome);
                        command.Parameters.AddWithValue("cpf", membro.CPF);
                        command.Parameters.AddWithValue("endereco", membro.Endereco);
                        command.Parameters.AddWithValue("data_nascimento", membro.Dtnascimento);
                        command.Parameters.AddWithValue("email", membro.Email);
                        command.Parameters.AddWithValue("foto", membro.Foto);

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
