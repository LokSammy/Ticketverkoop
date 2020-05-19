using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Ticketverkoop.Areas.Identity.Data;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Models;
using Ticketverkoop.Service;
using Ticketverkoop.ViewModels;

namespace Ticketverkoop.Controllers
{
    public class HomeController : Controller
    {
        private AspNetUsersService usersService;

        public IActionResult Index()
        {
            string url = "http://api.weatherstack.com/current?access_key=b43804f796c69ffeb190b7f4d8c286fd&query=Belgium";
            WebRequest requestObjectGet = WebRequest.Create(url);
            requestObjectGet.Method = "GET";
            HttpWebResponse responseObjectGet = null;
            responseObjectGet = (HttpWebResponse)requestObjectGet.GetResponse();

            string stringResult = null;
            using (Stream stream = responseObjectGet.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                stringResult = sr.ReadToEnd();
                sr.Close();
            }

            var weerObject = JsonSerializer.Deserialize<WeerVM>(stringResult);


            return View(weerObject);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            if(User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                usersService = new AspNetUsersService();
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                AspNetUsers userNow = usersService.GetUserById(userId);
                if (!userNow.EmailConfirmed)
                {
                    ViewBag.Message = "Bevestig uw Email aub.";
                    return View();
                }
                else 
                {
                    ContactVM contact = new ContactVM
                    {
                        Email = userNow.Email,
                        Naam = userNow.Name,
                        Voornaam = userNow.FirstName,
                        Message = " "

                    };
                    return View(contact);
                }


            }
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
