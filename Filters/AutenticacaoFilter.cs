using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ControlDocuments.Filters
{
    public class AutenticacaoFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var usuarioId = context.HttpContext.Session.GetInt32("UsuarioID");

            // Se o usuário não está logado, redireciona para a tela de login
            if (usuarioId == null)
            {
                context.Result = new RedirectToActionResult("Login", "Usuario", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
