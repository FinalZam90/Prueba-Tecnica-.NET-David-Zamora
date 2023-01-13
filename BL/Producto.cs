using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BL
{
    public class Producto
    {
        public static ML.Result Add(ML.Producto productoAdd)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaTecnicaContext context = new DL.PruebaTecnicaContext())
                {
                    var addQuery = context.Database.ExecuteSqlRaw($"ProductoAdd '{productoAdd.NombreProducto}', {productoAdd.PrecioUnitario}, {productoAdd.Stock}, '{productoAdd.Imagen}', {productoAdd.Proveedor.IdProveedor}, {productoAdd.Departamento.IdDepartamento}, '{productoAdd.Descripcion}'");
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

        public static ML.Result Update(ML.Producto productoUpdate)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaTecnicaContext context = new DL.PruebaTecnicaContext())
                {
                    var updateQuery = context.Database.ExecuteSqlRaw($"ProductoUpdate {productoUpdate.IdProducto}, '{productoUpdate.NombreProducto}', {productoUpdate.PrecioUnitario}, {productoUpdate.Stock}, '{productoUpdate.Imagen}', {productoUpdate.Proveedor.IdProveedor}, {productoUpdate.Departamento.IdDepartamento}, '{productoUpdate.Descripcion}'");
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

        public static ML.Result Delete(ML.Producto productoDelete)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaTecnicaContext context = new DL.PruebaTecnicaContext())
                {
                    var deleteQuery = context.Database.ExecuteSqlRaw($"ProductoDelete {productoDelete.IdProducto}");
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
                    var getAllQuery = context.Productos.FromSqlRaw($"ProductoGetAll").ToList();
                    if (getAllQuery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in getAllQuery)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = obj.IdProducto;
                            producto.NombreProducto = obj.Nombre;
                            producto.PrecioUnitario = obj.Precio;
                            producto.Stock = obj.Stock.Value;
                            producto.Imagen = obj.Imagen;

                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.IdProveedor = obj.IdProveedor.Value;
                            producto.Proveedor.NombreProveedor = obj.NombreProveedor;

                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = obj.IdDepartamento.Value;
                            producto.Departamento.NombreDepartamento = obj.NombreDepartamento;
                            producto.Descripcion = obj.Descripcion;
                            result.Objects.Add(producto);

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
        
        public static ML.Result GetById(ML.Producto productoGetById)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaTecnicaContext context = new DL.PruebaTecnicaContext())
                {
                    var getByIdQuery = context.Productos.FromSqlRaw($"ProductoGetById {productoGetById.IdProducto}").AsEnumerable().FirstOrDefault();
                    if (getByIdQuery != null)
                    {
                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = getByIdQuery.IdProducto;
                        producto.NombreProducto = getByIdQuery.Nombre;
                        producto.PrecioUnitario = getByIdQuery.Precio;
                        producto.Stock = getByIdQuery.Stock.Value;
                        producto.Imagen = getByIdQuery.Imagen;

                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = getByIdQuery.IdProveedor.Value;
                        producto.Proveedor.NombreProveedor = getByIdQuery.NombreProveedor;

                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = getByIdQuery.IdDepartamento.Value;
                        producto.Departamento.NombreDepartamento = getByIdQuery.NombreDepartamento;
                        producto.Descripcion = getByIdQuery.Descripcion;

                        result.Object = producto;
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
