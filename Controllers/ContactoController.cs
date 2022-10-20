using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AppMuebles.Models;
using AppMuebles.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;



namespace AppMuebles.Controllers
{
    public class ContactoController : Controller
    {
         private readonly ILogger<ContactoController> _logger;

        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;
        public ContactoController (ApplicationDbContext context, ILogger<ContactoController> logger ,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create (Contacto objContacto)
        {
            _context.Add(objContacto);
            _context.SaveChanges();

           
            return View("Confirmacion");
        }

        public IActionResult Confirmacion()
        {
            return View();
        }


        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DataContacto == null)
            {
                return NotFound();
            }

            var muebles = await _context.DataContacto.FindAsync(id);
            if (muebles == null)
            {
                return NotFound();
            }
            return View(muebles);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Email,Numero,Asunto,Mensaje,Status")] Contacto consulta)
        {
            if (id != consulta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactoExists(consulta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Listar));
            }
            return View(consulta);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DataContacto == null)
            {
                return NotFound();
            }

            var muebles = await _context.DataContacto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muebles == null)
            {
                return NotFound();
            }

            return View(muebles);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DataContacto == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DataMuebles'  is null.");
            }
            var muebles = await _context.DataContacto.FindAsync(id);
            if (muebles != null)
            {
                _context.DataContacto.Remove(muebles);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Listar));
        }


       


       
        public async Task<IActionResult> Listar(string? searchString,string? searchEmail,string? searchNumero ,string? searchEstado){

            var conssultas = from o in _context.DataContacto select o;
           //SELECT * FROM t_productos -> &
            if(!String.IsNullOrEmpty(searchString)){
                conssultas = conssultas.Where(s => s.Nombre.Contains(searchString)); //Algebra de bool
                // & + WHERE name like '%ABC%'
            }
            
            if(!String.IsNullOrEmpty(searchEmail)){
                conssultas = conssultas.Where(s => s.Email.Contains(searchEmail)); //Algebra de bool
                // & + WHERE name like '%ABC%'
            }
            
            if(!String.IsNullOrEmpty(searchNumero)){
                conssultas = conssultas.Where(s => s.Asunto.Contains(searchNumero)); //Algebra de bool
                // & + WHERE name like '%ABC%'
            }
            if(!String.IsNullOrEmpty(searchEstado)){
                conssultas = conssultas.Where(s => s.Status.Contains(searchEstado)); //Algebra de bool
                // & + WHERE name like '%ABC%'
            }

            return View(await conssultas.ToListAsync());


    }

    
     private bool ContactoExists(int id)
        {
          return (_context.DataContacto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
}
}
