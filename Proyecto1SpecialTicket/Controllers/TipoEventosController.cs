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
    public class TipoEventosController : Controller
    {
        //private readonly specialticketContext _context;
        private readonly ITipoEventoService _tipoEventoService;
        private readonly UserManager<Proyecto1SpecialTicketUser> _userManager;

        public TipoEventosController(ITipoEventoService tipoEventoService, UserManager<Proyecto1SpecialTicketUser> userManager)
        {
            //_context = context;
            _tipoEventoService = tipoEventoService;
            _userManager = userManager;
        }

        // GET: TipoEventos
        public async Task<IActionResult> Index()
        {
            return View(await _tipoEventoService.GetAllTipoEventosAsync());
        }

        // GET: TipoEventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tipoEvento = await _tipoEventoService.GetTipoEventoByIdAsync(id);

            if (tipoEvento == null) return NotFound();

            return View(tipoEvento);
        }

        // GET: TipoEventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoEventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active")] TipoEvento tipoEvento)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                tipoEvento.CreatedBy = userId;
                tipoEvento.UpdatedBy = userId;
                //_context.Add(tipoEvento);
                //await _context.SaveChangesAsync();
                await _tipoEventoService.CreateTipoEventoAsync(tipoEvento);
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEvento);
        }

        // GET: TipoEventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tipoEvento = await _tipoEventoService.GetTipoEventoByIdAsync(id);

            if (tipoEvento == null) return NotFound();

            return View(tipoEvento);
        }

        // POST: TipoEventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active")] TipoEvento tipoEvento)
        {
            if (id != tipoEvento.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    //var fechaCreacion = _context.TipoEventos
                    //    .Where(te => te.Id == tipoEvento.Id)
                    //    .Select(te => te.CreatedAt)
                    //    .FirstOrDefault();
                    //tipoEvento.CreatedAt = fechaCreacion;
                    var userId = _userManager.GetUserId(User);
                    tipoEvento.UpdatedBy = userId;
                    await _tipoEventoService.UpdateTipoEventoAsync(tipoEvento);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoEventoExists(tipoEvento.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEvento);
        }

        // GET: TipoEventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tipoEvento = await _tipoEventoService.GetTipoEventoByIdAsync(id);

            if (tipoEvento == null) return NotFound();

            return View(tipoEvento);
        }

        // POST: TipoEventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoEvento = await _tipoEventoService.GetTipoEventoByIdAsync(id);
            tipoEvento.Active = false;

            try
            {
                await _tipoEventoService.UpdateTipoEventoAsync(tipoEvento);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoEventoExists(tipoEvento.Id))
                    return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TipoEventoExists(int id)
        {
          return _tipoEventoService.GetTipoEventoByIdAsync(id) == null ? true : false;
        }
    }
}
