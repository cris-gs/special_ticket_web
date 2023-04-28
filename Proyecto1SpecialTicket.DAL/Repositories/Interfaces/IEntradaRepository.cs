using Proyecto1SpecialTicket.DAL.Repositories.Implementations;
using Proyecto1SpecialTicket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Proyecto1SpecialTicket.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for Entrada
    /// </summary>
    public interface IEntradaRepository : IGenericRepository<Entrada>
    {
        Task<IEnumerable<Entrada>> GetAllEntradasAsync();

        Task<Entrada> GetEntradaByIdAsync(int? id);

        Task<Entrada> GetEntradaByIdEventoAsync(int? id);

    }

    
}

