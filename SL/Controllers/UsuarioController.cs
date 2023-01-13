using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetAll();
            ML.Result resultRoles = new ML.Result();
            resultRoles = BL.Rol.GetAll();
            if (result.Correct == true)
            {
                usuario.Usuarios = result.Objects;
                usuario.Rol.Roles = resultRoles.Objects;
                return Ok(result);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("GetById/{IdUsuario}")]
        public IActionResult Get(int? IdUsuario)
        {
            ML.Result resultRoles = new ML.Result();
           
            ML.Usuario usuario = new ML.Usuario();
            
            usuario.Rol = new ML.Rol();
            resultRoles = BL.Rol.GetAll();
            

            ML.Result result = new ML.Result();
            

            if (IdUsuario == null)
            {
               
                usuario.Rol.Roles = resultRoles.Objects;
                
                return Ok(usuario);
            }
            else
            {
                usuario.IdUsuario = IdUsuario.Value;
                result = BL.Usuario.GetById(usuario);

                if (result.Correct == true)
                {
                    usuario = (ML.Usuario)result.Object;

                    

                }
                else
                {
                    return NotFound(usuario);
                }

                usuario.Rol.Roles = resultRoles.Objects;
                
                return Ok(usuario);
            }
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            result = BL.Usuario.Add(usuario);
            if (result.Correct == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] ML.Login login)
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();
            usuario.UserName = login.UserName;
            usuario.Password = login.Password;
            result = BL.Usuario.Login(usuario);
            if (result.Correct == true)
            {

                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("Update")]
        public IActionResult Update([FromBody] ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();


            result = BL.Usuario.Update(usuario);
            if (result.Correct == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("Delete/{IdUsuario}")]
        public IActionResult Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();
            if (IdUsuario == 0)
            {
                return NotFound();
            }
            else
            {
                usuario.IdUsuario = IdUsuario;
                result = BL.Usuario.Delete(usuario);
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
