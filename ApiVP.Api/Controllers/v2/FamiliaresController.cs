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
    public class FamiliaresController : ControllerBase
    {
        private readonly IFamiliarRepository repository;
        private readonly IMapper mapper;

        public FamiliaresController(IFamiliarRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtener todos los Familiares
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<FamiliarDTO>>> Get()
        {
            var familiaresDTO = mapper.Map<List<FamiliarDTO>>(await repository.GetAll());
            return Ok(familiaresDTO);
        }

        /// <summary>
        /// Obtener un Familiar en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Familiar a obtener</param>
        [HttpGet("{id}", Name = "ObtenerFamiliarv2")]
        public async Task<ActionResult<FamiliarDTO>> Get(int id)
        {
            var familiarDTO = mapper.Map<FamiliarDTO>(await repository.Get(id));
            if (familiarDTO == null)
            {
                return NotFound();
            }
            return Ok(familiarDTO);
        }

        /// <summary>
        /// Obtener un Familiar en específico mediante Correo
        /// </summary>
        /// <param name="param">Correo del Familiar a obtener</param>
        [HttpGet("correo/{param}")]
        public async Task<ActionResult<List<FamiliarDTO>>> GetByCorreo(string param)
        {
            var familiarDTO = mapper.Map<FamiliarDTO>(await repository.GetByCorreo(param));
            if (familiarDTO == null)
            {
                return NotFound();
            }
            return Ok(familiarDTO);
        }

        /// <summary>
        /// Registrar un Familiar
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<FamiliarDTO>> Post([FromBody] FamiliarCreateDTO familiarCreate)
        {
            var familiar = mapper.Map<Familiar>(familiarCreate);
            var familiarDTO = mapper.Map<FamiliarDTO>(await repository.Save(familiar));
            return new CreatedAtRouteResult("ObtenerFamiliarv2", new { id = familiar.Id }, familiarDTO);
        }

        /// <summary>
        /// Actualizar un Familiar en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Familiar</param>
        /// <param name="familiarCreate">Cuerpo del Familiar a actualizar</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<FamiliarDTO>> Put(int id, [FromBody] FamiliarCreateDTO familiarCreate)
        {
            var familiar = mapper.Map<Familiar>(familiarCreate);
            var familiarRepo = await repository.Update(id, familiar);
            if (familiarRepo == null)
            {
                return NotFound();
            }
            var familiarDTO = mapper.Map<FamiliarDTO>(familiarRepo);
            return new CreatedAtRouteResult("ObtenerFamiliarv2", new { id = familiar.Id }, familiarDTO);
        }

        /// <summary>
        /// Eliminar un Familiar en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Familiar a eliminar</param>
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