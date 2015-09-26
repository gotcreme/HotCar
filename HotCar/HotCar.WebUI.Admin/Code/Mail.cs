using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Configuration;
using ImapX;
using Attachment = System.Net.Mail.Attachment;
using MailAddress = System.Net.Mail.MailAddress;


namespace HotCar.WebUI.Admin.Code
{
    public class Mail
    {
        private static string MailDisplayName
        {
            get { return WebConfigurationManager.AppSettings["MailDisplayName"]; }
        }

        private static string MailPassword
        {
            get { return WebConfigurationManager.AppSettings["MailPassword"]; }
        }

        public static string MailLogin
        {
            get { return WebConfigurationManager.AppSettings["MailLogin"]; }
        }

        private static bool Connect(ImapClient client)
        {
            if (client.Connect())
            {
                //if (client.Login(MailLogin, MailPassword))
                if (client.Login("hotcarservice@gmail.com", "1597534682"))
                {
                    return true;
                }
            }

            return false;
        }

        private static int GetCountNewMessages(ImapClient client)
        {
            return client.Folders["INBOX"].Search("UNSEEN").Length;
        }

        private static int GetCountInputMessages(ImapClient client)
        {
            return client.Folders.Inbox.Messages.Count();
        }

        public static string GetMessageStatus()
        {
            var client = new ImapClient("imap.gmail.com", true);
            client.Behavior.AutoPopulateFolderMessages = true;

            if (Connect(client))
            {
                string result = "( " + GetCountNewMessages(client) + " ) / " + GetCountInputMessages(client);
                client.Logout();
                return result;
            }

            return "error: unauthorized";
        }

        public static void Send(string to, string message, string subject = "", Attachment attachment = null)
        {
            var fromAddress = new MailAddress(MailLogin, MailDisplayName);
            string fromPassword = MailPassword;
            var toAddress = to;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };

            var mail = new MailMessage(fromAddress.Address, toAddress);
            mail.Subject = subject;
            mail.Body = message;
            if (attachment != null)
            {
                mail.Attachments.Add(attachment);
            }

            smtp.Send(mail);
        }
    }
}