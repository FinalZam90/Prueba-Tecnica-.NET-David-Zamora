using System;
using System.Collections.Generic;

namespace DL;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public int? Stock { get; set; }

    public string? Imagen { get; set; }

    public int? IdProveedor { get; set; }
    public string NombreProveedor { get; set; }
    public int? IdDepartamento { get; set; }
    public string NombreDepartamento { get; set; }
    public string Descripcion { get; set; } = null!;

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual Proveedor? IdProveedorNavigation { get; set; }
}
