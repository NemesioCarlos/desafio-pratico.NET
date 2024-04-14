using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using webApiVS.Models;
using webApiVS.Service.ContatoService;
//CONTROLLER ENCHUTO
namespace webApiVS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        //injeção de dependencia
        private readonly IContatoInterface _contatoInterface;
        public ContatoController(IContatoInterface contatoInterface)
        {
            _contatoInterface = contatoInterface;
        }

        [HttpGet]
        //retorna todos os registros
        public async Task<ActionResult<ServiceResponse<List<ContatoModel>>>> GetContatos()
        {
            return Ok(await _contatoInterface.GetContatos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ContatoModel>>> GetContatoById(int id)
        {
            ServiceResponse<ContatoModel> serviceResponse = await _contatoInterface.GetContatoById(id);

            return Ok(serviceResponse);
        }

        [HttpPut]
         public async Task<ActionResult<ServiceResponse<List<ContatoModel>>>> UpdateContato(ContatoModel editadoContato)
        {                                   //indo no interface e pegando o update func e pegando updade e colocando o editado
            ServiceResponse<List<ContatoModel>> serviceResponse = await _contatoInterface.UpdateContato(editadoContato);

            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<ContatoModel>>>> CreateContato(ContatoModel novoContato)
        {
            return Ok(await _contatoInterface.CreateContato(novoContato));
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<ContatoModel>>>> DeleteContato(int id)
        {
            ServiceResponse<List<ContatoModel>> serviceResponse = await _contatoInterface.DeleteContato(id);
            return Ok(serviceResponse);
        }


    }
}
