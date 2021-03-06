﻿using System;
using System.IO;
using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using theworld50.Models;
using theworld50.ViewModels;
using theworld50.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace theworld50.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService mailService;
        private readonly IWorldRepository repository;

        public AppController(IMailService mailService, IWorldRepository repository)
        {
            this.mailService = mailService;
            this.repository = repository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var trips = repository.GetAllTrips();

            return View(trips);
        }

        [Authorize()]
        public IActionResult Trips()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View("_Error");
        }

        public IActionResult MakeError()
        {
            throw new Exception("error");
            return View();
        }

        public IActionResult Contact()
        {
            //throw new Exception();
            return View();
        }
        public IActionResult AngularTestPage()
        {
            return View();
        }

        public IActionResult Data()
        {
            return Json(JsonConvert.DeserializeObject(System.IO.File.ReadAllText("data/data.json")));
        }

        [HttpPost]
        public IActionResult Contact1(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];

                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("", "Could not send email, Config problem!");
                }

                if(mailService.SendMail(email, email, $"Contact Page from {model.Name} ({model.Email})", model.Message))
                {
                    ModelState.Clear();

                    ViewBag.Message = "Email has been sent!";
                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult AddMessage(string message)
        {
            return Json(message);
        }
    }
}
