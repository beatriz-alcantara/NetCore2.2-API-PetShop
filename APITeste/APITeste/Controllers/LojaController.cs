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
    public class LojaController : ControllerBase
    {
        private readonly LojaRepository Repo;
        public LojaController(ConnectionStrings c)
        {
            Repo = new LojaRepository(c);
        }
        /// <summary>
        ///  Listar Lojas
        /// </summary>
        /// <remarks> Informe um dos dados do Loja que você quer buscar </remarks>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Lojas>> Get([FromQuery]Lojas vm)
        {
            return Repo.Get(vm);
        }

        /// <summary>
        ///  Cadastrar Loja
        /// </summary>
        /// <remarks> Informe o nome da loja que você quer cadastrar </remarks>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Lojas> Post([FromBody] Lojas vm)
        {
            return Repo.Post(vm);
        }

        /// <summary>
        /// Atualize uma Loja
        /// </summary>
        /// <remarks> Digite o nome e o id que você quer atualizar </remarks>
        /// <param name="vm"></param>
        [HttpPut("{id}")]
        public void Put([FromBody] Lojas vm)
        {
            Repo.Put(vm);
        }

        /// <summary>
        /// Delete uma loja
        /// </summary>
        /// <remarks> Informe o id da loja que você quer apagar </remarks>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Repo.Delete(id);
        }
    }
}
