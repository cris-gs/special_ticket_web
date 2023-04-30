using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1SpecialTicket.BLL.Services.Interfaces;
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;

namespace Proyecto1SpecialTicket.Controllers
{
    public class DetalleEntradasController : Controller
    {
        //private readonly specialticketContext _context;
        private readonly IEntradaService _entradaService;

        public DetalleEntradasController(IEntradaService entradaService)
        {
            //_context = context;
            _entradaService = entradaService;
        }

        // GET: DetalleEntradas
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Index(int? id)
        {
            var listaEntradas = await _entradaService.GetDetalleEntradasAsync(id);

            return View(listaEntradas);
        }

        [Authorize(Roles = "Administrador, Cliente")]
        public IActionResult Buy(int? id)
        {
            if (id == null) return NotFound();

            return RedirectToAction("Create", "Compras", new { id });
        }

    }
}