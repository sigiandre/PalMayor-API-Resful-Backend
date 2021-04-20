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
    public class EnfermeroOfertasController : ControllerBase
    {
        private readonly IEnfermeroOfertaRepository repository;
        private readonly IMapper mapper;

        public EnfermeroOfertasController(IEnfermeroOfertaRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtener un EnfermeroOferta en específico mediante correo de Enfermero
        /// </summary>
        /// <param name="correo">Correo del Enfermero</param>
        [HttpGet("enfermero/{correo}")]
        public async Task<ActionResult<List<EnfermeroOfertaDTO>>> GetByEnfermero(string correo)
        {
            var enfermeroOfertasDTO = mapper.Map<List<EnfermeroOfertaDTO>>(await repository.GetAllByEnfermeroCorreo(correo));
            return Ok(enfermeroOfertasDTO);
        }

        /// <summary>
        /// Obtener un EnfermeroOferta en específico mediante Id de Oferta
        /// </summary>
        /// <param name="idOferta">Id de la Oferta</param>
        [HttpGet("oferta/{idOferta}")]
        public async Task<ActionResult<List<EnfermeroOfertaDTO>>> GetByOferta(int idOferta)
        {
            var enfermeroOfertasDTO = mapper.Map<List<EnfermeroOfertaDTO>>(await repository.GetAllByOferta(idOferta));
            return Ok(enfermeroOfertasDTO);
        }

        /// <summary>
        /// Obtener un EnfermeroOferta en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Enfermero a obtener</param>
        /// <param name="id2">Id de la Oferta a obtener</param>
        [HttpGet("enfermero/oferta/{id}/{id2}")]
        public async Task<ActionResult<EnfermeroOfertaDTO>> Get(int id, int id2)
        {
            var enfermeroOfertaDTO = mapper.Map<EnfermeroOfertaDTO>(await repository.GetByEnfermeroByOferta(id, id2));
            if (enfermeroOfertaDTO == null)
            {
                return NotFound();
            }
            return Ok(enfermeroOfertaDTO);
        }

        /// <summary>
        /// Registrar un EnfermeroOferta
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<EnfermeroOfertaDTO>> Post([FromBody] EnfermeroOfertaCreateDTO enfermeroOfertaCreate)
        {
            var enfermeroOferta = mapper.Map<EnfermeroOferta>(enfermeroOfertaCreate);
            var enfermeroOfertaDTO = mapper.Map<EnfermeroOfertaDTO>(await repository.Save(enfermeroOferta));
            return Ok(enfermeroOfertaDTO);
        }

    }
}