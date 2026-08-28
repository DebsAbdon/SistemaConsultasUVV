using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaConsultasUVV.Data;
using SistemaConsultasUVV.Models;
using System.Security.Claims;

namespace SistemaConsultasUVV.Controllers
{
    [Authorize]
    public class ConsultaController : Controller
    {
        private readonly AppDbContext _context;

        public ConsultaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Consulta
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usuarioId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var consultas = await _context.Consultas
                .Where(c => c.UsuarioId == usuarioId)
                .OrderBy(c => c.DataHora)
                .ToListAsync();

            return View(consultas);
        }

        // GET: /Consulta/Cadastro
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        // POST: /Consulta/Cadastro
        [HttpPost]
        public async Task<IActionResult> Cadastro(Consulta consulta)
        {
            if (!ModelState.IsValid)
            {
                return View(consulta);
            }

            consulta.UsuarioId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: /Consulta/Editar/5
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuarioId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c =>
                    c.Id == id && c.UsuarioId == usuarioId);

            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // POST: /Consulta/Editar
        [HttpPost]
        public async Task<IActionResult> Editar(Consulta consulta)
        {
            if (!ModelState.IsValid)
            {
                return View(consulta);
            }

            var usuarioId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var consultaExistente = await _context.Consultas
                .FirstOrDefaultAsync(c =>
                    c.Id == consulta.Id &&
                    c.UsuarioId == usuarioId);

            if (consultaExistente == null)
            {
                return NotFound();
            }

            consultaExistente.Especialidade = consulta.Especialidade;
            consultaExistente.DataHora = consulta.DataHora;
            consultaExistente.Descricao = consulta.Descricao;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // POST: /Consulta/Excluir
        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            var usuarioId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c =>
                    c.Id == id && c.UsuarioId == usuarioId);

            if (consulta == null)
            {
                return NotFound();
            }

            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
