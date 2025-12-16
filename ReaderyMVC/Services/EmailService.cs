using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using ReaderyMVC.Models;

using System.Net.Mail;
using System.Net; // Necessário se você for usar NetworkCredential para autenticação SMTP


namespace Readery.Services
{
    public class EmailService
    {
         // Email do usuário
        private readonly string _emailDestino;
        public int CodigoGerado { get; private set; }

        public EmailService(string contrutorEmailDestino)
        {
            _emailDestino = contrutorEmailDestino;
            CodigoGerado = new Random().Next(100000, 999999);
        }

        public async Task EnviarEmail()
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(
                    "readery3@gmail.com",
                    MandarEmail.ENV.senha
                )
            };

            string htmlBody = $@"<!DOCTYPE html>
<html lang='pt-BR'>
<head>
    <meta charset='UTF-8'>
    <title>Bem-vindo ao Readery</title>
</head>

<body style='margin:0; padding:0; background-color:#b8a28b;'>

    <table width='100%' cellpadding='0' cellspacing='0'
           style='background-color:#b8a28b; padding:20px 0;'>
        <tr>
            <td align='center'>

                <table width='600' cellpadding='0' cellspacing='0'
                       style='background-color:#E3D6C8; border-radius:8px;'>

                    <tr>
                        <td style='padding:25px 20px; text-align:center;'>
                            <h1 style='margin:0;
                                       font-size:26px;
                                       color:#000;
                                       font-family:Arial, Helvetica, sans-serif;'>
                                BEM-VINDO AO READERY
                            </h1>
                        </td>
                    </tr>

                    <tr>
                        <td style='padding:10px 35px;
                                   font-family:Arial, Helvetica, sans-serif;
                                   color:#000;
                                   font-size:16px;
                                   line-height:1.5;'>

                            <p>
                                Estamos muito felizes por ter nos escolhido para ser sua biblioteca pessoal.
                            </p>

                            <p>
                                Desejamos que você aproveite o app e possa nos seguir no LinkedIn para ajudar em nosso
                                desenvolvimento profissional:
                            </p>

                            <div style='text-align:center; padding:10px 0;'>
                                <ul style='list-style-type:none; padding:0; margin:0; text-align:center;'>

                                    <li style='margin-bottom:8px;'>
                                        <a href='https://www.linkedin.com/in/isaque-silva-b05184355/'
                                           style='color:#2563EB; text-decoration:none; font-size:14px; font-weight:500;'>
                                            Isaque Silva
                                        </a>
                                    </li>

                                    <li style='margin-bottom:8px;'>
                                        <a href='http://linkedin.com/in/rafaella-hahon-114b35260/'
                                           style='color:#2563EB; text-decoration:none; font-size:14px; font-weight:500;'>
                                            Rafaella Hahon
                                        </a>
                                    </li>

                                    <li style='margin-bottom:8px;'>
                                        <a href='https://www.linkedin.com/in/pablo-h-pedroza-b7020336a/'
                                           style='color:#2563EB; text-decoration:none; font-size:14px; font-weight:500;'>
                                            Pablo H. Pedroza
                                        </a>
                                    </li>

                                    <li style='margin-bottom:8px;'>
                                        <a href='https://www.linkedin.com/in/edulordeiro/'
                                           style='color:#2563EB; text-decoration:none; font-size:14px; font-weight:500;'>
                                            Edu Lordeiro
                                        </a>
                                    </li>

                                    <li style='margin-bottom:8px;'>
                                        <a href='https://www.linkedin.com/in/antonio-carlos-vieira-santos-2ba3a0230/'
                                           style='color:#2563EB; text-decoration:none; font-size:14px; font-weight:500;'>
                                            Antônio Carlos Vieira Santos
                                        </a>
                                    </li>

                                    <li>
                                        <a href='https://www.linkedin.com/in/guilherme-ribeiro-dos-santos-662798382/'
                                           style='color:#2563EB; text-decoration:none; font-size:14px; font-weight:500;'>
                                            Guilherme Ribeiro dos Santos
                                        </a>
                                    </li>

                                </ul>
                            </div>
                        </td>
                    </tr>

                </table>

            </td>
        </tr>
    </table>

</body>
</html>";
                                    

            var email = new MailMessage
            {
                From = new MailAddress("readery3@gmail.com"),
                Subject = "BEM VINDO AO READERY",
                Body = htmlBody,
                IsBodyHtml = true
            };

            email.To.Add(_emailDestino);

            await smtp.SendMailAsync(email);
        }
    }
}