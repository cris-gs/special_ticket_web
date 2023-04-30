using Proyecto1SpecialTicket.DAL.Repositories.Implementations;
using Proyecto1SpecialTicket.Models.Entities;
using Proyecto1SpecialTicket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Proyecto1SpecialTicket.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for Evento
    /// </summary>
    public interface IEventoRepository : IGenericRepository<Evento>
    {
        Task<IEnumerable<Evento>> GetAllEventosAsync();
        Task<Evento> GetEventoByIdAsync(int? id);
        Task<IEnumerable<DetalleEvento>> GetDetalleEventosAsync();
        Task<DetalleEvento> GetDetalleEventosByIdAsync(int? id);
        Task<IEnumerable<DetalleAsiento>> GetDetalleAsientosAsync(int? id);
    }

    
}

