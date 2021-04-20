using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    public class AncianosController : ControllerBase
    {
        private readonly IAncianoRepository repository;
        private readonly IMapper mapper;
        public AncianosController(IAncianoRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtener todos los Ancianos
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<AncianoDTO>>> Get()
        {
            var ancianosDTO = mapper.Map<List<AncianoDTO>>(await repository.GetAll());
            return Ok(ancianosDTO);
        }

        /// <summary>
        /// Obtener un Anciano en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Anciano a obtener</param>
        [HttpGet("{id}", Name = "ObtenerAncianov2")]
        public async Task<ActionResult<AncianoDTO>> Get(int id)
        {
            var ancianoDTO = mapper.Map<AncianoDTO>(await repository.Get(id));
            if (ancianoDTO == null)
            {
                return NotFound();
            }
            return Ok(ancianoDTO);
        }

        /// <summary>
        /// Obtener un Anciano en específico mediante Correo
        /// </summary>
        /// <param name="correo">Correo del Familiar para obtener al Anciano</param>
        [HttpGet("familiar/correo/{correo}")]
        public async Task<ActionResult<List<AncianoDTO>>> GetByFamiliarCorreo(string correo)
        {
            var ancianosDTO = mapper.Map<List<AncianoDTO>>(await repository.GetByCorreo(correo));
            if (ancianosDTO == null)
            {
                return NotFound();
            }
            return Ok(ancianosDTO);
        }

        /// <summary>
        /// Registrar un Anciano
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<AncianoDTO>> Post([FromBody] AncianoCreateDTO ancianoCreate)
        {
            var anciano = mapper.Map<Anciano>(ancianoCreate);
            var ancianoDTO = mapper.Map<AncianoDTO>(await repository.Save(anciano));
            return new CreatedAtRouteResult("ObtenerAncianov2", new { id = anciano.Id }, ancianoDTO);
        }

        /// <summary>
        /// Actualizar un Anciano en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Anciano</param>
        /// <param name="ancianoCreate">Cuerpo del Anciano a actualizar</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<AncianoDTO>> Put(int id, [FromBody] AncianoCreateDTO ancianoCreate)
        {
            var anciano = mapper.Map<Anciano>(ancianoCreate);
            var ancianoRepo = await repository.Update(id, anciano);
            if (ancianoRepo == null)
            {
                return NotFound();
            }
            var ancianoDTO = mapper.Map<AncianoDTO>(ancianoRepo);
            return new CreatedAtRouteResult("ObtenerAncianov2", new { id = anciano.Id }, ancianoDTO);
        }

        /// <summary>
        /// Eliminar un Anciano en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Anciano a eliminar</param>
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