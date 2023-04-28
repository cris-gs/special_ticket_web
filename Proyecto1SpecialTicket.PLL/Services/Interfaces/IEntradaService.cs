using Microsoft.AspNetCore.Http;
using Proyecto1SpecialTicket.DAL.Repositories;
using Proyecto1SpecialTicket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto1SpecialTicket.BLL.Services.Implementations
{
    /// <summary>
    /// Service Interface for Entrada. 
    /// </summary>
    
    public interface IEntradaService
    {
        Task<IEnumerable<Entrada>> GetAllEntradasAsync();

        Task<Entrada> GetEntradaByIdAsync(int? id);

        Task<Entrada> CreateEntradasAsync(IFormCollection form);

    }
}
