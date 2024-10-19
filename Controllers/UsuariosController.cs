using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ControlDocuments.Data;
using ControlDocuments.Models;
using System.Security.Cryptography;
using System.Text;

namespace ControlDocuments.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _db;

        public UsuarioController(AppDbContext context)
        {
            _db = context;
            Console.WriteLine("UsuarioController foi inicializado.");
        }

        // Método GET para exibir a página de registro
        public IActionResult Registrar()
        {
            return View();
        }

        // Método POST para processar o registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                // Criptografa a senha antes de salvar
                usuario.Senha = HashPassword(usuario.Senha);

                _db.Usuarios.Add(usuario);
                _db.SaveChanges();
                return RedirectToAction("Login");
            }

            // Retorna a view com o modelo caso haja erro de validação
            return View(usuario);
        }

        // Método GET para exibir a página de login
        public IActionResult Login()
        {
            Console.WriteLine("Ação Login foi chamada");
            return View();
        }

        // Método POST para processar o login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string senha)
        {
            // Aqui você deve usar a lógica de hashing que você já definiu em outro lugar
            string hashedPassword = HashPassword(senha);

            // Verifica se o usuário existe no banco de dados
            var usuario = _db.Usuarios.SingleOrDefault(u => u.Email == email && u.Senha == hashedPassword);

            if (usuario != null)
            {
                // Armazena os dados do usuário na sessão
                HttpContext.Session.SetInt32("UsuarioID", usuario.Id);
                HttpContext.Session.SetString("UsuarioNome", usuario.Nome);

                // Redireciona para a página inicial ou outra página desejada
                return RedirectToAction("Index", "Home");
            }

            // Se o login falhar, exibe uma mensagem de erro
            ViewBag.Message = "Email ou senha incorretos";
            return View(); // Retorna para a tela de login
        }

        // Método para sair (Logout)
        public IActionResult Logout()
        {
            // Limpa a sessão
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Método para criptografar a senha
        private string HashPassword(string senha)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
