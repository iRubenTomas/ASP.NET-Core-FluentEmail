using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System;
using System.Net.Mail;
using System.Text;

namespace EmailDemo
{
    //This is a email client
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //Testing locally using Papercut SMTP
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                // This should all come from appsettings.json file ->  Testing vs Production

                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25
                //DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                //PickupDirectoryLocation = @"C:\Users\Ruben Tomas\Desktop\ASP.NET-Core-FluentEmail\EmailDemo"


            });

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            /*
             * This is a template; 
             * StringBuilder is more efficient than string appending or string interpolation 
            */
            StringBuilder template = new();
            template.AppendLine("Dear @Model.FirstName,");
            template.AppendLine("<p>Thanks for purchasing a @Model.ProductName. We hope you enjoy it. <!p>");
            template.AppendLine("- The EmailDemo Team");




            var email = await Email
                .From("user@user.com")
                .To("test@test.com", "Test")
                .Subject("Thanks")
                .UsingTemplate(template.ToString(), new { FirstName = "Rose", ProductName = "Ball" })

                //.Body("Thanks for the gift!")
                .SendAsync();




            
        }
    }
}
