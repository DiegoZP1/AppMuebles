using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppMuebles.Data;
using AppMuebles.Models;

namespace AppMuebles.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
              return _context.DataMuebles != null ? 
                          View(await _context.DataMuebles.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DataMuebles'  is null.");
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DataMuebles == null)
            {
                return NotFound();
            }

            var muebles = await _context.DataMuebles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muebles == null)
            {
                return NotFound();
            }

            return View(muebles);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,EstadoMue,Descripcion,Precio,categoria,marca,color,PorcentajeDesc,Imagen,Status")] Muebles muebles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(muebles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(muebles);
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DataMuebles == null)
            {
                return NotFound();
            }

            var muebles = await _context.DataMuebles.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,EstadoMue,Descripcion,Precio,categoria,marca,color,PorcentajeDesc,Imagen,Status")] Muebles muebles)
        {
            if (id != muebles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(muebles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MueblesExists(muebles.Id))
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
            return View(muebles);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DataMuebles == null)
            {
                return NotFound();
            }

            var muebles = await _context.DataMuebles
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
            if (_context.DataMuebles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DataMuebles'  is null.");
            }
            var muebles = await _context.DataMuebles.FindAsync(id);
            if (muebles != null)
            {
                _context.DataMuebles.Remove(muebles);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MueblesExists(int id)
        {
          return (_context.DataMuebles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
