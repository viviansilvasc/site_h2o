using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;

namespace WebH2O.Utils
{
    /// <summary>
    /// Descrição resumida de WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        private DateTime date = DateTime.UtcNow;

        public RetornoWebService GetDate()
        {
            RetornoWebService rWS = new RetornoWebService();

            rWS.Messagem = date.ToString("dd/MM/yyyy");
            rWS.Sucesso = true;

            return rWS;
        }

        [WebMethod]
        public RetornoWebService GetTime()
        {
            RetornoWebService rWS = new RetornoWebService();

            rWS.Messagem = date.ToString("HH:mm:ss");
            rWS.Sucesso = true;

            return rWS;
        }

        [WebMethod]
        public RetornoWebService AutenticarUsuario(String email, String senha)
        {
            senha = CadastrarSenha(senha);
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT cod_usuario FROM usuario WHERE email = @email AND senha = @senha;", conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // ID (0)
                            rWS.Messagem = reader.GetInt32(0).ToString();
                            rWS.Sucesso = true;
                        }
                        else
                        {
                            //NENHUM EMAIL E SENHA COMPATÍVEIS
                            rWS.Messagem = "Email ou Senha Incorretos";
                            rWS.Sucesso = false;
                        }
                    }
                }

                conn.Close();
                conn.Dispose();
            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService PegarUsuario(int cod_usuario)
        {
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT cod_usuario, nome, email, cpf, rg FROM usuario WHERE cod_usuario = @cod_usuario", conn))
                {
                    cmd.Parameters.AddWithValue("@cod_usuario", cod_usuario);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                rWS.Messagem += reader.GetInt32(0) + ">" + reader.GetString(1) + ">" + reader.GetString(2) + ">" + reader.GetString(3) + ">" + reader.GetString(4);
                                rWS.Sucesso = true;
                            }
                        }
                        else
                        {
                            //NENHUM USUÁRIO ENCONTRADO
                            rWS.Messagem = "Não foi encontrado nenhum usuário!";
                            rWS.Sucesso = false;
                        }
                    }
                }

                conn.Close();
                conn.Dispose();
            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService PegarDispositivo(int cod_dispositivo)
        {
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT cod_dispositivo, id FROM dispositivo WHERE cod_dispositivo = @cod_dispositivo", conn))
                {
                    cmd.Parameters.AddWithValue("@cod_dispositivo", cod_dispositivo);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                rWS.Messagem += reader.GetInt32(0) + ">" + reader.GetString(1);
                                rWS.Sucesso = true;
                            }
                        }
                        else
                        {
                            //NENHUM DISPOSITIVO ENCONTRADO
                            rWS.Messagem = "Não foi encontrado nenhum dispositivo!";
                            rWS.Sucesso = false;
                        }
                    }
                }

                conn.Close();
                conn.Dispose();
            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService PegarCidade(int cod_cidade)
        {
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT nome FROM cidade WHERE cod_cidade = @cod_cidade", conn))
                {
                    cmd.Parameters.AddWithValue("@cod_cidade", cod_cidade);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                rWS.Messagem += reader.GetInt32(0) + ">" + reader.GetString(1);
                                rWS.Sucesso = true;
                            }
                        }
                        else
                        {
                            //NENHUMA CIDADE ENCONTRADA
                            rWS.Messagem = "Não foi encontrado nenhuma cidade!";
                            rWS.Sucesso = false;
                        }
                    }
                }

                conn.Close();
                conn.Dispose();
            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService CadastrarUsuarioCompleto(string nome, string email, string cpf, string rg, string senha, string logradouro, string bairro, string cidade, string dispositivo)
        {
            senha = CadastrarSenha(senha);
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO USUARIOCOMPLETO (NOME, EMAIL, CPF, RG, SENHA, LOGRADOURO, BAIRRO, CIDADE, DISPOSITIVO) VALUES (@nome, @email, @cpf, @rg, @senha, @logradouro, @bairro, @cidade, @dispositivo)", conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    cmd.Parameters.AddWithValue("@rg", rg);
                    cmd.Parameters.AddWithValue("@senha", senha);
                    cmd.Parameters.AddWithValue("@logradouro", logradouro);
                    cmd.Parameters.AddWithValue("@bairro", bairro);
                    cmd.Parameters.AddWithValue("@cidade", cidade);
                    cmd.Parameters.AddWithValue("@dispositivo", dispositivo);

 
                    cmd.ExecuteNonQuery();

                }

                conn.Close();
                conn.Dispose();
            }

            return rWS;
        }


        [WebMethod]
        public RetornoWebService CadastrarUsuario(string nome, string email, string cpf, string rg, string senha)
        {
            senha = CadastrarSenha(senha);
            int cod_usuario;
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO USUARIO (NOME, EMAIL, CPF, RG, SENHA) OUTPUT INSERTED.COD_USUARIO VALUES (@nome, @email, @cpf, @rg, @senha)", conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    cmd.Parameters.AddWithValue("@rg", rg);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    cod_usuario = (int)cmd.ExecuteScalar();
                    cmd.ExecuteNonQuery();

                }

                conn.Close();
                conn.Dispose();
            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService EditarUsuario(string nome, string email, string cpf, string rg, string senha)
        {
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE USUARIO SET NOME = @NOME, EMAIL = @EMAIL, CPF = @CPF, RG = @RG, SENHA = @SENHA WHERE COD_USUARIO = @COD_USUARIO", conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    cmd.Parameters.AddWithValue("@rg", rg);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    cmd.ExecuteNonQuery();

                }

                conn.Close();
                conn.Dispose();
            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService DeletarUsuario(int cod_usuario)
        {
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM USUARIO WHERE COD_USUARIO = @COD_USUARIO", conn))
                {
                    cmd.Parameters.AddWithValue("@cod_usuario", cod_usuario);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                conn.Dispose();
            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService CadastrarDispositivo(string id, int cod_usuario)
        {
            RetornoWebService rWS = new RetornoWebService();
            int cod_dispositivo;

            using (SqlConnection conn = ConexaoSql.Open())
            {


                using (SqlCommand cmd = new SqlCommand("INSERT INTO DISPOSITIVO (ID, COD_USUARIO) OUTPUT INSERTED.COD_DISPOSITIVO VALUES (@id, @cod_usuario)", conn))
                {
                   
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@cod_usuario", cod_usuario);

                    cod_dispositivo = (int)cmd.ExecuteScalar();
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                conn.Dispose();
            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService EditarDispositivo(string id)
        {
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE DISPOSITIVO SET ID = @ID WHERE COD_DISPOSITIVO = @COD_DISPOSITIVO", conn))
                {
                    
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                conn.Dispose();

            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService DeletarDispositivo(int cod_dispositivo)
        {
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {


                using (SqlCommand cmd = new SqlCommand("DELETE FROM DISPOSITIVO WHERE COD_DISPOSITIVO", conn))
                {
                    
                    cmd.Parameters.AddWithValue("@cod_dispositivo", cod_dispositivo);

                    cmd.ExecuteNonQuery();

                }

                conn.Close();
                conn.Dispose();

            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService CadastrarCidade(string cidade)
        {
            RetornoWebService rWS = new RetornoWebService();
            int cod_cidade;

            using (SqlConnection conn = ConexaoSql.Open())
            {

                using (SqlCommand cmd = new SqlCommand("INSERT INTO CIDADE (NOME) OUTPUT INSERTED.COD_CIDADE VALUES (@nome)", conn))
                {
                    cmd.Parameters.AddWithValue("@nome", cidade);

                    cod_cidade = (int)cmd.ExecuteScalar();
                    cmd.ExecuteNonQuery();

                }

                conn.Close();
                conn.Dispose();
            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService EditarCidade(string cidade)
        {
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {

                using (SqlCommand cmd = new SqlCommand("UPDATE CIDADE SET NOME = @NOME WHERE COD_CIDADE = @COD_CIDADE", conn))
                {
                    cmd.Parameters.AddWithValue("@nome", cidade);

                    cmd.ExecuteNonQuery();

                }

                conn.Close();
                conn.Dispose();
            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService DeletarCidade(int cod_cidade)
        {
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {

                using (SqlCommand cmd = new SqlCommand("DELETE FROM CIDADE WHERE COD_CIDADE = @COD_CIDADE", conn))
                {
                    cmd.Parameters.AddWithValue("@cod_cidade", cod_cidade);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                conn.Dispose();
            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService CadastrarBairro(string nome, int cod_cidade)
        {
            RetornoWebService rWS = new RetornoWebService();
            
            using (SqlConnection conn = ConexaoSql.Open())
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO BAIRRO (NOME, cod_cidade) OUTPUT INSERTED.COD_BAIRRO VALUES (@nome, @cod_cidade)", conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@cod_cidade", cod_cidade);

                    cmd.ExecuteNonQuery();

                }

                conn.Close();
                conn.Dispose();

            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService EditarBairro(int cod_cidade, string nome)
        {
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {

                using (SqlCommand cmd = new SqlCommand("UPDATE BAIRRO SET NOME = @NOME, COD_CIDADE = @COD_CIDADE WHERE COD_BAIRRO = @COD_BAIRRO", conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@cod_cidade", cod_cidade);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                conn.Dispose();

            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService DeletarBairro(int cod_bairro)
        {
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM BAIRRO WHERE COD_BAIRRO = @COD_BAIRRO", conn))
                {
                    cmd.Parameters.AddWithValue("@cod_bairro", cod_bairro);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                conn.Dispose();

            }

            return rWS;

        }

        [WebMethod]
        public RetornoWebService CadastrarEndereco(string logradouro, int numero, string cep, string complemento, int cod_usuario, int cod_bairro)
        {
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {

                using (SqlCommand cmd = new SqlCommand("INSERT INTO ENDERECO (LOGRADOURO, NUMERO, CEP, COMPLEMENTO, COD_USUARIO, COD_BAIRRO) VALUES (@logradouro, @numero, @cep, @complemento, @cod_usuario, @cod_bairro)", conn))
                {
                    cmd.Parameters.AddWithValue("@logradouro", logradouro);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.Parameters.AddWithValue("@cep", cep);
                    cmd.Parameters.AddWithValue("@complemento", complemento);
                    cmd.Parameters.AddWithValue("@cod_usuario", cod_usuario);
                    cmd.Parameters.AddWithValue("@cod_bairro", cod_bairro);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                conn.Dispose();

            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService EditarEndereco(string logradouro, int numero, string cep, string complemento, int cod_usuario, int cod_bairro)
        {
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {

                using (SqlCommand cmd = new SqlCommand("UPDATE ENDERECO SET LOGRADOURO = @LOGRADOURO, NUMERO = @NUMERO, CEP = @CEP, COMPLEMENTO = @COMPLEMENTO, COD_BAIRRO = @COD_BAIRRO, COD_USUARIO = @COD_USUARIO WHERE COD_ENDERECO = @COD_ENDERECO", conn))
                {
                    cmd.Parameters.AddWithValue("@logradouro", logradouro);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.Parameters.AddWithValue("@cep", cep);
                    cmd.Parameters.AddWithValue("@complemento", complemento);
                    cmd.Parameters.AddWithValue("@cod_bairro", cod_bairro);
                    cmd.Parameters.AddWithValue("@cod_usuario", cod_usuario);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                conn.Dispose();

            }

            return rWS;
        }

        [WebMethod]
        public RetornoWebService DeletarEndereco(int cod_endereco)
        {
            RetornoWebService rWS = new RetornoWebService();

            using (SqlConnection conn = ConexaoSql.Open())
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM ENDERECO WHERE COD_ENDERECO = @COD_ENDERECO", conn))
                {
                    cmd.Parameters.AddWithValue("@cod_endereco", cod_endereco);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                conn.Dispose();

            }

            return rWS;
        }

        private string CadastrarSenha(string senha)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue, MessageBytes = UE.GetBytes(senha);
            MD5 md5 = new MD5CryptoServiceProvider();
            string senhaToken = "";


            HashValue = md5.ComputeHash(MessageBytes);
            foreach (byte b in HashValue)
            {
                senhaToken += String.Format("{0:x2}", b);
            }
            return senhaToken;
        }
    }
}
