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
    public class OfertasController : ControllerBase
    {
        private readonly IOfertaRepository repository;
        private readonly IMapper mapper;

        public OfertasController(IOfertaRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtener todos las Ofertas
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<OfertaDTO>>> Get()
        {
            var ofertasDTO = mapper.Map<List<OfertaDTO>>(await repository.GetAll());
            return Ok(ofertasDTO);
        }

        /// <summary>
        /// Obtener una Oferta en específico mediante Id
        /// </summary>
        /// <param name="id">Id de la Oferta a obtener</param>
        [HttpGet("{id}", Name = "ObtenerOfertav2")]
        public async Task<ActionResult<OfertaDTO>> Get(int id)
        {
            var ofertaDTO = mapper.Map<OfertaDTO>(await repository.Get(id));
            if (ofertaDTO == null)
            {
                return NotFound();
            }
            return Ok(ofertaDTO);
        }

        /// <summary>
        /// Obtener una Oferta en específico mediante Correo
        /// </summary>
        /// <param name="param">Correo de la Oferta a obtener</param>
        [HttpGet("familiar/correo/{param}")]
        public async Task<ActionResult<List<OfertaDTO>>> GetByFamiliarCorreo(string param)
        {
            var ofertasDTO = mapper.Map<List<OfertaDTO>>(await repository.GetOfertasByCorreo(param));
            if (ofertasDTO == null)
            {
                return NotFound();
            }
            return Ok(ofertasDTO);
        }

        /// <summary>
        /// Registrar una Oferta
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<OfertaDTO>> Post([FromBody] OfertaCreateDTO ofertaCreate)
        {
            var oferta = mapper.Map<Oferta>(ofertaCreate);
            var ofertaDTO = mapper.Map<OfertaDTO>(await repository.Save(oferta));
            return new CreatedAtRouteResult("ObtenerOfertav2", new { id = oferta.Id }, ofertaDTO);
        }

        /// <summary>
        /// Actualizar una Oferta en específico mediante Id
        /// </summary>
        /// <param name="id">Id de la Oferta</param>
        /// <param name="ofertaCreate">Cuerpo de la Oferta a actualizar</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<OfertaDTO>> Put(int id, [FromBody] OfertaCreateDTO ofertaCreate)
        {
            var oferta = mapper.Map<Oferta>(ofertaCreate);
            var ofertaRepo = await repository.Update(id, oferta);
            if (ofertaRepo == null)
            {
                return NotFound();
            }
            var ofertaDTO = mapper.Map<OfertaDTO>(ofertaRepo);
            return new CreatedAtRouteResult("ObtenerOfertav2", new { id = oferta.Id }, ofertaDTO);
        }

        /// <summary>
        /// Eliminar una Oferta en específico mediante Id
        /// </summary>
        /// <param name="id">Id de la Oferta a eliminar</param>
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