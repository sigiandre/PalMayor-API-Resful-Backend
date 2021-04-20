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
    public class ServiciosController : ControllerBase
    {
        private readonly IServicioRepository repository;
        private readonly IMapper mapper;

        public ServiciosController(IServicioRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtener todos los Servicios
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<ServicioDTO>>> Get()
        {
            var serviciosDTO = mapper.Map<List<ServicioDTO>>(await repository.GetAll());
            return Ok(serviciosDTO);
        }

        /// <summary>
        /// Obtener un Servicio en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Servicio a obtener</param>
        [HttpGet("{id}", Name = "ObtenerServiciov1")]
        public async Task<ActionResult<ServicioDTO>> Get(int id)
        {
            var servicioDTO = mapper.Map<ServicioDTO>(await repository.Get(id));
            if (servicioDTO == null)
            {
                return NotFound();
            }
            return Ok(servicioDTO);
        }

        /// <summary>
        /// Obtener un Servicio en específico mediante correo del familiar
        /// </summary>
        /// <param name="correo">Correo del familiar para obtener los Servicios</param>
        [HttpGet("Familiar/correo/{correo}")]
        public async Task<ActionResult<List<ServicioDTO>>> GetByFamiliarCorreo(string correo)
        {
            var serviciosDTO = mapper.Map<List<ServicioDTO>>(await repository.GetByFamiliarCorreo(correo));
            if (serviciosDTO == null)
            {
                return NotFound();
            }

            return Ok(serviciosDTO);
        }

        /// <summary>
        /// Obtener un Servicio en específico mediante correo del enfermero
        /// </summary>
        /// <param name="correo">Correo del enfermero para obtener los Servicios</param>
        [HttpGet("Enfermero/correo/{correo}")]
        public async Task<ActionResult<List<ServicioDTO>>> GetByEnfermeroCorreo(string correo)
        {
            var serviciosDTO = mapper.Map<List<ServicioDTO>>(await repository.GetByEnfermeroCorreo(correo));
            if (serviciosDTO == null)
            {
                return NotFound();
            }

            return Ok(serviciosDTO);
        }

        /// <summary>
        /// Registrar un Servicio
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ServicioDTO>> Post([FromBody] ServicioCreateDTO servicioCreate)
        {
            var servicio = mapper.Map<Servicio>(servicioCreate);
            var servicioDTO = mapper.Map<ServicioDTO>(await repository.Save(servicio));
            return new CreatedAtRouteResult("ObtenerServiciov1", new { id = servicio.Id }, servicioDTO);
        }

        /// <summary>
        /// Actualizar un Servicio en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Servicio</param>
        /// <param name="servicioCreate">Cuerpo del Servicio a actualizar</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<ServicioDTO>> Put(int id, [FromBody] ServicioCreateDTO servicioCreate)
        {
            var servicio = mapper.Map<Servicio>(servicioCreate);
            var servicioRepo = await repository.Update(id, servicio);
            if (servicioRepo == null)
            {
                return NotFound();
            }
            var servicioDTO = mapper.Map<ServicioDTO>(servicioRepo);
            return new CreatedAtRouteResult("ObtenerServiciov1", new { id = servicio.Id }, servicioDTO);
        }

        /// <summary>
        /// Eliminar un Servicio en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Servicio a eliminar</param>
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