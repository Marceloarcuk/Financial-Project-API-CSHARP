using Domain.Interfaces.ICategoria;
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
    public class CategoriaController : ControllerBase
    {

        //--------------------------------------------
        //CONTRUTOR DA CLASSE
        //--------------------------------------------
        private readonly InterfaceCategoria _interfaceCategoria;
        private readonly ICategoriaServico _iCategoriaServico;
        public CategoriaController(InterfaceCategoria interfaceCategoria,
                                   ICategoriaServico iCategoriaServico)
        {
            _interfaceCategoria = interfaceCategoria;
            _iCategoriaServico = iCategoriaServico;
        }
        //--------------------------------------------
        //LISTAR CATEGORIA DO USUARIO
        [HttpGet("/api/ListarCategoriaUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarCategoriaUsuario(string emailUsuario)
        {
            return await _interfaceCategoria.ListarCategoriaUsuario(emailUsuario);
        }
        //--------------------------------------------
        //ADICIONAR CATEGORIA
        [HttpPost("/api/AdicionarCategoria")]
        [Produces("application/json")]
        public async Task<object> AdicionarCategoria(Categoria categoria)
        {
            await _iCategoriaServico.AdicionarCategoria(categoria);
            return categoria;
        }
        //--------------------------------------------
        //ATUALIZAR CATEGORIA
        [HttpPut("/api/AtualizarCategoria")]
        [Produces("application/json")]
        public async Task<object> AtualizarCategoria(Categoria categoria)
        {
            await _iCategoriaServico.AtualizarCategoria(categoria);
            return categoria;
        }
        //--------------------------------------------
        //OBTER CATEGORIA
        [HttpGet("/api/ObterCategoria")]
        [Produces("application/json")]
        public async Task<object> ObterCategoria(int id)
        {
            return await _interfaceCategoria.GetEntityById(id);
        }
        //--------------------------------------------
        //EXLUIR CATEGORIA
        [HttpDelete("/api/DeleteCategoria")]
        [Produces("application/json")]
        public async Task<object> DeleteCategoria(int id)
        {
            try
            {
                var categoria = await _interfaceCategoria.GetEntityById(id);
                await _interfaceCategoria.Delete(categoria);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
