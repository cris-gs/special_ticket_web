using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1SpecialTicket.IdentityData;
using Proyecto1SpecialTicket.BLL.Services.Interfaces;
using Proyecto1SpecialTicket.Models;

namespace Proyecto1SpecialTicket.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AsientosController : Controller
    {
        //private readonly specialticketContext _context;
        private readonly IAsientoService _asientoService;
        private readonly IEscenarioService _escenarioService;
        private readonly UserManager<Proyecto1SpecialTicketUser> _userManager;

        public AsientosController(IAsientoService asientoService, IEscenarioService escenarioService, UserManager<Proyecto1SpecialTicketUser> userManager)
        {
            //_context = context;
            _asientoService = asientoService;
            _escenarioService = escenarioService;
            _userManager = userManager;
        }

        // GET: Asientos
        public async Task<IActionResult> Index()
        {
            return View(await _asientoService.GetAllAsientosAsync());
        }

        // GET: Asientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var asiento = await _asientoService.GetAsientosByIdAsync(id);

            if (asiento == null) return NotFound();

            return View(asiento);
        }

        // GET: Asientos/Create
        public async Task<IActionResult> Create()
        {
            var listaEscenarios = await _escenarioService.GetAllEscenariosAsync();
            ViewData["IdEscenario"] = new SelectList(listaEscenarios, "Id", "Nombre");
            return View();
        }

        // POST: Asientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Cantidad,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdEscenario")] Asiento asiento)
        {
            var escenarioNavigation = await _escenarioService.GetEscenariosByIdAsync(asiento.IdEscenario);
            asiento.IdEscenarioNavigation = escenarioNavigation;

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                asiento.CreatedBy = userId;
                asiento.UpdatedBy = userId;
                //_context.Add(asiento);
                //await _context.SaveChangesAsync();
                await _asientoService.CreateAsientosAsync(asiento);
                return RedirectToAction(nameof(Index));
            }
            var listaEscenarios = await _escenarioService.GetAllEscenariosAsync();
            ViewData["IdEscenario"] = new SelectList(listaEscenarios, "Id", "Nombre");
            return View(asiento);
        }

        // GET: Asientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var asiento = await _asientoService.GetAsientosByIdAsync(id);
            if (asiento == null) return NotFound();

            var listaEscenarios = await _escenarioService.GetAllEscenariosAsync();
            ViewData["IdEscenario"] = new SelectList(listaEscenarios, "Id", "Nombre", asiento.IdEscenario);
            return View(asiento);
        }

        // POST: Asientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Cantidad,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdEscenario")] Asiento asiento)
        {
            if (id != asiento.Id) return NotFound();

            var escenarioNavigation = await _escenarioService.GetEscenariosByIdAsync(asiento.IdEscenario);
            asiento.IdEscenarioNavigation = escenarioNavigation;

            //if (ModelState.IsValid)
            //{
            //}
            try
            {
                //var fechaCreacion = _context.Asientos
                //    .Where(te => te.Id == asiento.Id)
                //    .Select(te => te.CreatedAt)
                //    .FirstOrDefault();
                //asiento.CreatedAt = fechaCreacion;
                var userId = _userManager.GetUserId(User);
                asiento.UpdatedBy = userId;
                await _asientoService.UpdateAsientosAsync(asiento);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsientoExists(asiento.Id))
                {
                    return NotFound();
                }
                else
                {
                    var listaEscenarios = await _escenarioService.GetAllEscenariosAsync();
                    ViewData["IdEscenario"] = new SelectList(listaEscenarios, "Id", "Nombre", asiento.IdEscenario);
                    return View(asiento);
                }
            }
            
            
        }

        // GET: Asientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var asiento = await _asientoService.GetAsientosByIdAsync(id);

            if (asiento == null) return NotFound();

            return View(asiento);
        }

        // POST: Asientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asiento = await _asientoService.GetAsientosByIdAsync(id);
            asiento.Active = false;

            try
            {
                await _asientoService.UpdateAsientosAsync(asiento);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsientoExists(asiento.Id))
                    return NotFound();
                else throw;
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool AsientoExists(int id)
        {
          return _asientoService.GetAsientosByIdAsync(id) == null ? true : false;
        }
    }
}
