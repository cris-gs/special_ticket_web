using Proyecto1SpecialTicket.DAL.Repositories.Interfaces;
using Proyecto1SpecialTicket.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Proyecto1SpecialTicket.DAL.DataContext;
using System.Linq;
using Proyecto1SpecialTicket.Models.Entities;

namespace Proyecto1SpecialTicket.DAL.Repositories.Implementations
{
    /// <summary>
    /// Repository for Entrada
    /// </summary>
    public class EntradaRepository : GenericRepository<Entrada>, IEntradaRepository
    {
        /// <summary>
        /// Constructor of EntradaRepository
        /// </summary>
        /// <param name="specialTicketContext"></param>
        public EntradaRepository(specialticketContext _context) : base(_context)
        {   
        }

        public async Task<IEnumerable<Entrada>> GetAllEntradasAsync()
        {
            return await _context.Entradas
                                 .Where(x=>x.Active)
                                 .ToListAsync();
        }

        public async Task<Entrada> GetEntradaByIdAsync(int? id)
        {
            var entrada = await _context.Entradas
                                 .Where(t => t.Active)
                                 .FirstOrDefaultAsync(m => m.Id == id);

            return entrada;
        }

        public async Task<Entrada> GetEntradaByEventoAndAsientoAsync(int? idAsiento, int? idEvento)
        {
            var asiento = await _context.Eventos
                              .Join(_context.TipoEventos, e => e.IdTipoEvento, te => te.Id, (e, te) => new { Evento = e, TipoEvento = te })
                              .Join(_context.Escenarios, ev => ev.Evento.IdEscenario, esc => esc.Id, (ev, esc) => new { ev, Escenario = esc })
                              .Join(_context.Asientos, es => es.Escenario.Id, a => a.IdEscenario, (es, a) => new { es, Asiento = a })
                              .Where(x => x.es.ev.Evento.Active && x.Asiento.Id == idAsiento && x.es.ev.Evento.Id == idEvento)
                              .Select(x => new Entrada
                              {
                                  IdEvento = x.es.ev.Evento.Id,
                                  TipoAsiento = x.Asiento.Descripcion,
                                  Disponibles = x.Asiento.Cantidad
                              })
                              .FirstOrDefaultAsync();

            return asiento;
        }

        public async Task<IEnumerable<DetalleEntrada>> GetDetalleEntradasAsync(int? id)
        {
            var listaEntradas = await _context.Entradas
                    .Where(e => e.IdEvento == id && e.Active == true)
                    .Select(e => new DetalleEntrada
                    {
                        Id = e.Id,
                        Disponibles = e.Disponibles,
                        TipoAsiento = e.TipoAsiento,
                        Precio = e.Precio
                    }).ToListAsync();

            return listaEntradas;
        }
    }
}