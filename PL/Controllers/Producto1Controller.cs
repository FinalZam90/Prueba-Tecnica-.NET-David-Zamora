using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Producto1Controller : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public Producto1Controller(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Producto producto = new ML.Producto();
            producto.Departamento = new ML.Departamento();
            producto.Proveedor = new ML.Proveedor();
            ML.Result result = new ML.Result();

            ML.Result resultDepartamento = new ML.Result();
            ML.Result resultProveedor= new ML.Result();

            resultDepartamento = BL.Departamento.GetAll();
            resultProveedor = BL.Proveedor.GetAll();

            try
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.GetAsync("Producto/GetAll");


                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        result.Objects = new List<object>();
                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Producto resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(resultItem.ToString());
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
                    producto.Productos = result.Objects;
                    producto.Proveedor.Proveedores = resultProveedor.Objects;
                    producto.Departamento.Departamentos = resultDepartamento.Objects;
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

            return View(producto);
        }
        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {

            ML.Producto producto = new ML.Producto();
            producto.Departamento = new ML.Departamento();
            producto.Proveedor = new ML.Proveedor();
            ML.Result result = new ML.Result();

            ML.Result resultDepartamento = new ML.Result();
            ML.Result resultProveedor = new ML.Result();

            resultDepartamento = BL.Departamento.GetAll();
            resultProveedor = BL.Proveedor.GetAll();

            try
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.GetAsync(("Producto/GetById/" + IdProducto.Value));


                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Producto>();
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

                    producto = (ML.Producto)result.Object;

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
            producto.Productos = result.Objects;
            producto.Proveedor.Proveedores = resultProveedor.Objects;
            producto.Departamento.Departamentos = resultDepartamento.Objects;
            return View(producto);

        }

        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            IFormFile image = Request.Form.Files["ImagenData"];



            if (image != null)
            {

                byte[] ImagenBytes = ConvertToBytes(image);

                producto.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            if (ModelState.IsValid == true)
            {
                if (producto.IdProducto == 0)
                {
                    try
                    {
                        string urlAPI = _configuration["UrlAPI"];
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(urlAPI);

                            var responseTask = client.PostAsJsonAsync<ML.Producto>("Producto/Add", producto);


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
                            ViewBag.Message = "El Producto Se ha Registrado Con Exito";
                        }
                        else
                        {
                            ViewBag.Message = "El Producto No Se ha Registrado Con Exito";
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

                            var responseTask = client.PostAsJsonAsync<ML.Producto>("Producto/Update", producto);


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
                            ViewBag.Message = "El Producto Se ha Actualizado Con Exito";
                        }
                        else
                        {
                            ViewBag.Message = "El Producto No Se ha Actualizado Con Exito";
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
                ML.Result resultDepartamento = new ML.Result();
                ML.Result resultProveedor = new ML.Result();

                producto.Productos = result.Objects;
                producto.Proveedor.Proveedores = resultProveedor.Objects;
                producto.Departamento.Departamentos = resultDepartamento.Objects;
                return View(producto);

            }

        }

        [HttpGet]
        public ActionResult Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();
            ML.Producto producto = new ML.Producto();
            if (IdProducto == 0)
            {
                ViewBag.Message = "Ocurrió un Error Al Eliminar";
            }
            else
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.GetAsync(("Producto/Delete/" + IdProducto));


                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Producto>();
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

                    ViewBag.Message = "El Producto Se ha Eliminado Con Exito";

                }
                else
                {
                    ViewBag.Message = "El Producto No Se ha Eliminado Con Exito";
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
