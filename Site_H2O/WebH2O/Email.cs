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
namespace WebH2O
{
    public class Email
    {
        public string CorpoEMail { get; private set; }

        public string AssuntoEmail { get; private set; }

        public string EmailDestino { get; private set; }

        private Email(string corpoEmail, string assuntoEmail, string emailDestino)
        {
            CorpoEMail = corpoEmail;
            AssuntoEmail = assuntoEmail;
            EmailDestino = emailDestino;
        }

        public static Email Criar(string corpoEmail, string assuntoEmail, string emailDestino)
        {
            if (string.IsNullOrWhiteSpace(corpoEmail))
                throw new ArgumentException("corpoEmail");

            if (string.IsNullOrWhiteSpace(assuntoEmail))
                throw new ArgumentException("assuntoEmail");

            if (string.IsNullOrWhiteSpace(emailDestino))
                throw new ArgumentException("emailDestino");

            string email = emailDestino.Trim();
            int arroba, arroba2, ponto;

            arroba = email.IndexOf('@');
            arroba2 = email.LastIndexOf('@');
            ponto = email.LastIndexOf('.');

            if (arroba <= 0 || ponto <= (arroba + 1) || ponto == (email.Length - 1) || arroba2 != arroba)
                throw new ArgumentException("E-mail Inválido");

            return new Email(corpoEmail.Trim(), assuntoEmail.Trim(), email);
        }

        public string Enviar()
        {
            string msgErro = string.Empty;

            //enviando e-mail
            // Especifica o servidor SMTP e a porta
            using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
            {
                try
                {
                    // EnableSsl ativa a comunicação segura com o servidor
                    client.EnableSsl = true;

                    // Especifica a credencial utilizada para envio da mensagem
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential("h2o.bandtec@gmail.com", "vivianzika");

                    // Especifia o remetente e o destinatário da mensagem
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(
                        new System.Net.Mail.MailAddress("h2o.bandtec@gmail.com", "Grupo H2O", Encoding.UTF8),
                        new System.Net.Mail.MailAddress(EmailDestino));

                    // Preenche o corpo e o assunto da mensagem
                    message.BodyEncoding = Encoding.UTF8;
                    message.Body = CorpoEMail;
                    message.SubjectEncoding = Encoding.UTF8;
                    message.Subject = AssuntoEmail;

                    // Anexos devem ser adicionados através do método
                    // message.Attachments.Add()

                    // Envia a mensagem
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    // Exceções devem ser tratadas aqui!
                    return ex.Message;
                }

            }
            return "";
        }


    }
}
