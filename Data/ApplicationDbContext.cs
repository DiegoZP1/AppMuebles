using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppMuebles.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {   
    }
    public DbSet<AppMuebles.Models.Muebles> DataMuebles {get ;set; }
    public DbSet<AppMuebles.Models.Proforma> DataProformas {get ;set; }

    public DbSet<AppMuebles.Models.Pago> DataPago {get ;set; }
    public DbSet<AppMuebles.Models.Pedido> DataPedido {get ;set; }

    public DbSet<AppMuebles.Models.DetallePedido> DataDetallePedido {get ;set; }
}
