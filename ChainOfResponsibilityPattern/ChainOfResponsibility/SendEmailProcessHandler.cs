using ChainOfResponsibilityPattern.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Org.BouncyCastle.Utilities;
using MimeKit.Text;

namespace ChainOfResponsibilityPattern.ChainOfResponsibility
{
    public class SendEmailProcessHandler : ProcessHandler
    {
        private readonly AppUser _user;
        public SendEmailProcessHandler(AppUser user)
        {
            _user = user;
        }

        //private  string _fileName;

        //private  string _toMail;

        //public SendEmailProcessHandler(string fileName, string toMail)
        //{
        //    _fileName = fileName;
        //    _toMail = toMail;
        //}

        public override async Task<object> HandleAsync(object o)
        {
            string displayName = "Social_Network Mail";
            string SmtpPass = "enodxsnicrvxqoap";
            string SmtpUser = "testozge8@gmail.com";
            string SmtpHost = "smtp.gmail.com";
            string EmailFrom = "testozge8@gmail.com";
            int SmtpPort = 587;

            var zipMemoryStream = o as MemoryStream;
            zipMemoryStream.Position = 0;
            var multipart = new Multipart("mixed");
            var textPart = new TextPart(TextFormat.Html)
            {
                Text = "<p>Zip dosyası ektedir.</p>",
                ContentTransferEncoding = ContentEncoding.Base64,
            };

            MimeMessage email = new();
            email.Sender = MailboxAddress.Parse($"{displayName} < {EmailFrom}>");
            email.To.Add(MailboxAddress.Parse(_user.Email)); 
            email.Subject = "Zip Dosyası";
            BodyBuilder builder = new();

            var attachmentPart = new MimePart(MediaTypeNames.Application.Zip)
            {
                Content = new MimeContent(zipMemoryStream),
                ContentId = "ProductList",
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = "ProductList"
            };
            //var attachment = new Attachment(zipMemoryStream, "ProductList", MediaTypeNames.Application.Zip);
            multipart.Add(attachmentPart);
            multipart.Add(textPart);
            email.Body = multipart;

            using (MailKit.Net.Smtp.SmtpClient smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.Connect(SmtpHost, SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(SmtpUser, SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            return base.HandleAsync(null);
        }

    }
}
