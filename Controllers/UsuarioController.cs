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

    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;

        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;
        public UsuarioController(ApplicationDbContext context, ILogger<UsuarioController> logger ,UserManager<IdentityUser> userManager)
        {
           _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        

        public async Task<IActionResult> Index()
        {
        var applicationDbContext = _context.DataProformas.Include(c => c.Producto);            
        return View(await applicationDbContext.ToListAsync());
        }


       
    }
}