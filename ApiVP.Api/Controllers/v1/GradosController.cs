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
    public class GradosController : ControllerBase
    {
        private readonly IGradoRepository repository;
        private readonly IMapper mapper;

        public GradosController(IGradoRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtener todos los Grados
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<GradoDTO>>> Get()
        {
            var gradosDTO = mapper.Map<List<GradoDTO>>(await repository.GetAll());
            return Ok(gradosDTO);
        }

        /// <summary>
        /// Obtener un Grado en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Grado a obtener</param>
        [HttpGet("{id}", Name = "ObtenerGradov1")]
        public async Task<ActionResult<GradoDTO>> Get(int id)
        {
            var gradoDTO = mapper.Map<GradoDTO>(await repository.Get(id));
            if (gradoDTO == null)
            {
                return NotFound();
            }
            return Ok(gradoDTO);
        }

        /// <summary>
        /// Registrar un Grado
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<GradoDTO>> Post([FromBody] GradoCreateDTO gradoCreate)
        {
            var grado = mapper.Map<Grado>(gradoCreate);
            var gradoDTO = mapper.Map<GradoDTO>(await repository.Save(grado));
            return new CreatedAtRouteResult("ObtenerGradov1", new { id = grado.Id }, gradoDTO);
        }

        /// <summary>
        /// Actualizar un Grado en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Grado</param>
        /// <param name="gradoCreate">Cuerpo del Grado a actualizar</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<GradoDTO>> Put(int id, [FromBody] GradoCreateDTO gradoCreate)
        {
            var grado = mapper.Map<Grado>(gradoCreate);
            var gradoRepo = await repository.Update(id, grado);
            if (gradoRepo == null)
            {
                return NotFound();
            }
            var gradoDTO = mapper.Map<GradoDTO>(gradoRepo);
            return new CreatedAtRouteResult("ObtenerGradov1", new { id = grado.Id }, gradoDTO);
        }

        /// <summary>
        /// Eliminar un Grado en específico mediante Id
        /// </summary>
        /// <param name="id">Id del Grado a eliminar</param>
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