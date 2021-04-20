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
    public class RangoHorasController : ControllerBase
    {
        private readonly IRangoHoraRepository repository;
        private readonly IMapper mapper;

        public RangoHorasController(IRangoHoraRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtener todos los RangoHora
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<RangoHoraDTO>>> Get()
        {
            var rangohorasDTO = mapper.Map<List<RangoHoraDTO>>(await repository.GetAll());
            return Ok(rangohorasDTO);
        }

        /// <summary>
        /// Obtener un RangoHora en específico mediante Id
        /// </summary>
        /// <param name="id">Id del RangoHora a obtener</param>
        [HttpGet("{id}", Name = "ObtenerRangoHorav2")]
        public async Task<ActionResult<RangoHoraDTO>> Get(int id)
        {
            var rangohoraDTO = mapper.Map<RangoHoraDTO>(await repository.Get(id));
            if (rangohoraDTO == null)
            {
                return NotFound();
            }
            return Ok(rangohoraDTO);
        }

        /// <summary>
        /// Registrar un RangoHora
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<RangoHoraDTO>> Post([FromBody] RangoHoraCreateDTO rangoHoraCreate)
        {
            var rangohora = mapper.Map<RangoHora>(rangoHoraCreate);
            var rangohoraDTO = mapper.Map<RangoHoraDTO>(await repository.Save(rangohora));
            return new CreatedAtRouteResult("ObtenerRangoHorav2", new { id = rangohora.Id }, rangohoraDTO);
        }

        /// <summary>
        /// Actualizar un RangoHora en específico mediante Id
        /// </summary>
        /// <param name="id">Id del RangoHora</param>
        /// <param name="rangoHoraCreate">Cuerpo del RangoHora a actualizar</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<RangoHoraDTO>> Put(int id, [FromBody] RangoHoraCreateDTO rangoHoraCreate)
        {
            var rangohora = mapper.Map<RangoHora>(rangoHoraCreate);
            var rangohoraRepo = await repository.Update(id, rangohora);
            if (rangohoraRepo == null)
            {
                return NotFound();
            }
            var rangohoraDTO = mapper.Map<RangoHoraDTO>(rangohoraRepo);
            return new CreatedAtRouteResult("ObtenerRangoHorav2", new { id = rangohora.Id }, rangohoraDTO);
        }

        /// <summary>
        /// Eliminar un RangoHora en específico mediante Id
        /// </summary>
        /// <param name="id">Id del RangoHora a eliminar</param>
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