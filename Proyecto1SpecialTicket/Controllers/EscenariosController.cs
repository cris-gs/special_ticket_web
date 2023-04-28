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
using Proyecto1SpecialTicket.BLL.Services.Implementations;
using Proyecto1SpecialTicket.Areas.Identity.Data;
using Proyecto1SpecialTicket.Models;
using SpecialTicket.BLL.Services.Implementations;

namespace Proyecto1SpecialTicket.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class EscenariosController : Controller
    {
        //private readonly specialticketContext _context;
        private readonly IEscenarioService _escenarioService;
        private readonly UserManager<Proyecto1SpecialTicketUser> _userManager;

        public EscenariosController(IEscenarioService escenarioService, UserManager<Proyecto1SpecialTicketUser> userManager)
        {
            //_context = context;
            _escenarioService = escenarioService;
            _userManager = userManager;
        }

        // GET: Escenarios
        public async Task<IActionResult> Index()
        {
            return View(await _escenarioService.GetAllEscenariosAsync());
        }

        // GET: Escenarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null ) return NotFound();

            var escenario = await _escenarioService.GetEscenariosByIdAsync(id);

            if (escenario == null) return NotFound();

            return View(escenario);
        }

        // GET: Escenarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Escenarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Localizacion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active")] Escenario escenario)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                escenario.CreatedBy = userId;
                escenario.UpdatedBy = userId;
                //_context.Add(escenario);
                //await _context.SaveChangesAsync();
                await _escenarioService.CreateEscenariosAsync(escenario);
                return RedirectToAction(nameof(Index));
            }
            return View(escenario);
        }

        // GET: Escenarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var escenario = await _escenarioService.GetEscenariosByIdAsync(id);
            if (escenario == null) return NotFound();

            return View(escenario);
        }

        // POST: Escenarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Localizacion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active")] Escenario escenario)
        {
            if (id != escenario.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    //var fechaCreacion = _context.Escenarios
                    //    .Where(te => te.Id == escenario.Id)
                    //    .Select(te => te.CreatedAt)
                    //    .FirstOrDefault();
                    
                    //escenario.CreatedAt = fechaCreacion;
                    var userId = _userManager.GetUserId(User);
                    escenario.UpdatedBy = userId;
                    await _escenarioService.UpdateEscenariosAsync(escenario);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscenarioExists(escenario.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(escenario);
        }

        // GET: Escenarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var escenario = await _escenarioService.GetEscenariosByIdAsync(id);

            if (escenario == null) return NotFound();

            return View(escenario);
        }

        // POST: Escenarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var escenario = await _escenarioService.GetEscenariosByIdAsync(id);
            escenario.Active = false;

            try
            {
                await _escenarioService.UpdateEscenariosAsync(escenario);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EscenarioExists(escenario.Id))
                    return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EscenarioExists(int id)
        {
            return _escenarioService.GetEscenariosByIdAsync(id) == null ? true : false;
        }
    }
}
