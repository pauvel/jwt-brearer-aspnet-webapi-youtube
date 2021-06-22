using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyApi.Helpers;
using MyApi.Models;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PersonasController : ControllerBase
    {
        private readonly IConfiguration conf;

        public PersonasController(IConfiguration config)
        {
            conf = config;
        }


        [HttpGet]
        public IEnumerable<object> GetAll(){
            var personas = new List<object>(){
                new {
                    nombre = "Paul Veliz",
                    estatura = 1.70,
                    nacionalidad = "MEX"
                },
                new {
                    nombre = "Juan Pedro",
                    estatura = 1.90,
                    nacionalidad = "MEX"
                },
                new {
                    nombre = "Juan Perez",
                    estatura = 1.62,
                    nacionalidad = "CL"
                },
            };

            return personas;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<object>  Login([FromBody]Persona persona){
            string secret = this.conf.GetValue<string>("Secret");
            var jwtHelper = new JWTHelper(secret);
            var token = jwtHelper.CreateToken(persona.Usuario);

            return Ok(new {
                ok = true,
                msg = "Logeado con exíto.",
                token
            });
        }
    }
}