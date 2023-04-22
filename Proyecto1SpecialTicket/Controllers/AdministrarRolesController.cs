using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1SpecialTicket.Areas.Identity.Data;
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;

namespace Proyecto1SpecialTicket.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdministrarRolesController : Controller
    {
        private readonly specialticketContext _context;
        private readonly UserManager<Proyecto1SpecialTicketUser> _userManager;
        private readonly IServiceProvider serviceProvider;

        public AdministrarRolesController(specialticketContext context, UserManager<Proyecto1SpecialTicketUser> userManager, IServiceProvider serviceProvider)
        {
            _context = context;
            _userManager = userManager;
            this.serviceProvider = serviceProvider;
        }

        // GET: AdministrarRoles
        public IActionResult Index()
        {

            var listaUsuarios = (from u in _context.Users
                          join ur in _context.UserRoles on u.Id equals ur.UserId
                          join r in _context.Roles on ur.RoleId equals r.Id
                          select new AdministrarRoles
                          {
                              Id = u.Id,
                              UserName = u.UserName,
                              RoleId = r.Id,
                              RolName = r.Name
                          }).ToList();

            return View(listaUsuarios);
        }

        // GET: AdministrarRoles/Details/5
        public IActionResult Details(string? id)
        {
            if (id == null || _context.AdministrarRoles == null)
            {
                return NotFound();
            }

            var administrarRoles = (from u in _context.Users
                                   join ur in _context.UserRoles on u.Id equals ur.UserId
                                   join r in _context.Roles on ur.RoleId equals r.Id
                                   where u.Id == id
                                   select new AdministrarRoles
                                   {
                                       Id = u.Id,
                                       UserName = u.UserName,
                                       RoleId = r.Id,
                                       RolName = r.Name
                                   }).ToList();

            return View(administrarRoles[0]);
        }

        // GET: AdministrarRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdministrarRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,RoleId,RolName")] AdministrarRoles administrarRoles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administrarRoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(administrarRoles);
        }

        // GET: AdministrarRoles/Edit/5
        public IActionResult Edit(string? id)
        {
            if (id == null || _context.AdministrarRoles == null)
            {
                return NotFound();
            }

            var administrarRoles = (from u in _context.Users
                                    join ur in _context.UserRoles on u.Id equals ur.UserId
                                    join r in _context.Roles on ur.RoleId equals r.Id
                                    where u.Id == id
                                    select new AdministrarRoles
                                    {
                                        Id = u.Id,
                                        UserName = u.UserName,
                                        RoleId = r.Id,
                                        RolName = r.Name
                                    }).ToList();

            //ViewData["Roles"] = new SelectList(_context.Roles, "Name", "Name");

            return View(administrarRoles[0]);
        }

        //POST: AdministrarRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(string id, [Bind("Id,UserName,RoleId,RolName,Roles")] AdministrarRoles administrarRoles)
        {
            if (id != administrarRoles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Obtener el usuario de la base de datos usando su ID
                    var user = await _userManager.FindByIdAsync(id);
                    // Verificar que el usuario existe y si no existe, devolver un mensaje de error
                    if (user == null)
                    {
                        return NotFound($"No se pudo encontrar el usuario con ID '{id}'.");
                    }

                    // Quitar los roles actuales del usuario
                    var currentRole = await _userManager.GetRolesAsync(user);

                    if (currentRole != null && currentRole.Count > 0)
                    {
                        await _userManager.RemoveFromRolesAsync(user, currentRole);
                    }

                    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var roleExists = await roleManager.RoleExistsAsync(administrarRoles.Roles);
                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(administrarRoles.Roles));
                    }
                    await _userManager.AddToRoleAsync(user, administrarRoles.Roles);

                    // Guardar los cambios en la base de datos
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministrarRolesExists(administrarRoles.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(administrarRoles);
        }

        //// GET: AdministrarRoles/Delete/5
        //public async Task<IActionResult> Delete(string? id)
        //{
        //    if (id == null || _context.AdministrarRoles == null)
        //    {
        //        return NotFound();
        //    }

        //    var administrarRoles = await _context.AdministrarRoles
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (administrarRoles == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(administrarRoles);
        //}

        //// POST: AdministrarRoles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.AdministrarRoles == null)
        //    {
        //        return Problem("Entity set 'SpecialticketContext.AdministrarRoles'  is null.");
        //    }
        //    var administrarRoles = await _context.AdministrarRoles.FindAsync(id);
        //    if (administrarRoles != null)
        //    {
        //        _context.AdministrarRoles.Remove(administrarRoles);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool AdministrarRolesExists(string id)
        {
            return (_context.AdministrarRoles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
