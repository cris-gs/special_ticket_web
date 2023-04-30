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
    /// Repository for Compra
    /// </summary>
    public class CompraRepository : GenericRepository<Compra>, ICompraRepository
    {
        /// <summary>
        /// Constructor of CompraRepository
        /// </summary>
        /// <param name="specialTicketContext"></param>
        public CompraRepository(specialticketContext _context) : base(_context)
        {   
        }

        public async Task<IEnumerable<Compra>> GetAllComprasAsync()
        {
            return await _context.Compras
                                 .Where(x=>x.Active)
                                 .ToListAsync();
        }

        public async Task<Compra> GetCompraByIdAsync(int? id)
        {
            var compra = await _context.Compras
                                 .Where(t => t.Active)
                                 .FirstOrDefaultAsync(m => m.Id == id);

            return compra;
        }

        public async Task<Compra> GetCompraAnteriorByIdAsync(int? id)
        {
            var compra = await _context.Compras
                                     .AsNoTracking()
                                     .Where(t => t.Active)
                                     .FirstOrDefaultAsync(m => m.Id == id);

            return compra;
        }

        public async Task<IEnumerable<Compra>> GetCompraByIdClienteAsync(string? id)
        {
            var compra = await _context.Compras
                                 .Where(t => t.Active && t.IdCliente == id)
                                 .ToListAsync();

            return compra;
        }

        public async Task<IEnumerable<ImprimirEntrada>> GetCompraByClienteAsync(string? idCliente)
        {
            var entradasReservadas = await _context.Compras
                                    .Where(c => c.IdCliente == idCliente && c.Active == true)
                                    .Join(_context.Entradas, c => c.IdEntrada, en => en.Id, (c, en) => new { Compra = c, Entrada = en })
                                    .Join(_context.Eventos, ce => ce.Entrada.IdEvento, ev => ev.Id, (ce, ev) => new { CompraEntrada = ce, Evento = ev })
                                    .Select(cev => new ImprimirEntrada
                                    {
                                        Id = cev.CompraEntrada.Compra.Id,
                                        Cantidad = cev.CompraEntrada.Compra.Cantidad,
                                        FechaReserva = cev.CompraEntrada.Compra.FechaReserva,
                                        FechaPago = cev.CompraEntrada.Compra.FechaPago,
                                        TipoAsiento = cev.CompraEntrada.Entrada.TipoAsiento,
                                        Precio = cev.CompraEntrada.Entrada.Precio,
                                        Total = cev.CompraEntrada.Entrada.Precio * cev.CompraEntrada.Compra.Cantidad,
                                        Evento = cev.Evento.Descripcion
                                    }).ToListAsync();

            return entradasReservadas;
        }

        public DateTime GetFechaReserva(int? id)
        {
            var fechaReserva = _context.Compras
                        .Where(te => te.Id == id)
                        .Select(te => te.FechaReserva)
                        .FirstOrDefault();

            return fechaReserva;
        }

        public async Task<List<Compra>> GetEntradaCompradaByIdAsync(int? id)
        {
            var listaEntradaComprada = await _context.Compras
                                    .Where(c => c.Id == id)
                                    .Select(cev => new Compra
                                    {
                                        Id = cev.Id,
                                        Cantidad = cev.Cantidad,
                                        FechaReserva = cev.FechaReserva,
                                        FechaPago = cev.FechaPago,
                                        CreatedAt = cev.CreatedAt,
                                        CreatedBy = cev.CreatedBy,
                                        UpdatedAt = cev.UpdatedAt,
                                        UpdatedBy = cev.UpdatedBy,
                                        Active = cev.Active,
                                        IdCliente = cev.IdCliente,
                                        IdEntrada = cev.IdEntrada
                                    }).ToListAsync();

            return listaEntradaComprada;
        }
    }
}