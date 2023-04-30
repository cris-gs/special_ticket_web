using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1SpecialTicket.BLL.Services.Interfaces;
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;

namespace Proyecto1SpecialTicket.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class DetalleAsientosController : Controller
    {
        //private readonly specialticketContext _context;
        private readonly IEventoService _eventoService;

        public DetalleAsientosController(IEventoService eventoService)
        {
            //_context = context;
            _eventoService = eventoService;
        }

        // GET: DetalleAsientos
        public async Task<IActionResult> Index(int? id)
        {
            var listaAsientos = await _eventoService.GetDetalleAsientosAsync(id);

            return View(listaAsientos);
        }

        // Crear entradas
        public IActionResult Create(int? idAsiento, int? idEvento)
        {
            return RedirectToAction("Create", "Entradas", new { idAsiento, idEvento });
        }

    }
}
