using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public DateTime FechaDeNacimiento { get; set; }

    public string? Curp { get; set; }

    public string Genero { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string NumeroDeTelefono { get; set; } = null!;

    public string? Celular { get; set; }

    public string Password { get; set; } = null!;

    public string? Imagen { get; set; }

    public int? IdRol { get; set; }
    public string NombreRol { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
