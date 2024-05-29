using System.ComponentModel.DataAnnotations.Schema;

namespace EntradasBackEnd
{
    public class GastosView
    {
        public int id { get; set; }
        public int idPersona { get; set; }
        public string? nombre { get; set; }
        public string? apellidos { get; set; }
        public DateTime? fecha { get; set; }
        public string? descripcion { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal importe { get; set; }        
    }
}
