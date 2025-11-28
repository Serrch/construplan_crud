using System.ComponentModel.DataAnnotations;

namespace construplan_examen.Models
{
    public class OrdenDeCompra
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Numero de Orden es obligatorio")]
        [RegularExpression(@"^\d{1,9999}$", ErrorMessage = "El Numero de Orden debe contener solo números y al menos 1 digito")]
        public string NumeroDeOrden { get; set; } = string.Empty; 

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime? Fecha { get; set; }

        [Required(ErrorMessage = "El Proveedor es obligatorio")]
        public string Proveedor { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Monto Total es obligatorio")]
        [Range(1, 999999, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal MontoTotal { get; set; }
    }
}
