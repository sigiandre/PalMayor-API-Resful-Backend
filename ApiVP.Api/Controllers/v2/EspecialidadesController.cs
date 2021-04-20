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
    public class EspecialidadesController : ControllerBase
    {
        private readonly IEspecialidadRepository repository;
        private readonly IMapper mapper;
        public EspecialidadesController(IEspecialidadRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtener todos las Especialidades
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<EspecialidadDTO>>> Get()
        {
            var especialidadesDTO = mapper.Map<List<EspecialidadDTO>>(await repository.GetAll());
            return Ok(especialidadesDTO);
        }

        /// <summary>
        /// Obtener una Especialidad en específico mediante Id
        /// </summary>
        /// <param name="id">Id de la Especialidad a obtener</param>
        [HttpGet("{id}", Name = "ObtenerEspecialidadv2")]
        public async Task<ActionResult<EspecialidadDTO>> Get(int id)
        {
            var especialidadDTO = mapper.Map<EspecialidadDTO>(await repository.Get(id));
            if (especialidadDTO == null)
            {
                return NotFound();
            }
            return Ok(especialidadDTO);
        }

        /// <summary>
        /// Registrar una Especialidad
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<EspecialidadDTO>> Post([FromBody] EspecialidadCreateDTO especialidadCreate)
        {
            var especialidad = mapper.Map<Especialidad>(especialidadCreate);
            var especialidadDTO = mapper.Map<EspecialidadDTO>(await repository.Save(especialidad));
            return new CreatedAtRouteResult("ObtenerEspecialidadv2", new { id = especialidad.Id }, especialidadDTO);
        }

        /// <summary>
        /// Actualizar una Especialidad en específico mediante Id
        /// </summary>
        /// <param name="id">Id de la Especialidad</param>
        /// <param name="especialidadCreate">Cuerpo de la Especialidad a actualizar</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<EspecialidadDTO>> Put(int id, [FromBody] EspecialidadCreateDTO especialidadCreate)
        {
            var especialidad = mapper.Map<Especialidad>(especialidadCreate);
            var especialidadRepo = await repository.Update(id, especialidad);
            if (especialidadRepo == null)
            {
                return NotFound();
            }
            var especialidadDTO = mapper.Map<EspecialidadDTO>(especialidadRepo);
            return new CreatedAtRouteResult("ObtenerEspecialidadv2", new { id = especialidad.Id }, especialidadDTO);
        }

        /// <summary>
        /// Eliminar una Especialidad en específico mediante Id
        /// </summary>
        /// <param name="id">Id de la Especialidad a eliminar</param>
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