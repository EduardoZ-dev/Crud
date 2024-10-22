using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualLockerAPI.DTO_s;
using VirtualLockerAPI.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace VirtualLockerAPI.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AccesoController(ApplicationDbContext DbContext)
        {
            dbContext = DbContext;
        }

        [HttpGet]
        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(UsuarioDTO modelo)
        {

            if (modelo.Clave != modelo.ConfirmarClave)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            Usuario usuario = new Usuario()
            {
                NombreCompleto = modelo.NombreCompleto,
                Correo = modelo.Correo,
                Clave = modelo.Clave,
            };

            await dbContext.Usuarios.AddAsync(usuario);
            await dbContext.SaveChangesAsync();

            if(usuario.idUsuario != 0) return RedirectToAction("Login","Acceso");

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO modelo)
        {
            Usuario? usuario_encontrado = await dbContext.Usuarios
                .Where(u => u.Correo == modelo.Correo && u.Clave == modelo.Clave)
                .FirstOrDefaultAsync();

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontró el usuario";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
               new Claim(ClaimTypes.Name, usuario_encontrado.NombreCompleto)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }
        
    }
}
