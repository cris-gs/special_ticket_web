using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1SpecialTicket.BLL.Services.Interfaces;
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;

namespace Proyecto1SpecialTicket.Controllers
{
    public class DetalleEventosController : Controller
    {
        //private readonly specialticketContext _context;
        private readonly IEventoService _eventoService;

        public DetalleEventosController(IEventoService eventoService)
        {
            //_context = context;
            _eventoService = eventoService;
        }

        // GET: DetalleEventos
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            var listaEventos = await _eventoService.GetDetalleEventosAsync();

            return View(listaEventos);
        }

        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Events()
        {
            var listaEventos = await _eventoService.GetDetalleEventosAsync();

            return View(listaEventos);
        }
        [Authorize(Roles = "Administrador, Cliente")]
        // GET: DetalleEventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var detalleEvento = await _eventoService.GetDetalleEventosByIdAsync(id);

            if (detalleEvento == null) return NotFound();

            return View(detalleEvento);
        }

        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> DetailsEvents(int? id)
        {
            if (id == null) return NotFound();

            var detalleEvento = await _eventoService.GetDetalleEventosByIdAsync(id);

            if (detalleEvento == null) return NotFound();

            return View(detalleEvento);
        }

        // Asientos
        [Authorize(Roles = "Administrador, Cliente")]
        public IActionResult Seats(int? id)
        {
            if (id == null) return NotFound();

            return RedirectToAction("Index", "DetalleAsientos", new { id });
        }

        [Authorize(Roles = "Administrador, Cliente")]
        public IActionResult Tickets(int? id)
        {
            if (id == null) return NotFound();

            return RedirectToAction("Index", "DetalleEntradas", new { id });
        }

    }
}
