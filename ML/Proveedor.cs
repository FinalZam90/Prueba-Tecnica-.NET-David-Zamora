﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Proveedor
    {
        public int IdProveedor 
        { 
            get; 
            set;
        }
        public string? NombreProveedor 
        { 
            get; 
            set; 
        }
        public string? Telefono 
        { 
            get;
            set; 
        }
        public string? Imagen 
        {
            get; 
            set; 
        }
        public List<object>? Proveedores 
        { 
            get; 
            set; 
        }
    }
}
