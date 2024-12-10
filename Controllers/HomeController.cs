using Azure.Core;
using DutchTreat.Data;
using DutchTreat.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;


namespace DutchTreat.Controllers

{/*Course: Web Programming 3
* Assessment: Milestone 1
* Created by: Rita Youssef - 2124602
* Date: 24-10-2024 
* Class Name: HomeController.cs
* Description: The HomeController class handles the interactions between the views and the model while managing user requests. 
* Time Task B): 2 hours
* Time Task C): 12 hours */

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IDutchRepository _repository;
        private readonly string _secretKey = "6LfdJ20qAAAAAKpHIR54YJPNzTnneqYL7E0NGy0G";

        //constructor 
        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender, IDutchRepository repository)
        {
            _logger = logger;
            _emailSender = emailSender;
            _repository = repository;
        }

        // Action method to display the shop page.
        public IActionResult Shop()
        {
            var results = _repository.GetAllProducts();
            return View(results);
        }

        // Action method to redirect the home page to the contact page.
        public IActionResult Index()
        {
            return RedirectToAction("Contact"); 
        }

        // Action method to display the privacy policy page.
        public IActionResult Privacy()
        {
            return View();
        }
        // GET action method to display the contact form and storing the topic dropdown content.
        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Header = "Contact Us";
            var topics = new List<SelectListItem>
        {
            new SelectListItem { Value = "My order", Text = "My order" },
            new SelectListItem { Value = "Feedback", Text = "Feedback" },
            new SelectListItem { Value = "Product Questions", Text = "Product Questions" },
            new SelectListItem { Value = "Customer Service and feedback", Text = "Customer Service and feedback" },
            new SelectListItem { Value = "Technical questions, specifications, geometry, sizing and historical information", Text = "Technical questions, specifications, geometry, sizing and historical information" },
            new SelectListItem { Value = "Warranty", Text = "Warranty" },
            new SelectListItem { Value = "Registration", Text = "Registration" },
            new SelectListItem { Value = "Catalogue requests", Text = "Catalogue requests" },
            new SelectListItem { Value = "Owner's manuals", Text = "Owner's manuals" },
            new SelectListItem { Value = "Media enquiries", Text = "Media enquiries" },
            new SelectListItem { Value = "Sponsorship and donations", Text = "Sponsorship and donations" }
        };

            ViewBag.Topics = new SelectList(topics, "Value", "Text");
            return View();
        }


        // POST action method to handle the submission of the contact form and sending it to the recipient & admin, also includes backend recaptcha validation, saving the contact form to the database
        // and to redirect to success page if the form met the Contact model requirements or failure page if the email could not have been sent.
        [HttpPost("contact")]
        public async Task<IActionResult> ContactAsync(ContactModel contact)
        {
            var client = new HttpClient();
            var recaptchaResponse = base.Request.Form["g-recaptcha-response"];
            var result = await client.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={_secretKey}&response={recaptchaResponse}", null);
            var responseBody = await result.Content.ReadAsStringAsync();
            var recaptchaResult = JsonConvert.DeserializeObject<ContactModel.CaptchaResponse>(responseBody);

            if (!recaptchaResult.Success)
            {
                ModelState.AddModelError("", "Complete Recaptcha");
                return View(contact);
            }

            if (ModelState.IsValid)
            {
                string emailContent = GenerateEmailContent(contact);
                _repository.AddContact(contact);
                _repository.SaveAll();

                try
                {
                    await _emailSender.SendEmailAsync(contact.Email, contact.Topic, emailContent);
                    return View("Success", contact);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("Failure"); 
                }
            }

            return View(contact);
        }
        // Method to generate email content to send it in good format to the recipient & admin.
        public string GenerateEmailContent(ContactModel contact)
        {
                return $"  <h1>Topic: {contact.Topic}</h1>" +
           $" <p>Thanks for your message, {contact.FirstName} {contact.LastName}.</p> " +
           $" <p>We received the following information:</p> " +
           $" <ul> <li> Postal Code: {contact.PostalCode}</li>  <li> Email: {contact.Email}</li> <li> Phone Number: {contact.Phone}</li></ul> " +
           $" <p><strong> Your message:</strong><p>"+
           $" <p>{contact.QorC}</p>";
        }

        // Action method to display the error page.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
