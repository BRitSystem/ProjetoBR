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
    public class PerfilController : ControllerBase
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
                List<Perfil> LstPerfil = new List<Perfil>();

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select PERF_ID, PERF_NOME,PERF_CADASTRO,PERF_PRESENCA, PERF_FINANCEIRO from PERFIS";

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Perfil perfil = new Perfil()
                            {
                                Id = reader["PERF_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PERF_ID"]),
                                Nome = reader["PERF_NOME"] == DBNull.Value ? string.Empty : reader["PERF_NOME"].ToString(),
                                Cadastro = (bool)reader["PERF_CADASTRO"],
                                Financeiro = (bool)reader["PERF_FINANCEIRO"],
                                Presenca = (bool)reader["PERF_PRESENCA"]

                            };

                            LstPerfil.Add(perfil);
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, LstPerfil.ToArray());
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
        [Route("Pesquisar/{PERF_ID:int}")]
        public ActionResult GetById(int id)
        {
            try
            {
                Perfil perfil = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select PERF_ID, PERF_NOME,PERF_CADASTRO,PERF_PRESENCA, PERF_FINANCEIRO from PERFIS";
                        command.Parameters.AddWithValue("PERF_ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            perfil = new Perfil()
                            {
                                Id = reader["PERF_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PERF_ID"]),
                                Nome = reader["PERF_NOME"] == DBNull.Value ? string.Empty : reader["PERF_NOME"].ToString(),
                                Cadastro = (bool)reader["PERF_CADASTRO"],
                                Financeiro = (bool)reader["PERF_FINANCEIRO"],
                                Presenca = (bool)reader["PERF_PRESENCA"]
                            };
                        }
                    }

                    connection.Close();
                }

                return StatusCode(200, perfil);
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
        [Route("Deletar/{PERF_ID:int}")]
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
                        command.CommandText = "delete from PERFIS where PERF_ID = @id";
                        command.Parameters.AddWithValue("PERF_ID", id);

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
        /// <param name="perfil"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Novo")]
        public ActionResult Post(Perfil perfil)
        {
            try
            {
                bool resultado = false;

                if (perfil == null) throw new ArgumentNullException("PERFIS");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "insert into PERFIS(PERF_NOME,PERF_CADASTRO,PERF_PRESENCA, PERF_FINANCEIRO) values(@nome, @cadastro, @presenca, @financeiro)";

                        command.Parameters.AddWithValue("nome", perfil.Nome);
                        command.Parameters.AddWithValue("cadastro", perfil.Cadastro);
                        command.Parameters.AddWithValue("presenca", perfil.Presenca);
                        command.Parameters.AddWithValue("financeiro", perfil.Financeiro);

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
        /// <param name="perfil"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Atualizar/{PERF_ID:int}")]
        public ActionResult Put(int id, Perfil perfil)
        {
            try
            {
                bool resultado = false;

                if (perfil == null) throw new ArgumentNullException("PERFIS");
                if (id == 0) throw new ArgumentNullException("id");

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "update PERFIS set PERF_NOME = @nome ,PERF_CADASTRO = @cadastro ,PERF_PRESENCA = @presenca , PERF_FINANCEIRO = @financeiro where PERF_ID = @id";

                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("nome", perfil.Nome);
                        command.Parameters.AddWithValue("cadastro", perfil.Cadastro);
                        command.Parameters.AddWithValue("presenca", perfil.Presenca);
                        command.Parameters.AddWithValue("financeiro", perfil.Financeiro);


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