using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(UserManager<ApplicationUser> userManager,
                               SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [AllowAnonymous]     // QUALQUER USUARIO PODE ACESSAR
        [Produces("application/json")]
        [HttpPost("/api/AdicionarUsuario")]
        public async Task<IActionResult> AdicionarUsuario([FromBody] UserLogin userLogin)
        {
            if (string.IsNullOrWhiteSpace(userLogin.Email) ||
                string.IsNullOrWhiteSpace(userLogin.cpf) ||
                string.IsNullOrWhiteSpace(userLogin.Senha))
            {
                return Ok("Falta alguns dados.");
            }
            
            var user = new ApplicationUser
            {
                Email = userLogin.Email,
                UserName = userLogin.Email,
                CPF = userLogin.cpf
             };
            
            var result = await _userManager.CreateAsync(user, userLogin.Senha);
            if(result.Errors.Any())
            {
                return Ok(result.Errors);
            }


            // GERACAO DE CONFIRMACAO POR EMAIL - precisa implementar
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // RETORNO DO EMAIL DE CONFIRMACAO
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var response_Retorno = await _userManager.ConfirmEmailAsync(user, code);
            if (response_Retorno.Succeeded)
            {
                return Ok("Usuario adicionado");
            }
            else
            {
                return Ok("Erro ao confirmar usuario.");
            }


        }

    }

}
