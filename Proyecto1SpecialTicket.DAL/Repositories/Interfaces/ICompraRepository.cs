using Proyecto1SpecialTicket.DAL.Repositories.Implementations;
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Proyecto1SpecialTicket.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for Compra
    /// </summary>
    public interface ICompraRepository : IGenericRepository<Compra>
    {
        Task<IEnumerable<Compra>> GetAllComprasAsync();

        Task<Compra> GetCompraByIdAsync(int? id);

        Task<Compra> GetCompraAnteriorByIdAsync(int? id);

        Task<IEnumerable<Compra>> GetCompraByIdClienteAsync(string? id);

        Task<IEnumerable<ImprimirEntrada>> GetCompraByClienteAsync(string? idCliente);

        DateTime GetFechaReserva(int? id);

        Task<List<Compra>> GetEntradaCompradaByIdAsync(int? id);
    }

    
}

