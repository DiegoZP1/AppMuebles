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
using Newtonsoft.Json;

namespace AppMuebles.Controllers
{
    public class MueblesController: Controller
    {
        private readonly ILogger<MueblesController> _logger;
        private readonly ApplicationDbContext _context;
       private readonly UserManager<IdentityUser> _userManager;
       public string baseUrl = "https://filtrobusquedamuebles.azurewebsites.net/api/";
    

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


         [HttpPost]
        public async Task<IActionResult> Index(IFormFile busqueda)
        {
            var oLista = new List<Muebles>();
            var byte_imagen = new byte[10000];
            try
            {
                if (busqueda.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        busqueda.CopyTo(ms);
                        byte_imagen = ms.ToArray();
                    }
                }
            }
            catch
            {
                ViewBag.Message = "No se ha subido el archivo correctamente";
                return View(oLista);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    var fileStreamContent = new StreamContent(new MemoryStream(byte_imagen));
                    multipartFormContent.Add(fileStreamContent, name: "busqueda", fileName: busqueda.FileName);
                    HttpResponseMessage getData = await client.PostAsync("SearchAllMueblesImage?code=4PjBjTx9i7m907p2s6l9gdxXs2YV1T0g_5NShN35cHgqAzFuH6vMeA==", multipartFormContent);
                    if (getData.IsSuccessStatusCode)
                    {
                        var jsonString = getData.Content.ReadAsStringAsync();
                        jsonString.Wait();
                        oLista = JsonConvert.DeserializeObject<List<Muebles>>(jsonString.Result);
                    }
                    else
                    {
                        Console.WriteLine("Error en la API");
                    }
                }
            }
            return View(oLista);
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