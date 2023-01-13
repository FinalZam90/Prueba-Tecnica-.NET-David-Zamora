using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Proveedor1Controller : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public Proveedor1Controller(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        
        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Proveedor proveedor = new ML.Proveedor();
            
            ML.Result result = new ML.Result();


            try
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.GetAsync("Proveedor/GetAll");


                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        result.Objects = new List<object>();
                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Proveedor resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Proveedor>(resultItem.ToString());
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
                    proveedor.Proveedores = result.Objects;
                    
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

            return View(proveedor);
        }
        [HttpGet]
        public ActionResult Form(int? IdProveedor)
        {

            ML.Result result = new ML.Result();
            ML.Proveedor proveedor = new ML.Proveedor();
            try
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.GetAsync(("Proveedor/GetById/" + IdProveedor.Value));


                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Proveedor>();
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

                    proveedor = (ML.Proveedor)result.Object;

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
            

            return View(proveedor);

        }

        [HttpPost]
        public ActionResult Form(ML.Proveedor proveedor)
        {
            ML.Result result = new ML.Result();
            IFormFile image = Request.Form.Files["ImagenData"];



            if (image != null)
            {

                byte[] ImagenBytes = ConvertToBytes(image);

                proveedor.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            if (ModelState.IsValid == true)
            {
                if (proveedor.IdProveedor == 0)
                {
                    try
                    {
                        string urlAPI = _configuration["UrlAPI"];
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(urlAPI);

                            var responseTask = client.PostAsJsonAsync<ML.Proveedor>("Proveedor/Add", proveedor);


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
                            ViewBag.Message = "El Proveedor Se ha Registrado Con Exito";
                        }
                        else
                        {
                            ViewBag.Message = "El Proveedor No Se ha Registrado Con Exito";
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

                            var responseTask = client.PostAsJsonAsync<ML.Proveedor>("Proveedor/Update", proveedor);


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
                            ViewBag.Message = "El Proveedor Se ha Actualizado Con Exito";
                        }
                        else
                        {
                            ViewBag.Message = "El Proveedor No Se ha Actualizado Con Exito";
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



                
                return View(proveedor);

            }

        }

        [HttpGet]
        public ActionResult Delete(int IdProveedor)
        {
            ML.Result result = new ML.Result();
            ML.Proveedor proveedor = new ML.Proveedor();
            if (IdProveedor == 0)
            {
                ViewBag.Message = "Ocurrió un Error Al Eliminar";
            }
            else
            {
                proveedor.IdProveedor = IdProveedor;
                result = BL.Proveedor.Delete(proveedor);
                if (result.Correct == true)
                {
                    ViewBag.Message = "El Proveedor Se ha Eliminado Con Exito";
                }
                else
                {
                    ViewBag.Message = "El Proveedor No Se ha Eliminado Con Exito";
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

