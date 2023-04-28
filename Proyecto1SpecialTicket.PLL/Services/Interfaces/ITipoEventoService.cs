using Proyecto1SpecialTicket.DAL.Repositories;
using Proyecto1SpecialTicket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto1SpecialTicket.BLL.Services.Implementations
{
    /// <summary>
    /// Service Interface for TipoEvento. 
    /// </summary>
    
    public interface ITipoEventoService
    {

        Task<IEnumerable<TipoEvento>> GetAllTipoEventosAsync();
    }
}
