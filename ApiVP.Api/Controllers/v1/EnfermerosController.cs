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
    public class EnfermerosController : ControllerBase
    {
        private readonly IEnfermeroRepository repository;
        private readonly IMapper mapper;

        public EnfermerosController(IEnfermeroRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtener todos los Enfermeros
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<EnfermeroDTO>>> Get()
        {
            var enfermerosDTO = mapper.Map<List<EnfermeroDTO>>(await repository.GetAll());
            return Ok(enfermerosDTO);
        }

        /// <summary>
        /// Obtener un Enfermero en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Enfermero a obtener</param>
        [HttpGet("{id}", Name = "ObtenerEnfermerov1")]
        public async Task<ActionResult<EnfermeroDTO>> Get(int id)
        {
            var enfermeroDTO = mapper.Map<EnfermeroDTO>(await repository.Get(id));
            if (enfermeroDTO == null)
            {
                return NotFound();
            }
            return Ok(enfermeroDTO);
        }

        /// <summary>
        /// Obtener un Enfermero en específico mediante su correo
        /// </summary>
        /// <param name="correo">Correo del Enfermero a obtener</param>
        [HttpGet("correo/{correo}")]
        public async Task<ActionResult<EnfermeroDTO>> GetByCorreo(string correo)
        {
            var enfermeroDTO = mapper.Map<EnfermeroDTO>(await repository.GetEnfermeroByCorreo(correo));
            if (enfermeroDTO == null)
            {
                return NotFound();
            }
            return Ok(enfermeroDTO);
        }

        /// <summary>
        /// Registrar un Enfermero
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<EnfermeroDTO>> Post([FromBody] EnfermeroCreateDTO enfermeroCreate)
        {
            var enfermero = mapper.Map<Enfermero>(enfermeroCreate);
            var enfermeroDTO = mapper.Map<EnfermeroDTO>(await repository.Save(enfermero));
            return new CreatedAtRouteResult("ObtenerEnfermerov1", new { id = enfermero.Id }, enfermeroDTO);
        }

        /// <summary>
        /// Actualizar un Enfermero en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Enfermero</param>
        /// <param name="enfermeroCreate">Cuerpo del Anciano a actualizar</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<EnfermeroDTO>> Put(int id, [FromBody] EnfermeroCreateDTO enfermeroCreate)
        {
            var enfermero = mapper.Map<Enfermero>(enfermeroCreate);
            var enfermeroRepo = await repository.Update(id, enfermero);
            if (enfermeroRepo == null)
            {
                return NotFound();
            }
            var enfermeroDTO = mapper.Map<EnfermeroDTO>(enfermeroRepo);
            return new CreatedAtRouteResult("ObtenerEnfermerov1", new { id = enfermero.Id }, enfermeroDTO);
        }

        /// <summary>
        /// Eliminar un Enfermero en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Enfermero a eliminar</param>
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