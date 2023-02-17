using CatIstagram.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CatIstagram.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ApiController
    {
 
        [HttpGet]
        public ActionResult Index()
        {
            return Ok("Worked success !!!");
        }

 
    }
}
