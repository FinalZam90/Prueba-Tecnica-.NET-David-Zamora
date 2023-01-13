using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Proveedor
    {
        public static ML.Result Login(ML.Usuario usuarioLogin)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaTecnicaContext context = new DL.PruebaTecnicaContext())
                {
                    var loginQuery = context.Usuarios.FromSqlRaw($"UsuarioGetByUsername '{usuarioLogin.UserName}'").AsEnumerable().First();

                    if (loginQuery != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();


                        usuario.UserName = loginQuery.UserName;
                        usuario.Password = loginQuery.Password;

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = Int32.Parse(loginQuery.IdRol.ToString());
                        usuario.Rol.NombreRol = loginQuery.NombreRol;

                        result.Object = usuario;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }
        public static ML.Result Add(ML.Proveedor proveedorAdd)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaTecnicaContext context = new DL.PruebaTecnicaContext())
                {
                    var addQuery = context.Database.ExecuteSqlRaw($"ProveedorAdd '{proveedorAdd.NombreProveedor}', '{proveedorAdd.Telefono}', '{proveedorAdd.Imagen}'");
                    if (addQuery > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Proveedor proveedorUpdate)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaTecnicaContext context = new DL.PruebaTecnicaContext())
                {
                    var updateQuery = context.Database.ExecuteSqlRaw($"ProveedorUpdate {proveedorUpdate.IdProveedor}, '{proveedorUpdate.NombreProveedor}', '{proveedorUpdate.Telefono}', '{proveedorUpdate.Imagen}'");
                    if (updateQuery > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(ML.Proveedor proveedorDelete)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaTecnicaContext context = new DL.PruebaTecnicaContext())
                {
                    var deleteQuery = context.Database.ExecuteSqlRaw($"ProveedorDelete {proveedorDelete.IdProveedor}");
                    if (deleteQuery > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaTecnicaContext context = new DL.PruebaTecnicaContext())
                {
                    var getAllQuery = context.Proveedors.FromSqlRaw($"ProveedorGetAll").ToList();
                    if (getAllQuery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in getAllQuery)
                        {
                            ML.Proveedor proveedor = new ML.Proveedor();
                            proveedor.IdProveedor = obj.IdProveedor;
                            proveedor.NombreProveedor = obj.Nombre;
                            proveedor.Telefono = obj.NumeroDeTelefono;
                            proveedor.Imagen = obj.Imagen;
                            result.Objects.Add(proveedor);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(ML.Proveedor proveedorGetById)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaTecnicaContext context = new DL.PruebaTecnicaContext())
                {
                    var getByIdQuery = context.Proveedors.FromSqlRaw($"ProveedorGetById {proveedorGetById.IdProveedor}").AsEnumerable().FirstOrDefault();
                    if (getByIdQuery != null)
                    {
                        ML.Proveedor proveedor = new ML.Proveedor();
                        proveedor.IdProveedor = getByIdQuery.IdProveedor;
                        proveedor.NombreProveedor = getByIdQuery.Nombre;
                        proveedor.Telefono = getByIdQuery.NumeroDeTelefono;
                        proveedor.Imagen = getByIdQuery.Imagen;
                        result.Object = proveedor;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
