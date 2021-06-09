using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail; 

namespace DawProject.Models
{
    public class SendMail
    {
        public bool SendEmail(string email, string subject, string message)
        {
            try
            { 
              var senderEmail = new MailAddress("monitore26@gmail.com", "Employee Monitor");
              var receiverEmail = new MailAddress(email, "LuckyLuciano");
              var password = "Asdqwe!23";
              var sub = subject;
              var body = message;
              var smtp = new SmtpClient
              {
                  Host = "smtp.gmail.com",
                  Port = 587,
                  EnableSsl = true,
                  DeliveryMethod = SmtpDeliveryMethod.Network,
                  UseDefaultCredentials = false,
                  Credentials = new NetworkCredential(senderEmail.Address, password)
              };
              using (var mess = new MailMessage(senderEmail, receiverEmail)
              {
                  Subject = subject,
                  Body = body
              }) 
              smtp.Send(mess); 
              return true; 
            }
            catch (Exception e)
            {
                var x_err = e.Message;
                return false;
            } 
        }
    }
}