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
using Proyecto1SpecialTicket.Areas.Identity.Data;
using Proyecto1SpecialTicket.BLL.Services.Implementations;
using Proyecto1SpecialTicket.Models;
using SkiaSharp;

namespace Proyecto1SpecialTicket.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class TipoEscenariosController : Controller
    {
        private readonly ITipoEscenarioService _tipoEscenarioService;
        private readonly IEscenarioService _escenarioService;
        private readonly UserManager<Proyecto1SpecialTicketUser> _userManager;

        public TipoEscenariosController(ITipoEscenarioService tipoEscenario, IEscenarioService escenarioService, UserManager<Proyecto1SpecialTicketUser> userManager)
        {
            _tipoEscenarioService = tipoEscenario;
            _escenarioService = escenarioService;
            _userManager = userManager;
        }

        // GET: TipoEscenarios
        public async Task<IActionResult> Index()
        {
            return View(await _tipoEscenarioService.GetAllTipoEscenariosAsync());
        }

        // GET: TipoEscenarios/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null) return NotFound();

            var tipoEscenario = await _tipoEscenarioService.GetTipoEscenarioByIdAsync(id);
            if (tipoEscenario == null) return NotFound();

            return View(tipoEscenario);
        }

        // GET: TipoEscenarios/Create
        public async Task<IActionResult> Create()
        {
            var listaEscenarios = await _escenarioService.GetAllEscenariosAsync();
            ViewData["IdEscenario"] = new SelectList(listaEscenarios, "Id", "Nombre");
            return View();
        }

        // POST: TipoEscenarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdEscenario")] TipoEscenario tipoEscenario)
        {
            var escenarioNavigation = await _escenarioService.GetEscenariosByIdAsync(tipoEscenario.IdEscenario);
            tipoEscenario.IdEscenarioNavigation = escenarioNavigation;

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                tipoEscenario.CreatedBy = userId;
                tipoEscenario.UpdatedBy = userId;
                await _tipoEscenarioService.CreateTipoEscenariosAsync(tipoEscenario);
 
                return RedirectToAction(nameof(Index));
            }
            var listaEscenarios = await _escenarioService.GetAllEscenariosAsync();
            ViewData["IdEscenario"] = new SelectList(listaEscenarios, "Id", "Nombre");
            return View(tipoEscenario);
        }

        // GET: TipoEscenarios/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return NotFound();

            var tipoEscenario = await _tipoEscenarioService.GetTipoEscenarioByIdAsync(id);
            if (tipoEscenario == null) return NotFound();

            var listaEscenarios = await _escenarioService.GetAllEscenariosAsync();
            ViewData["IdEscenario"] = new SelectList(listaEscenarios, "Id", "Nombre");
            return View(tipoEscenario);
        }

        // POST: TipoEscenarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdEscenario")] TipoEscenario tipoEscenario)
        {
            if (id != tipoEscenario.Id) return NotFound();

            var escenarioNavigation = await _escenarioService.GetEscenariosByIdAsync(tipoEscenario.IdEscenario);
            tipoEscenario.IdEscenarioNavigation = escenarioNavigation;

            //if (ModelState.IsValid)
            //{
            //}
            try
            {
                //var fechaCreacion = _context.TipoEscenarios
                //    .Where(te => te.Id == tipoEscenario.Id)
                //    .Select(te => te.CreatedAt)
                //    .FirstOrDefault();
                //tipoEscenario.CreatedAt = fechaCreacion;
                var userId = _userManager.GetUserId(User);
                tipoEscenario.UpdatedBy = userId;

                await _tipoEscenarioService.UpdateTipoEscenariosAsync(tipoEscenario);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoEscenarioExists(tipoEscenario.Id))
                {
                    return NotFound();
                }
                else
                {
                    var listaEscenarios = await _escenarioService.GetAllEscenariosAsync();
                    ViewData["IdEscenario"] = new SelectList(listaEscenarios, "Id", "Nombre");

                    return View(tipoEscenario);
                }
            }
            
            
        }

        // GET: TipoEscenarios/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();

            var tipoEscenario = await _tipoEscenarioService.GetTipoEscenarioByIdAsync(id);

            if (tipoEscenario == null) return NotFound();

            return View(tipoEscenario);
        }

        // POST: TipoEscenarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var tipoEscenario = await _tipoEscenarioService.GetTipoEscenarioByIdAsync(id);
            tipoEscenario.Active = false;

            try
            {
                await _tipoEscenarioService.UpdateTipoEscenariosAsync(tipoEscenario);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoEscenarioExists(tipoEscenario.Id))
                    return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TipoEscenarioExists(int id)
        {
            return _tipoEscenarioService.GetTipoEscenarioByIdAsync(id) == null ? true : false;
        }

    }
}
