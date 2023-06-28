using Domain.Interfaces.IDespesa;
using Domain.Interfaces.InterfaceServicos;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[AllowAnonymous]
    public class DespesaController : ControllerBase
    {
        private readonly InterfaceDespesa _interfaceDespesa;
        private readonly IDespesaServico _iDespesaServico;
        public DespesaController(InterfaceDespesa interfaceDespesa,
                                   IDespesaServico iDespesaServico)
        {
            _interfaceDespesa = interfaceDespesa;
            _iDespesaServico = iDespesaServico;
        }
        //--------------------------------------------
        //LISTAR DESPESA DO USUARIO
        [HttpGet("/api/ListarDespesaUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarDespesaUsuario(string emailUsuario)
        {
            return await _interfaceDespesa.ListarDespesasUsuario(emailUsuario);
        }
        //--------------------------------------------
        //ADICIONAR DESPESA
        [HttpPost("/api/AdicionarDespesa")]
        [Produces("application/json")]
        public async Task<object> AdicionarDespesa(Despesa despesa)
        {
            await _iDespesaServico.AdicionarDespesa(despesa);
            return despesa;
        }
        //--------------------------------------------
        //ATUALIZAR DESPESA
        [HttpPut("/api/AtualizarDespesa")]
        [Produces("application/json")]
        public async Task<object> AtualizarDespesa(Despesa despesa)
        {
            await _iDespesaServico.AtualizarDespesa(despesa);
            return despesa;
        }
        //--------------------------------------------
        //OBTER DESPESA
        [HttpGet("/api/ObterDespesa")]
        [Produces("application/json")]
        public async Task<object> ObterDespesa(int id)
        {
            return await _interfaceDespesa.GetEntityById(id);
        }
        //--------------------------------------------
        //EXLUIR DESPESA
        [HttpDelete("/api/DeleteDespesa")]
        [Produces("application/json")]
        public async Task<object> DeleteDespesa(int id)
        {
            try
            {
                var despesa = await _interfaceDespesa.GetEntityById(id);
                await _interfaceDespesa.Delete(despesa);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        //--------------------------------------------
        //OBTER DESPESA
        [HttpGet("/api/CarregaGraficos")]
        [Produces("application/json")]
        public async Task<object> CarregaGraficos(string emailUsuario)
        {
            return await _iDespesaServico.CarregaGraficos(emailUsuario);
        }
        //--------------------------------------------
    }
}
