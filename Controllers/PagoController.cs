using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AppMuebles.Data;
using AppMuebles.Models;
using Microsoft.EntityFrameworkCore;



namespace AppMuebles.Controllers
{
    public class PagoController:Controller
    {
        
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PagoController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

       public IActionResult Create(Decimal monto)
        {
            Pago pago = new Pago();
            pago.UserID = _userManager.GetUserName(User);
            pago.MontoTotal = monto;
            return View(pago);
        } 


         [HttpPost]
        public IActionResult Pagar(Pago pago)
        {
            pago.PaymentDate = DateTime.UtcNow;
            _context.Add(pago);

            var itemsProforma = from o in _context.DataProformas select o;
            itemsProforma = itemsProforma.
                Include(p => p.Producto).
                Where(s => s.UserID.Equals(pago.UserID) && s.Status.Equals("Pendiente"));

            Pedido pedido = new Pedido();
            pedido.UserID = pago.UserID;
            pedido.Total = pago.MontoTotal;
            pedido.pago = pago;
            pedido.Status = "Pendiente";
            _context.Add(pedido);


            List<DetallePedido> itemsPedido = new List<DetallePedido>();
            foreach(var item in itemsProforma.ToList()){
                DetallePedido detallePedido = new DetallePedido();
                detallePedido.pedido=pedido;
                detallePedido.Precio = item.Precio;
                detallePedido.Producto = item.Producto;
                detallePedido.Cantidad = item.Cantidad;
                itemsPedido.Add(detallePedido);
            }

            _context.AddRange(itemsPedido);

            foreach (Proforma p in itemsProforma.ToList())
            {
                p.Status="PROCESADO";
            }
            _context.UpdateRange(itemsProforma);

            _context.SaveChanges();

            ViewData["Message"] = "El pago se ha registrado";
            return View("Confirmacion");
        }  
          public async Task<IActionResult> Reportes()
        {
        var userID = _userManager.GetUserName(User);
        var applicationDbContext = _context.DataProformas.Include(c => c.Producto);            
        return View(await applicationDbContext.ToListAsync());  

          }


         public async Task<IActionResult> Confirmacion(int? id)
        {
            /*var productos = from o in _context.Productos select o;*/
            Proforma objProduct = await _context.DataProformas.FindAsync(id);
            if(objProduct == null){
                return NotFound();
            }
        
            return View(objProduct);
        }




        public IActionResult Index()
        {
            
            return View(_context.DataPago.ToList());
        }

      

    }
}






