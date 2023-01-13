namespace ML
{
    public class Usuario
    {
        public int? IdUsuario
        {
            get;
            set;
        }


        public string Nombre
        {
            get;
            set;
        }
        public string ApellidoPaterno
        {
            get;
            set;
        }
        public string? ApellidoMaterno
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string? CURP
        {
            get;
            set;
        }
        public string Genero
        {
            get;
            set;
        }
        public string FechaDeNacimiento
        {
            get;
            set;
        }
        public string NumeroDeTelefono
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string? Celular
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string? Imagen
        {
            get;
            set;
        }
        public string? NombreCompleto
        {
            get;
            set;
        }
        public bool Status
        {
            get;
            set;
        }
        public List<object>? Usuarios
        {
            get;
            set;
        }
        public ML.Rol? Rol
        {
            get;
            set;
        }
    }
}