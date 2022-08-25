using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppMuebles.Models;
using AppMuebles.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppMuebles.Controllers
{
    public class MueblesController: Controller
    {
        private readonly ILogger<MueblesController> _logger;
        private readonly ApplicationDbContext _context;
    

        public MueblesController(ApplicationDbContext context,ILogger<MueblesController> logger)
        {
            _context = context;
            _logger = logger;
        
        }

         public async Task<IActionResult> Index(string? searchString)
        {
            
            var muebles = from o in _context.DataMuebles select o;
            //SELECT * FROM t_productos -> &
            if(!String.IsNullOrEmpty(searchString)){
                muebles = muebles.Where(s => s.Nombre.Contains(searchString)); //Algebra de bool
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


    }
}