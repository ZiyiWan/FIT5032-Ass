using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FIT5032_Week08A.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public async Task SendAsync(String toEmail, String body)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("576384038@qq.com", "After Class!");
            var to = new EmailAddress(toEmail, "");
            var subject = "Confirm!";          
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, "");
            var bytes = File.ReadAllBytes("D:\\study\\confirm.docx");
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment("confirm.docx", file);
            var response = client.SendEmailAsync(msg);
        }

    }
}