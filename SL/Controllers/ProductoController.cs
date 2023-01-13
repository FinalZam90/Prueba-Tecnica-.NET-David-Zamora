using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        // GET: api/<ProductoController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            producto.Proveedor = new ML.Proveedor();
            producto.Departamento = new ML.Departamento();

            ML.Result result = BL.Producto.GetAll();
            ML.Result resultProveedores = BL.Proveedor.GetAll();
            ML.Result resultDepartamento = BL.Departamento.GetAll();

            if (result.Correct == true)
            {
                producto.Productos = result.Objects;
                producto.Proveedor.Proveedores = resultProveedores.Objects;
                producto.Departamento.Departamentos = resultDepartamento.Objects;

                return Ok(result);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        // GET api/<ProveedorController>/5
        [HttpGet("GetById/{IdProducto}")]
        public IActionResult Get(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            producto.Proveedor = new ML.Proveedor();
            producto.Departamento = new ML.Departamento();

            ML.Result result = BL.Producto.GetAll();
            ML.Result resultProveedores = BL.Proveedor.GetAll();
            ML.Result resultDepartamento = BL.Departamento.GetAll();

            if (IdProducto == null)
            {
                return Ok(producto);
            }
            else
            {
                producto.IdProducto = IdProducto.Value;
                result = BL.Producto.GetById(producto);

                if (result.Correct == true)
                {
                    producto = (ML.Producto)result.Object;
                }
                else
                {
                    return NotFound(producto);
                }

                producto.Productos = result.Objects;
                producto.Proveedor.Proveedores = resultProveedores.Objects;
                producto.Departamento.Departamentos = resultDepartamento.Objects;

                return Ok(producto);
            }
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            result = BL.Producto.Add(producto);
            if (result.Correct == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        // PUT api/<ProveedorController>/5
        [HttpPost("Update")]
        public IActionResult Update([FromBody] ML.Producto producto)
        {
            ML.Result result = new ML.Result();


            result = BL.Producto.Update(producto);
            if (result.Correct == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }


        // DELETE api/<ProveedorController>/5
        [HttpGet("Delete/{IdProducto}")]
        public IActionResult Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();
            ML.Producto producto = new ML.Producto();
            if (IdProducto == 0)
            {
                return NotFound();
            }
            else
            {
                producto.IdProducto = IdProducto;
                result = BL.Producto.Delete(producto);
                if (result.Correct == true)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }
}
