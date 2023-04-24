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
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;

namespace Proyecto1SpecialTicket.Controllers
{
    public class DetalleEventosController : Controller
    {
        private readonly specialticketContext _context;

        public DetalleEventosController(specialticketContext context)
        {
            _context = context;
        }

        // GET: DetalleEventos
        public List<DetalleEvento> GetEvents()
        {
            //var now = DateTime.Now;
            //var listaEventos = _context.Eventos
            //                  .Join(_context.TipoEventos, evnt => evnt.IdTipoEvento, tev => tev.Id, (evnt, tev) => new { evnt, tev })
            //                  .Join(_context.Escenarios, x => x.evnt.IdTipoEvento, esc => esc.Id, (ev, esc) => new { ev, esc })
            //                  .Join(_context.TipoEscenarios, y => y.esc.Id, te => te.IdEscenario, (esc, te) => new { esc, te })
            //                  .Where(e => e.esc.ev.evnt.Fecha > now)
            //                  .OrderBy(e => e.esc.ev.evnt.Id)
            //                  .Select(m => new DetalleEvento
            //                  {
            //                      Id = m.esc.ev.evnt.Id,
            //                      Descripcion = m.esc.ev.evnt.Descripcion,
            //                      TipoEvento = m.esc.ev.tev.Descripcion,
            //                      Fecha = m.esc.ev.evnt.Fecha,
            //                      TipoEscenario = m.te.Descripcion,
            //                      Escenario = m.esc.esc.Nombre,
            //                      Localizacion = m.esc.esc.Localizacion,
            //                  }).ToList();

            var now = DateTime.Now;
            var query = from ev in _context.Eventos
                        join tev in _context.TipoEventos on ev.IdTipoEvento equals tev.Id
                        join esc in _context.Escenarios on ev.IdEscenario equals esc.Id
                        join te in _context.TipoEscenarios on esc.Id equals te.IdEscenario
                        where ev.Fecha > now
                        orderby ev.Id ascending
                        select new DetalleEvento
                        {
                            Id = ev.Id,
                            Descripcion = ev.Descripcion,
                            TipoEvento = tev.Descripcion,
                            Fecha = ev.Fecha,
                            TipoEscenario = te.Descripcion,
                            Escenario = esc.Nombre,
                            Localizacion = esc.Localizacion
                        };

            var eventos = new List<DetalleEvento>();
            foreach (var item in query)
            {
                eventos.Add(new DetalleEvento
                {
                    Id = item.Id,
                    Descripcion = item.Descripcion,
                    TipoEvento = item.TipoEvento,
                    Fecha = item.Fecha,
                    TipoEscenario = item.TipoEscenario,
                    Escenario = item.Escenario,
                    Localizacion = item.Localizacion
                });
            }
            return eventos;
        }

        // GET: DetalleEventos
        [Authorize(Roles = "Administrador")]
        public IActionResult Index()
        {
            var listaEventos = GetEvents();

            return View(listaEventos);
        }

        [Authorize(Roles = "Administrador, Cliente")]
        public IActionResult Events()
        {
            var listaEventos = GetEvents();

            return View(listaEventos);
        }
        [Authorize(Roles = "Administrador, Cliente")]
        // GET: DetalleEventos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.DetalleEvento == null)
            {
                return NotFound();
            }

            //var detalleEvento = _context.Eventos
            //                    .Join(_context.TipoEventos, evnt => evnt.IdTipoEvento, tev => tev.Id, (evnt, tev) => new { evnt, tev })
            //                    .Join(_context.Escenarios, x => x.evnt.IdTipoEvento, esc => esc.Id, (ev, esc) => new { ev, esc })
            //                    .Join(_context.TipoEscenarios, y => y.esc.Id, te => te.IdEscenario, (esc, te) => new { esc, te })
            //                    .Where(e => e.esc.ev.evnt.Id == id)
            //                    .Select(m => new DetalleEvento
            //                    {
            //                        Id = m.esc.ev.evnt.Id,
            //                        Descripcion = m.esc.ev.evnt.Descripcion,
            //                        TipoEvento = m.esc.ev.tev.Descripcion,
            //                        Fecha = m.esc.ev.evnt.Fecha,
            //                        TipoEscenario = m.te.Descripcion,
            //                        Escenario = m.esc.esc.Nombre,
            //                        Localizacion = m.esc.esc.Localizacion,
            //                    })
            //                    .SingleOrDefault();

            var detalleEvento = (from ev in _context.Eventos
                        join tev in _context.TipoEventos on ev.IdTipoEvento equals tev.Id
                        join esc in _context.Escenarios on ev.IdEscenario equals esc.Id
                        join te in _context.TipoEscenarios on esc.Id equals te.IdEscenario
                        where ev.Id == id
                        select new DetalleEvento
                        {
                            Id = ev.Id,
                            Descripcion = ev.Descripcion,
                            TipoEvento = tev.Descripcion,
                            Fecha = ev.Fecha,
                            TipoEscenario = te.Descripcion,
                            Escenario = esc.Nombre,
                            Localizacion = esc.Localizacion
                        }).FirstOrDefault();

            if (detalleEvento == null)
            {
                return NotFound();
            }

            return View(detalleEvento);
        }

        [Authorize(Roles = "Administrador, Cliente")]
        public IActionResult DetailsEvents(int? id)
        {
            if (id == null || _context.DetalleEvento == null)
            {
                return NotFound();
            }

            //var detalleEvento = _context.Eventos
            //                    .Join(_context.TipoEventos, evnt => evnt.IdTipoEvento, tev => tev.Id, (evnt, tev) => new { evnt, tev })
            //                    .Join(_context.Escenarios, x => x.evnt.IdTipoEvento, esc => esc.Id, (ev, esc) => new { ev, esc })
            //                    .Join(_context.TipoEscenarios, y => y.esc.Id, te => te.IdEscenario, (esc, te) => new { esc, te })
            //                    .Where(e => e.esc.ev.evnt.Id == id)
            //                    .Select(m => new DetalleEvento
            //                    {
            //                        Id = m.esc.ev.evnt.Id,
            //                        Descripcion = m.esc.ev.evnt.Descripcion,
            //                        TipoEvento = m.esc.ev.tev.Descripcion,
            //                        Fecha = m.esc.ev.evnt.Fecha,
            //                        TipoEscenario = m.te.Descripcion,
            //                        Escenario = m.esc.esc.Nombre,
            //                        Localizacion = m.esc.esc.Localizacion,
            //                    })
            //                    .SingleOrDefault();

            var detalleEvento = (from ev in _context.Eventos
                                 join tev in _context.TipoEventos on ev.IdTipoEvento equals tev.Id
                                 join esc in _context.Escenarios on ev.IdEscenario equals esc.Id
                                 join te in _context.TipoEscenarios on esc.Id equals te.IdEscenario
                                 where ev.Id == id
                                 select new DetalleEvento
                                 {
                                     Id = ev.Id,
                                     Descripcion = ev.Descripcion,
                                     TipoEvento = tev.Descripcion,
                                     Fecha = ev.Fecha,
                                     TipoEscenario = te.Descripcion,
                                     Escenario = esc.Nombre,
                                     Localizacion = esc.Localizacion
                                 }).FirstOrDefault();

            if (detalleEvento == null)
            {
                return NotFound();
            }

            return View(detalleEvento);
        }

        // Asientos
        [Authorize(Roles = "Administrador, Cliente")]
        public IActionResult Seats(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "DetalleAsientos", new { id = id });
        }

        [Authorize(Roles = "Administrador, Cliente")]
        public IActionResult Tickets(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "DetalleEntradas", new { id = id });
        }

    }
}
