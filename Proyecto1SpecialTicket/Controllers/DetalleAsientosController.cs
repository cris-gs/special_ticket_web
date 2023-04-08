using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;

namespace Proyecto1SpecialTicket.Controllers
{
    public class DetalleAsientosController : Controller
    {
        private readonly SpecialticketContext _context;

        public DetalleAsientosController(SpecialticketContext context)
        {
            _context = context;
        }

        // GET: DetalleAsientos
        public IActionResult Index(int? id)
        {
            var listaAsientos = _context.Eventos
                              .Join(_context.TipoEventos, e => e.IdTipoEvento, te => te.Id, (e, te) => new { Evento = e, TipoEvento = te })
                              .Join(_context.Escenarios, ev => ev.Evento.IdEscenario, esc => esc.Id, (ev, esc) => new { ev, Escenario = esc })
                              .Join(_context.Asientos, es => es.Escenario.Id, a => a.IdEscenario, (es, a) => new { es, Asiento = a })
                              .Where(x => x.es.ev.Evento.Active && x.es.ev.Evento.Id == id)
                              .Select(x => new DetalleAsiento
                              {
                                  Id = x.Asiento.Id,
                                  IdEvento = x.es.ev.Evento.Id,
                                  TipoAsiento = x.Asiento.Descripcion,
                                  Cantidad = x.Asiento.Cantidad
                              }).ToList();

            return View(listaAsientos);
        }

        // Crear entradas
        public IActionResult Create(int? id, int? idE)
        {
            return RedirectToAction("Create", "Entradas", new { id = id, idE = idE });
        }

    }
}
