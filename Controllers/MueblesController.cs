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
    public class MueblesController: Controller
    {
        private readonly ILogger<MueblesController> _logger;
        private readonly ApplicationDbContext _context;
       private readonly UserManager<IdentityUser> _userManager;
    

        public MueblesController(ApplicationDbContext context,ILogger<MueblesController> logger,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }


            [HttpGet]
         public async Task<IActionResult> Index(string? searchString ,string? Search ,string? Color,string? Categoria)
        {
            
            var muebles = from o in _context.DataMuebles select o;
            //SELECT * FROM t_productos -> &
            if(!String.IsNullOrEmpty(searchString)){
                muebles = muebles.Where(s => s.Nombre.Contains(searchString)); //Algebra de bool
                // & + WHERE name like '%ABC%'
            }

            if(!String.IsNullOrEmpty(Search)){
                muebles = muebles.Where(s => s.EstadoMue.Contains(Search)); //Algebra de bool
                // & + WHERE name like '%ABC%'
            }


            if(!String.IsNullOrEmpty(Color)){
                muebles = muebles.Where(s => s.color.Contains(Color)); //Algebra de bool
                // & + WHERE name like '%ABC%'
            }

            if(!String.IsNullOrEmpty(Categoria)){
                muebles = muebles.Where(s => s.categoria.Contains(Categoria)); //Algebra de bool
                // & + WHERE name like '%ABC%'
            }

            muebles = muebles.Where(s => s.Status.Contains("Activo"));
            
            return View(await muebles.ToListAsync());
        }


        
        


         public async Task<IActionResult> Details(int? id)
        {
            /*var productos = from o in _context.Productos select o;*/
            Muebles objProduct = await _context.DataMuebles.FindAsync(id);
            if(objProduct == null){
                return NotFound();
            }
        
            return View(objProduct);
        }

         public async Task<IActionResult> Add(int? id){
            var userID = _userManager.GetUserName(User);
            if(userID == null){
                ViewData["Message"] = "Por favor debe loguearse antes de agregar un producto";
                List<Muebles> productos = new List<Muebles>();
                return  View("Logearse",productos);
            }else{
                var producto = await _context.DataMuebles.FindAsync(id);

                
                Proforma proforma = new Proforma();
                proforma.Producto = producto;
                proforma.Precio = producto.Precio;
                proforma.Cantidad = 1;
                proforma.UserID = userID;
                _context.Add(proforma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

        }



    }
}