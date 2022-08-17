using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route(nameof(PasswordController))]
    public class PasswordController : Controller
    {
        
        [HttpGet]
        public string Get()
        {
            return "Get";
        }

        [HttpPost]
        public string Post()
        {
            return "Post";
        }

        [HttpPut]
        public string Put()
        {
            return "Put";
        }

        [HttpDelete]
        public string Delete()
        {
            return "Delete";
        }
    }
}
