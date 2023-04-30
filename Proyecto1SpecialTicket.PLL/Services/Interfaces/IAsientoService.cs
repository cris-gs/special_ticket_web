using Proyecto1SpecialTicket.DAL.Repositories;
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto1SpecialTicket.BLL.Services.Interfaces
{
    /// <summary>
    /// Service Interface for Asiento. 
    /// </summary>
    
    public interface IAsientoService
    {
        Task<IEnumerable<Asiento>> GetAllAsientosAsync();

        Task<Asiento> GetAsientosByIdAsync(int? id);

        Task<Asiento> CreateAsientosAsync(Asiento asiento);

        Task<Asiento> UpdateAsientosAsync(Asiento asiento);


    }
}
