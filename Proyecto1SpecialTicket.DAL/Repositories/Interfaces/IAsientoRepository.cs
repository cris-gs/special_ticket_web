using Proyecto1SpecialTicket.DAL.Repositories.Implementations;
using Proyecto1SpecialTicket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Proyecto1SpecialTicket.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for Evento
    /// </summary>
    public interface IAsientoRepository : IGenericRepository<Asiento>
    {
        Task<IEnumerable<Asiento>> GetAllAsientosAsync();

        Task<Asiento> GetAsientosByIdAsync(int? id);
    }

    
}

