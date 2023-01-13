using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace PL.Controllers
{
    public class Usuario1Controller : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public Usuario1Controller(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ML.Login login)
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();
            try
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.PostAsJsonAsync<ML.Login>("Usuario/Login", login);


                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
                if (result.Correct == true)
                {
                    usuario = (ML.Usuario)result.Object;
                    if (usuario.Password == login.Password)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "El Usuario o Contraseña Ingresada son Incorrectos";
                        return PartialView("ModalLogin");
                    }
                }
                else
                {
                    ViewBag.Message = "El Usuario o Contraseña Ingresada son Incorrectos";
                    return PartialView("ModalLogin");
                }
            }
            catch (Exception ex)
            {

                ViewBag.Message = "Error Interno " + result.ErrorMessage;
                return PartialView("ModalLogin");
            }


        }
        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = new ML.Result();

            ML.Result resultRoles = new ML.Result();
            resultRoles = BL.Rol.GetAll();

            try
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.GetAsync("Usuario/GetAll");


                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        result.Objects = new List<object>();
                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
                if (result.Correct == true)
                {
                    usuario.Usuarios = result.Objects;
                    usuario.Rol.Roles = resultRoles.Objects;
                }
                else
                {
                    ViewBag.Message = "OCURRÓ EL SIGUIENTE ERROR " + result.ErrorMessage;
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return View(usuario);
        }
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {

            ML.Result result = new ML.Result();
            ML.Result resultRoles = new ML.Result();
            

            ML.Usuario usuario = new ML.Usuario();
            
            usuario.Rol = new ML.Rol();
            
            resultRoles = BL.Rol.GetAll();

            try
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.GetAsync(("Usuario/GetById/" + IdUsuario.Value));


                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Usuario>();
                        readTask.Wait();
                        result.Object = readTask.Result;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

                if (result.Correct == true)
                {

                    usuario = (ML.Usuario)result.Object;

                }
                else
                {
                    ViewBag.Message = "Ocurrio el siguiente error: " + result.ErrorMessage;
                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }
            usuario.Rol.Roles = resultRoles.Objects;
            
            return View(usuario);

        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            IFormFile image = Request.Form.Files["ImagenData"];



            if (image != null)
            {

                byte[] ImagenBytes = ConvertToBytes(image);

                usuario.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            if (ModelState.IsValid == true)
            {
                if (usuario.IdUsuario == null)
                {
                    usuario.IdUsuario = 0;
                    try
                    {
                        string urlAPI = _configuration["UrlAPI"];
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(urlAPI);

                            var responseTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Add", usuario);


                            responseTask.Wait();

                            var resultServicio = responseTask.Result;

                            if (resultServicio.IsSuccessStatusCode)
                            {

                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                            }
                        }
                        if (result.Correct == true)
                        {
                            ViewBag.Message = "El Usuario Se ha Registrado Con Exito";
                        }
                        else
                        {
                            ViewBag.Message = "El Usuario No Se ha Registrado Con Exito";
                        }

                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                    }

                }
                else
                {
                    try
                    {
                        string urlAPI = _configuration["UrlAPI"];
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(urlAPI);

                            var responseTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Update", usuario);


                            responseTask.Wait();

                            var resultServicio = responseTask.Result;

                            if (resultServicio.IsSuccessStatusCode)
                            {

                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                            }
                        }
                        if (result.Correct == true)
                        {
                            ViewBag.Message = "El Usuario Se ha Actualizado Con Exito";
                        }
                        else
                        {
                            ViewBag.Message = "El Usuario No Se ha Actualizado Con Exito";
                        }

                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                    }

                }
                return PartialView("Modal");
            }
            else
            {
                ML.Result resultRoles = new ML.Result();
               


                usuario.Rol = new ML.Rol();
                resultRoles = BL.Rol.GetAll();
                usuario.Rol.Roles = resultRoles.Objects;
                return View(usuario);

            }

        }

        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();
            if (IdUsuario == 0)
            {
                ViewBag.Message = "Ocurrió un Error Al Eliminar";
            }
            else
            {
                usuario.IdUsuario = IdUsuario;
                result = BL.Usuario.Delete(usuario);
                if (result.Correct == true)
                {
                    ViewBag.Message = "El Usuario Se ha Eliminado Con Exito";
                }
                else
                {
                    ViewBag.Message = "El Usuario No Se ha Eliminado Con Exito";
                }
            }
            return PartialView("Modal");
        }
        public static byte[] ConvertToBytes(IFormFile Imagen)
        {
            using var fileStream = Imagen.OpenReadStream();

            byte[] datos = new byte[fileStream.Length];
            fileStream.Read(datos, 0, (int)fileStream.Length);

            return datos;
        }
    }
}
