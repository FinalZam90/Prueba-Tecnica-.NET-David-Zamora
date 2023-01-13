using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        // GET: api/<ProveedorController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Proveedor proveedor = new ML.Proveedor();
            
            ML.Result result = BL.Proveedor.GetAll();
            
            if (result.Correct == true)
            {
                proveedor.Proveedores = result.Objects;
               
                return Ok(result);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        // GET api/<ProveedorController>/5
        [HttpGet("GetById/{IdProveedor}")]
        public IActionResult Get(int? IdProveedor)
        {
            ML.Proveedor proveedor = new ML.Proveedor();
            ML.Result result = new ML.Result();


            if (IdProveedor == null)
            {
                return Ok(proveedor);
            }
            else
            {
                proveedor.IdProveedor = IdProveedor.Value;
                result = BL.Proveedor.GetById(proveedor);

                if (result.Correct == true)
                {
                    proveedor = (ML.Proveedor)result.Object;
                }
                else
                {
                    return NotFound(proveedor);
                }


                return Ok(proveedor);
            }
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Proveedor proveedor)
        {
            ML.Result result = new ML.Result();

            result = BL.Proveedor.Add(proveedor);
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
        public IActionResult Update([FromBody] ML.Proveedor proveedor)
        {
            ML.Result result = new ML.Result();


            result = BL.Proveedor.Update(proveedor);
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
        [HttpDelete("Delete/{IdProveedor}")]
        public IActionResult Delete(int IdProveedor)
        {
            ML.Result result = new ML.Result();
            ML.Proveedor proveedor = new ML.Proveedor();
            if (IdProveedor == 0)
            {
                return NotFound();
            }
            else
            {
                proveedor.IdProveedor = IdProveedor;
                result = BL.Proveedor.Delete(proveedor);
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
