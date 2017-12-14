using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Sistema.Utils;
using WebH2O.Utils;
using WebH2O.Models;

namespace WebH2O
{
    public partial class Cadastro : System.Web.UI.Page
    {
        RetornoWebService rWS = new RetornoWebService();
        WebService proxy = new WebService();

        public const int TamanhoMaximoDoLogin = 32;
        public const int TamanhoMaximoDoNome = 32;
        public const int TamanhoMaximoDoPassword = 20;

        public int Id;
        public string Login;
        public string Nome;
        public string Token;

        public string erroRetorno = string.Empty;


        private string EnviarParaCliente()
        {
            string value = Id + "|" + Token;

            HttpCookie cookie = new HttpCookie("user", value);

            cookie.Expires = DateTime.UtcNow.AddYears(1);

            HttpContext.Current.Response.SetCookie(cookie);

            return "user=" + value + ";";
        }

        private void RemoverDoCliente()
        {
            HttpCookie cookie = new HttpCookie("user", "");

            cookie.Expires = DateTime.UtcNow.AddYears(-1);

            HttpContext.Current.Response.SetCookie(cookie);
        }

       

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ValidarNome(object sender, EventArgs e)
        {
            if (boxID.Text.Length < 6)
            {
                errousuario.Text = "ID INVÁLIDO !";
                panelErroUsuario.Visible = true;
                return;
            }
            
            else if (boxNome.Text.Length == 0)
            {
                errousuario.Text = "NOME INVÁLIDO !";
                panelErroUsuario.Visible = true;
                return;
            }

            else if (boxCpf.Text.Length != 11)
            {
                errousuario.Text = "CPF INVÁLIDO !";
                panelErroUsuario.Visible = true;
                return;
            }

            else if (boxRg.Text.Length != 9)
            {
                errousuario.Text = "RG INVÁLIDO !";
                panelErroUsuario.Visible = true;
                return;
            }

            else
            {

                coluna1.Attributes["class"] = "col-sm-4 painel-des";
                coluna2.Attributes["class"] = "col-sm-4";
                errousuario.Text = "";

                panelErroUsuario.Visible = false;

                boxLogradouro.Enabled = true;
                boxNumero.Enabled = true;
                boxCidade.Enabled = true;
                boxBairro.Enabled = true;
                boxCep.Enabled = true;
                boxComplemento.Enabled = true;
                Button1.Enabled = true;

                boxNome.Enabled = false;
                boxID.Enabled = false;
                boxCpf.Enabled = false;
                boxRg.Enabled = false;
                btnPasso1.Enabled = false;

            }
        }

        protected void ValidarEndereço(object sender, EventArgs e)
        {
            if (boxLogradouro.Text.Length == 0)
            {
                erroendereco.Text = "Logradouro Inválido";
                panelErroEndereco.Visible = true;
                return;
            }

            else if (boxNumero.Text.Length == 0)
            {
                erroendereco.Text = "Número Inválido";
                panelErroEndereco.Visible = true;
                return;
            }

            else if (boxCep.Text.Length != 8)
            {
                erroendereco.Text = "Cep Inválido";
                panelErroEndereco.Visible = true;
                return;
            }

            else if (boxBairro.Text.Length == 0)
            {
                erroendereco.Text = "Bairro Inválido";
                panelErroEndereco.Visible = true;
                return;
            }

            else if (boxCidade.Text.Length == 0)
            {
                erroendereco.Text = "Cidade Inválida";
                panelErroEndereco.Visible = true;
                return;
            }

            else
            {
                coluna2.Attributes["class"] = "col-sm-4 painel-des";
                coluna3.Attributes["class"] = "col-sm-4";
                errousuario.Text = "";

                boxLogradouro.Enabled = false;
                boxNumero.Enabled = false;
                boxCidade.Enabled = false;
                boxBairro.Enabled = false;
                boxCep.Enabled = false;
                boxComplemento.Enabled = false;
                Button1.Enabled = false;

                boxEmail.Enabled = true;
                boxConfEmail.Enabled = true;
                boxSenha.Enabled = true;
                boxConfSenha.Enabled = true;
                Button2.Enabled = true;
            }
        }

        protected void Cadastrar(object sender, EventArgs e)
        {
            Usuario uS = (Usuario)Session["cod_usuario"];
            Bairro bS = (Bairro)Session["cod_bairro"];
            Cidade cS = (Cidade)Session["cod_cidade"];

            string email = boxEmail.Text.Trim();
            int arroba, arroba2, ponto;

            arroba = email.IndexOf('@');
            arroba2 = email.LastIndexOf('@');
            ponto = email.LastIndexOf('.');

            if (arroba <= 0 || ponto <= (arroba + 1) || ponto == (email.Length - 1) || arroba2 != arroba)
            {
                // E-mail inválido!

                erroinfo.Text = "E-mail Inválido";
                panelErroInfo.Visible = true;
                return;
            }

            else if (boxConfEmail.Text != boxEmail.Text)
            {
                erroinfo.Text = "Confirmação de E-mail Inválido";
                panelErroInfo.Visible = true;
                return;
            }

            else if (boxSenha.Text.Length < 6)
            {
                erroinfo.Text = "Senha Inválida";
                panelErroInfo.Visible = true;
                return;
            }

            else if (boxConfSenha.Text != boxSenha.Text)
            {
                erroinfo.Text = "Confirmação de Senha Inválida";
                panelErroInfo.Visible = true;
                return;
            }

            else
            {
                erroinfo.Text = "";

                rWS = proxy.CadastrarUsuarioCompleto(boxNome.Text, boxEmail.Text, boxCpf.Text, boxRg.Text, boxSenha.Text, boxLogradouro.Text, boxBairro.Text, boxCidade.Text, boxID.Text);
                       
            }

            Email.Criar("Cadastro realizado com sucesso", "Cadastro H2O", boxEmail.Text).Enviar();

            Response.Redirect("default.aspx");
        }
    }
}
