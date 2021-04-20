using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiVP.Repository;
using ApiVP.Domain.Models;
using ApiVP.Domain.Entities;


namespace ApiVP.Api.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository repository;
        private readonly IMapper mapper;

        public UsuariosController(IUsuarioRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtener todos los Usuarios
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> Get()
        {
            var usuariosDTO = mapper.Map<List<UsuarioDTO>>(await repository.GetAll());
            return Ok(usuariosDTO);
        }

        /// <summary>
        /// Obtener todos los correos de los Usuarios
        /// </summary>
        [HttpGet("correos")]
        public async Task<ActionResult<List<string>>> GetCorreos()
        {
            var usuariosCorreo = await repository.GetAllCorreos();
            return Ok(usuariosCorreo);
        }

        /// <summary>
        /// Obtener un Usuario en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Usuario a obtener</param>
        [HttpGet("{id}", Name = "ObtenerUsuariov2")]
        public async Task<ActionResult<UsuarioDTO>> Get(int id)
        {
            var usuarioDTO = mapper.Map<UsuarioDTO>(await repository.Get(id));
            if (usuarioDTO == null)
            {
                return NotFound();
            }
            return Ok(usuarioDTO);
        }

        /// <summary>
        /// Registrar un Usuario
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Post([FromBody] UsuarioCreateDTO usuarioCreate)
        {
            var usuario = mapper.Map<Usuario>(usuarioCreate);
            var usuarioDTO = mapper.Map<UsuarioDTO>(await repository.Save(usuario));
            return new CreatedAtRouteResult("ObtenerUsuariov2", new { id = usuario.Id }, usuarioDTO);
        }

        /// <summary>
        /// Actualizar un Usuario en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Usuario</param>
        /// <param name="usuarioCreate">Cuerpo del Usuario a actualizar</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDTO>> Put(int id, [FromBody] UsuarioCreateDTO usuarioCreate)
        {
            var usuario = mapper.Map<Usuario>(usuarioCreate);
            var usuarioRepo = await repository.Update(id, usuario);
            if (usuarioRepo == null)
            {
                return NotFound();
            }
            var usuarioDTO = mapper.Map<UsuarioDTO>(usuarioRepo);
            return new CreatedAtRouteResult("ObtenerUsuariov2", new { id = usuario.Id }, usuarioDTO);
        }

        /// <summary>
        /// Actualizar un campo en específico del Usuario
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult<UsuarioDTO>> Patch(int id, [FromBody] JsonPatchDocument<UsuarioCreateDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var usuario = await repository.Get(id);
            if (usuario == null)
            {
                return NotFound();
            }
            var usuarioCreate = mapper.Map<UsuarioCreateDTO>(usuario);
            patchDocument.ApplyTo(usuarioCreate, ModelState);
            mapper.Map(usuarioCreate, usuario);
            var isValid = TryValidateModel(usuario);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }
            var us = await repository.Update(id, usuario);
            return Ok();

        }

        /// <summary>
        /// Eliminar un Usuario en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Usuario a eliminar</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await repository.Delete(id);
            if (!response)
            {
                return NotFound();
            }
            return Ok();
        }

        /// <summary>
        /// Login del Usuario
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDTO>> Login([FromBody] UsuarioCreateDTO usuarioCreate)
        {
            var usuario = mapper.Map<Usuario>(usuarioCreate);
            var resp = await repository.Login(usuario);
            if (resp == null)
            {
                return BadRequest();
            }
            var usuarioDTO = mapper.Map<UsuarioDTO>(resp);
            return new CreatedAtRouteResult("ObtenerUsuariov2", new { id = usuario.Id }, usuarioDTO);
        }
    }
}