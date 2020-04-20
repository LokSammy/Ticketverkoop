using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Ticketverkoop.Models;
using Ticketverkoop.ViewModels;

namespace Ticketverkoop.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactVM contactVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(contactVM.Email);

                    message.To.Add("sammy.mvccore@gmail.com");
                    message.Subject = "Contact pagina van " + contactVM.Naam + " " + contactVM.Voornaam;
                    message.Body = contactVM.Message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;

                    smtp.Credentials = new System.Net.NetworkCredential("sammy.mvccore@gmail.com", "azerty-123");
                    smtp.EnableSsl = true;
                    smtp.Send(message);

                    ModelState.Clear();
                    ViewBag.Message = "Bericht is verstuurd. We gaan u zo snel mogelijk contacteren.";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Bericht kan niet verstuurd worden, probeer het later nog een keer.: {ex.Message}";
                }
            }

            return View();
        }
    }
}
