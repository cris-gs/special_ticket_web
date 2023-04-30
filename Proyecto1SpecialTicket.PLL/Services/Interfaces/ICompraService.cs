using Microsoft.AspNetCore.Http;
using Proyecto1SpecialTicket.DAL.Repositories;
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto1SpecialTicket.BLL.Services.Interfaces
{
    /// <summary>
    /// Service Interface for Compra. 
    /// </summary>
    
    public interface ICompraService
    {
        Task<IEnumerable<Compra>> GetAllComprasAsync();

        Task<Compra> GetCompraByIdAsync(int? id);

        Task<Compra> GetCompraAnteriorByIdAsync(int? id);

        Task<IEnumerable<Compra>> GetCompraByIdClienteAsync(string? id);

        Task<Compra> CreateCompraAsync(Compra compra);

        Task<Compra> UpdateCompraAsync(Compra compra);

        Task<IEnumerable<ImprimirEntrada>> GetCompraByClienteAsync(string? idCliente);

        DateTime GetFechaReserva(int? id);

        Task<List<Compra>> GetEntradaCompradaByIdAsync(int? id);

    }
}
