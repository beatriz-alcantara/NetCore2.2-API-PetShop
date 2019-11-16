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
    public class LojaClienteController : ControllerBase
    {
        private readonly LojasClientesRepository Repo;
        public LojaClienteController(ConnectionStrings c)
        {
            Repo = new LojasClientesRepository(c);
        }
        /// <summary>
        ///  Listar Relacionamentos de clientes com loja
        /// </summary>
        /// <remarks> Busque por uma relação </remarks>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<LojasClientes>> Get([FromQuery]LojasClientes vm)
        {
            return Repo.Get(vm);
        }

        /// <summary>
        ///  Cadastrar relação de Loja com Cliente
        /// </summary>
        /// <remarks> Informe o id do cliente e o da loja a serem relacionados </remarks>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<LojasClientes> Post([FromBody] LojasClientes vm)
        {
            return Repo.Post(vm);
        }


        /// <summary>
        /// Delete uma Relação de loja com cliente
        /// </summary>
        /// <remarks> Informe o id da relação que você quer apagar </remarks>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Repo.Delete(id);
        }
    }
}
