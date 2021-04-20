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
    public class FechaAtencionesController : ControllerBase
    {
        private readonly IFechaAtencionRepository repository;
        private readonly IMapper mapper;

        public FechaAtencionesController(IFechaAtencionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtener todos las FechaAtencion
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<FechaAtencionDTO>>> Get()
        {
            var fechaAntencionesDTO = mapper.Map<List<FechaAtencionDTO>>(await repository.GetAll());
            return Ok(fechaAntencionesDTO);
        }

        /// <summary>
        /// Obtener una FechaAtencion en específico mediante Id
        /// </summary>
        /// <param name="id">Id de la FechaAtencion a obtener</param>
        [HttpGet("{id}", Name = "ObtenerFechaAtencionv1")]
        public async Task<ActionResult<FechaAtencionDTO>> Get(int id)
        {
            var fechaAtencionDTO = mapper.Map<FechaAtencionDTO>(await repository.Get(id));
            if (fechaAtencionDTO == null)
            {
                return NotFound();
            }
            return Ok(fechaAtencionDTO);
        }

        /// <summary>
        /// Registrar una FechaAtencion
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<FechaAtencionDTO>> Post([FromBody] FechaAtencionCreateDTO fechaAtencionCreate)
        {
            var fechaAtencion = mapper.Map<FechaAtencion>(fechaAtencionCreate);
            var fechaAtencionDTO = mapper.Map<FechaAtencion>(await repository.Save(fechaAtencion));
            return new CreatedAtRouteResult("ObtenerFechaAtencionv1", new { id = fechaAtencion.Id }, fechaAtencionDTO);
        }

        /// <summary>
        /// Actualizar una FechaAtencion en específico mediante Id
        /// </summary>
        /// <param name="id">Id de la FechaAtencion</param>
        /// <param name="fechaAtencionCreate">Cuerpo de la FechaAtencion a actualizar</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<FechaAtencionDTO>> Put(int id, [FromBody] FechaAtencionCreateDTO fechaAtencionCreate)
        {
            var fechaAtencion = mapper.Map<FechaAtencion>(fechaAtencionCreate);
            var fechaAtencionRepo = await repository.Update(id, fechaAtencion);
            if (fechaAtencionRepo == null)
            {
                return NotFound();
            }
            var fechaAtencionDTO = mapper.Map<FechaAtencionDTO>(fechaAtencionRepo);
            return new CreatedAtRouteResult("ObtenerFechaAtencionv1", new { id = fechaAtencion.Id }, fechaAtencionDTO);
        }

        /// <summary>
        /// Eliminar una FechaAtencion en específico mediante Id
        /// </summary>
        /// <param name="id">Id de la FechaAtencion a eliminar</param>
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