using Microsoft.AspNetCore.Http;
using Proyecto1SpecialTicket.DAL.Repositories;
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto1SpecialTicket.BLL.Services.Interfaces
{
    /// <summary>
    /// Service Interface for Entrada. 
    /// </summary>
    
    public interface IEntradaService
    {
        Task<IEnumerable<Entrada>> GetAllEntradasAsync();

        Task<Entrada> GetEntradaByIdAsync(int? id);

        Task<Entrada> GetEntradaByEventoAndAsientoAsync(int? idAsiento, int? idEvento);

        Task<Entrada> CreateEntradaAsync(Entrada entrada);

        Task<Entrada> UpdateEntradaAsync(Entrada entrada);

        Task<IEnumerable<DetalleEntrada>> GetDetalleEntradasAsync(int? id);

        //Task<Entrada> CreateEntradasAsync(IFormCollection form);

    }
}
