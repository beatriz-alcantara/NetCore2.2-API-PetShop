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
    public class PetController : ControllerBase
    {
        private readonly PetRepository Repo;
        public PetController(ConnectionStrings c)
        {
            Repo = new PetRepository(c);
        }
        /// <summary>
        ///  Listar Pets
        /// </summary>
        /// <remarks> Busque um pet </remarks>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Pets>> Get([FromQuery]Pets vm)
        {
            return Repo.Get(vm);
        }

        /// <summary>
        ///  Cadastrar Pet
        /// </summary>
        /// <remarks> Informe o nome do pet que você quer cadastrar </remarks>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Pets> Post([FromBody] Pets vm)
        {
            return Repo.Post(vm);
        }

        /// <summary>
        /// Atualize um Pet
        /// </summary>
        /// <remarks> Informe e os dados que você quer atualizar </remarks>
        /// <param name="vm"></param>
        [HttpPut("{id}")]
        public void Put([FromBody] Pets vm)
        {
            Repo.Put(vm);
        }

        /// <summary>
        /// Delete um pet
        /// </summary>
        /// <remarks> Informe o id do pet que você quer apagar </remarks>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Repo.Delete(id);
        }
    }
}
