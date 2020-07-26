using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsuariosController : ControllerBase
    {
        [HttpPost]        
        [Route("Autenticar/{username}/{password}")]
        
        public ActionResult Authenticate(string username, string password)
        {
            try
            {
                string token = "";
                Usuario usuario = null;

                using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "select USU_ID, USU_USERNAME, USU_PASSWORD, Role from Usuarios where USU_USERNAME = @username and USU_PASSWORD = @password";
                        command.Parameters.AddWithValue("USU_USERNAME", username);
                        command.Parameters.AddWithValue("USU_PASSWORD", password);

                        SqlDataReader reader = command.ExecuteReader();                                                
                        while (reader.Read())
                        {
                            usuario = new Usuario()
                            {
                                Username = reader["USU_USERNAME"] == DBNull.Value ? string.Empty : reader["USU_USERNAME"].ToString(),
                                Role = reader["Role"] == DBNull.Value ? string.Empty : reader["Role"].ToString(),
                                Password = reader["USU_PASSWORD"] == DBNull.Value ? string.Empty : reader["USU_PASSWORD"].ToString(),
                                Id = reader["USU_ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["USU_ID"]),
                            };
                        }
                                                
                    }
                    connection.Close();

                    // Verifica se o usuário existe
                    if (usuario == null)
                        return NotFound(new { message = "Usuário ou senha inválidos" });

                    // Gera o Token
                    token = TokenService.GenerateToken(usuario);

                    // Oculta a senha
                    usuario.Password = "";
                    
                }

                return StatusCode(200, token);
            }
            catch (Exception ex)
            {
                return StatusCode(401, ex.Message);
            }
        }
    }
}
