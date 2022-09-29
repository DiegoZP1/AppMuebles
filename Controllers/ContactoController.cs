using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppMuebles.Models;
using AppMuebles.Data;


namespace AppMuebles.Controllers
{
    public class ContactoController : Controller
    {
         private readonly ILogger<ContactoController> _logger;

        private readonly ApplicationDbContext _context;

        public ContactoController (ApplicationDbContext context, ILogger<ContactoController> logger)
        {
            _context = context;
            _logger = logger;
            
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


       
        public IActionResult ListarConsultas(){
            var conssultas = _context.DataContacto.ToList();
            return View(conssultas);

        
/*
        public async Task<IActionResult> ListarConsultas()
        {
            return View(await _context.DataContacto.ToListAsync());
        }
*/
    }
}
}
