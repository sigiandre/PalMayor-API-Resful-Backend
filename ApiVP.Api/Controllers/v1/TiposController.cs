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
    public class TiposController : ControllerBase
    {
        private readonly ITipoRepository repository;
        private readonly IMapper mapper;

        public TiposController(ITipoRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtener todos los Tipos
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<TipoDTO>>> Get()
        {
            var tiposDTO = mapper.Map<List<TipoDTO>>(await repository.GetAll());
            return Ok(tiposDTO);
        }


        /// <summary>
        /// Obtener un Tipo en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Tipo a obtener</param>
        [HttpGet("{id}", Name = "ObtenerTipov1")]
        public async Task<ActionResult<TipoDTO>> Get(int id)
        {
            var tipoDTO = mapper.Map<TipoDTO>(await repository.Get(id));
            if (tipoDTO == null)
            {
                return NotFound();
            }
            return Ok(tipoDTO);
        }

        /// <summary>
        /// Registrar un Tipo
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TipoDTO>> Post([FromBody] TipoCreateDTO tipoCreate)
        {
            var tipo = mapper.Map<Tipo>(tipoCreate);
            var tipoDTO = mapper.Map<TipoDTO>(await repository.Save(tipo));
            return new CreatedAtRouteResult("ObtenerTipov1", new { id = tipo.Id }, tipoDTO);
        }

        /// <summary>
        /// Actualizar un Tipo en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Tipo</param>
        /// <param name="tipoCreate">Cuerpo del Tipo a actualizar</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<TipoDTO>> Put(int id, [FromBody] TipoCreateDTO tipoCreate)
        {
            var tipo = mapper.Map<Tipo>(tipoCreate);
            var tipoRepo = await repository.Update(id, tipo);
            if (tipoRepo == null)
            {
                return NotFound();
            }
            var tipoDTO = mapper.Map<TipoDTO>(tipoRepo);
            return new CreatedAtRouteResult("ObtenerTipov1", new { id = tipo.Id }, tipoDTO);
        }

        /// <summary>
        /// Eliminar un Tipo en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Tipo a eliminar</param>
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