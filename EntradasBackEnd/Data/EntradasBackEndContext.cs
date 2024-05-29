using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EntradasBackEnd;

namespace EntradasBackEnd.Data
{
    public class EntradasBackEndContext : DbContext
    {
        public EntradasBackEndContext (DbContextOptions<EntradasBackEndContext> options)
            : base(options)
        {
        }

        public DbSet<EntradasBackEnd.Personas> Personas { get; set; } = default!;        
        public DbSet<EntradasBackEnd.BalanceView> BalanceView { get; set; } = default!;
        public DbSet<EntradasBackEnd.Gastos> Gastos { get; set; } = default!;
        public DbSet<EntradasBackEnd.GastosView> GastosView { get; set; } = default!;
    }
}
