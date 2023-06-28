using Domain.Interfaces.InterfaceServicos;
using Domain.Interfaces.ISistemaFinanceiro;
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
    public class SistemaFinanceiroController : ControllerBase
    {
        //--------------------------------------------
        //CONTRUTOR DA CLASSE
        //--------------------------------------------
        private readonly InterfaceSistemaFinanceiro _interfaceSistemaFinanceiro;
        private readonly ISistemaFinanceiroServico _iSistemaFinanceiroServico;
        
        public SistemaFinanceiroController(InterfaceSistemaFinanceiro interfaceSistemaFinanceiro,
            ISistemaFinanceiroServico iSistemaFinanceiroServico)
        {
            _interfaceSistemaFinanceiro = interfaceSistemaFinanceiro;
            _iSistemaFinanceiroServico = iSistemaFinanceiroServico;
        }
        //--------------------------------------------


        //--------------------------------------------
        //LISTAR USUARIOS
        [HttpGet("/api/ListaSistemasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListaSistemasUsuario(string emailUsuario)
        {
            return await _interfaceSistemaFinanceiro.ListaSistemasUsuario(emailUsuario);
        }
        //--------------------------------------------
        //ADICIONAR SISTEMA FINANCEIRO
        [HttpPost("/api/AdicionarSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            await _iSistemaFinanceiroServico.AdicionarSistemaFinanceiro(sistemaFinanceiro);
            return Task.FromResult(sistemaFinanceiro);
        }
        //--------------------------------------------
        //ATUALIZAR  SISTEMA FINANCEIRO
        [HttpPut("/api/AtualizarSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> AtualizarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            await _iSistemaFinanceiroServico.AtualizarSistemaFinanceiro(sistemaFinanceiro);
            return Task.FromResult(sistemaFinanceiro);
        }
        //--------------------------------------------
        //OBTER  SISTEMA FINANCEIRO
        [HttpGet("/api/ObterSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> ObterSistemaFinanceiro(int Id)
        {
            return await _interfaceSistemaFinanceiro.GetEntityById(Id);
        }

        //--------------------------------------------
        //DELETAR  SISTEMA FINANCEIRO
        [HttpDelete("/api/DeleteSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> DeleteSistemaFinanceiro(int Id)
        {
            try
            {
                var sistemaFinanceiro = await _interfaceSistemaFinanceiro.GetEntityById(Id);
                await _interfaceSistemaFinanceiro.Delete(sistemaFinanceiro);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;

        }

    }
}
