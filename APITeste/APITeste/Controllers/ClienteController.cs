using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfraTeste.Models;
using InfraTeste.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APITeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepository Repo;
        public ClienteController(ConnectionStrings c)
        {
            Repo = new ClienteRepository(c);
        }
        /// <summary>
        ///  Listar Clientes
        /// </summary>
        /// <remarks> Informe um dos dados do cliente que você quer buscar </remarks>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Cliente>> Get([FromQuery]Cliente vm)
        {
            return Repo.Get(vm);
        }

        // POST api/values7
        [HttpPost]
        public ActionResult<Cliente> Post([FromBody] Cliente vm)
        {
            return Repo.Post(vm);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put([FromBody] Cliente vm)
        {
            Repo.Put(vm);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Repo.Delete(id);
        }
    }
}
