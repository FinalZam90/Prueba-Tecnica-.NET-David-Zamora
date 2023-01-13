using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuarioAdd)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaTecnicaContext context = new DL.PruebaTecnicaContext())
                {
                    var addQuery = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuarioAdd.Nombre}', '{usuarioAdd.ApellidoPaterno}', '{usuarioAdd.ApellidoMaterno}', '{DateTime.ParseExact(usuarioAdd.FechaDeNacimiento, "dd-MM-yyyy", null)}', '{usuarioAdd.CURP}', '{usuarioAdd.Genero}', '{usuarioAdd.UserName}', '{usuarioAdd.Email}', '{usuarioAdd.NumeroDeTelefono}', '{usuarioAdd.Celular}', '{usuarioAdd.Password}', '{usuarioAdd.Imagen}', {usuarioAdd.Rol.IdRol}");
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

        public static ML.Result Update(ML.Usuario usuarioUpdate)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaTecnicaContext context = new DL.PruebaTecnicaContext())
                {
                    var updateQuery = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuarioUpdate.IdUsuario}, '{usuarioUpdate.Nombre}', '{usuarioUpdate.ApellidoPaterno}', '{usuarioUpdate.ApellidoMaterno}', '{DateTime.ParseExact(usuarioUpdate.FechaDeNacimiento, "dd-MM-yyyy", null)}', '{usuarioUpdate.CURP}', '{usuarioUpdate.Genero}', '{usuarioUpdate.UserName}', '{usuarioUpdate.Email}', '{usuarioUpdate.NumeroDeTelefono}', '{usuarioUpdate.Celular}', '{usuarioUpdate.Password}', '{usuarioUpdate.Imagen}', {usuarioUpdate.Rol.IdRol}");
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

        public static ML.Result Delete(ML.Usuario usuarioDelete)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaTecnicaContext context = new DL.PruebaTecnicaContext())
                {
                    var deleteQuery = context.Database.ExecuteSqlRaw($"UsuarioDelete {usuarioDelete.IdUsuario}");
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
                    var getAllQuery = context.Usuarios.FromSqlRaw($"UsuarioGetAll").ToList();
                    if (getAllQuery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in getAllQuery)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.FechaDeNacimiento = obj.FechaDeNacimiento.ToString("dd-MM-yyyy");
                            usuario.CURP = obj.Curp;
                            usuario.Genero = obj.Genero;
                            usuario.UserName = obj.UserName;
                            usuario.Email = obj.Email;
                            usuario.NumeroDeTelefono = obj.NumeroDeTelefono;
                            usuario.Celular = obj.Celular;
                            usuario.Password = obj.Password;
                            usuario.Imagen = obj.Imagen;

                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;
                            usuario.Rol.NombreRol = obj.NombreRol;
                            result.Objects.Add(usuario);

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

        public static ML.Result GetById(ML.Usuario usuarioGetById)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaTecnicaContext context = new DL.PruebaTecnicaContext())
                {
                    var getByIdQuery = context.Usuarios.FromSqlRaw($"UsuarioGetById {usuarioGetById.IdUsuario}").AsEnumerable().FirstOrDefault();
                    if (getByIdQuery != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = getByIdQuery.IdUsuario;
                        usuario.Nombre = getByIdQuery.Nombre;
                        usuario.ApellidoPaterno = getByIdQuery.ApellidoPaterno;
                        usuario.ApellidoMaterno = getByIdQuery.ApellidoMaterno;
                        usuario.FechaDeNacimiento = getByIdQuery.FechaDeNacimiento.ToString("dd-MM-yyyy");
                        usuario.CURP = getByIdQuery.Curp;
                        usuario.Genero = getByIdQuery.Genero;
                        usuario.UserName = getByIdQuery.UserName;
                        usuario.Email = getByIdQuery.Email;
                        usuario.NumeroDeTelefono = getByIdQuery.NumeroDeTelefono;
                        usuario.Celular = getByIdQuery.Celular;
                        usuario.Password = getByIdQuery.Password;
                        usuario.Imagen = getByIdQuery.Imagen;

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = getByIdQuery.IdRol.Value;
                        usuario.Rol.NombreRol = getByIdQuery.NombreRol;

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
    }
}
