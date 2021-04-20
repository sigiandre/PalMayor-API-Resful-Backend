using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiVP.Repository;
using ApiVP.Domain.Models;
using ApiVP.Domain.Entities;

namespace ApiVP.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolRepository repository;
        private readonly IMapper mapper;

        public RolesController(IRolRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtener todos los Roles
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<RolDTO>>> Get()
        {
            var rolesDTO = mapper.Map<List<RolDTO>>(await repository.GetAll());
            return Ok(rolesDTO);
        }

        /// <summary>
        /// Obtener un Rol en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Rol a obtener</param>
        [HttpGet("{id}", Name = "ObtenerRolv1")]
        public async Task<ActionResult<RolDTO>> Get(int id)
        {
            var rolDTO = mapper.Map<RolDTO>(await repository.Get(id));
            if (rolDTO == null)
            {
                return NotFound();
            }
            return Ok(rolDTO);
        }

        /// <summary>
        /// Registrar un Rol
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<RolDTO>> Post([FromBody] RolCreateDTO rolCreate)
        {
            var rol = mapper.Map<Rol>(rolCreate);
            var rolDTO = mapper.Map<RolDTO>(await repository.Save(rol));
            return new CreatedAtRouteResult("ObtenerRolv1", new { id = rol.Id }, rolDTO);
        }

        /// <summary>
        /// Actualizar un Rol en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Rol</param>
        /// <param name="rolCreate">Cuerpo del Rol a actualizar</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<RolDTO>> Put(int id, [FromBody] RolCreateDTO rolCreate)
        {
            var rol = mapper.Map<Rol>(rolCreate);
            var rolRepo = await repository.Update(id, rol);
            if (rolRepo == null)
            {
                return NotFound();
            }
            var rolDTO = mapper.Map<RolDTO>(rolRepo);
            return new CreatedAtRouteResult("ObtenerRolv1", new { id = rol.Id }, rolDTO);
        }

        /// <summary>
        /// Eliminar un Rol en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Rol a eliminar</param>
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
    }
}