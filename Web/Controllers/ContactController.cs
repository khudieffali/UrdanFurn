﻿using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
    }
}
