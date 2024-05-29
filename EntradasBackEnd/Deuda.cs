using System.ComponentModel.DataAnnotations.Schema;

namespace EntradasBackEnd
{
    public class Deuda
    {
        public string? nombreDeudor { get; set; }
        public string? nombreAcreedor { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal importe { get; set; }

        public Deuda(string? nombreDeudor, string? nombreAcreedor, decimal importe)
        {
            this.nombreDeudor = nombreDeudor;
            this.nombreAcreedor = nombreAcreedor;
            this.importe = importe;
        }       
    }
}
