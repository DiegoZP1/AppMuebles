using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppMuebles.Models;
using AppMuebles.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Identity;
using System.Dynamic;


namespace AppMuebles.Controllers
{
    //CARRITO
    public class ProformasController: Controller
    {
        private readonly ILogger<MueblesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager; 

        public ProformasController(ApplicationDbContext context,
            ILogger<MueblesController> logger,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;

        }
        public async Task<IActionResult> Index(){
           /* var producto  = Util.SessionExtensions.Get<Producto>(HttpContext.Session,"Producto");*/


            var  userID = _userManager.GetUserName(User);
            var items = from o in _context.DataProformas select o;
            items = items.Include(p => p.Producto).Where(w => w.UserID.Equals(userID) && w.Status.Equals("Pendiente"));

            var carrito = await items.ToListAsync();
            var total= carrito.Sum(c => c.Cantidad * c.Precio);

            dynamic model = new ExpandoObject();
            model.montoTotal = total;
            model.elementosCarrito = carrito;

             return View(model);
        }


//ELIMINAR

            public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.DataProformas.FirstOrDefaultAsync(m => m.id == id);
            if (produto == null)
            {
                return NotFound();
            }
            

            return View(produto);
        }

 // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.DataProformas.FindAsync(id);
            _context.DataProformas.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


//EDITAR

     // GET: Proforma/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proforma = await _context.DataProformas.FindAsync(id);
            if (proforma == null)
            {
                return NotFound();
            }
            return View(proforma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cantidad,Precio,UserID")] Proforma proforma)
        {
            if (id != proforma.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proforma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProformaExists(proforma.id))
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
            return View(proforma);
        }







        private bool ProformaExists(int id)
        {
            return _context.DataProformas.Any(e => e.id == id);
        }
    }
}