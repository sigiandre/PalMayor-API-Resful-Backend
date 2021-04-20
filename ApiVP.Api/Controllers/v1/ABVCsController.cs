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
    public class ABVCsController : ControllerBase
    {
        private readonly IABVCRepository repository;
        private readonly IMapper mapper;

        public ABVCsController(IABVCRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtener todos los ABVC(Actividad Básica de la Vida Cotidiana)
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<ABVCDTO>>> Get()
        {
            var abvcsDTO = mapper.Map<List<ABVCDTO>>(await repository.GetAll());
            return Ok(abvcsDTO);
        }

        /// <summary>
        /// Obtener un ABVC(Actividad Básica de la Vida Cotidiana) en específico
        /// </summary>
        /// <param name="id">Id del ABVC a obtener</param>
        [HttpGet("{id}", Name = "ObtenerABVCv1")]
        public async Task<ActionResult<ABVCDTO>> Get(int id)
        {
            var abvcDTO = mapper.Map<ABVCDTO>(await repository.Get(id));
            if (abvcDTO == null)
            {
                return NotFound();
            }
            return Ok(abvcDTO);
        }

        /// <summary>
        /// Registra un ABVC(Actividad Básica de la Vida Cotidiana)
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ABVCDTO>> Post([FromBody] ABVCCreateDTO abvcCreate)
        {
            var abvc = mapper.Map<ABVC>(abvcCreate);
            var abvcDTO = mapper.Map<ABVCDTO>(await repository.Save(abvc));
            return new CreatedAtRouteResult("ObtenerABVCv1", new { id = abvc.Id }, abvcDTO);
        }

        /// <summary>
        /// Actualiza un ABVC(Actividad Básica de la Vida Cotidiana) en específico
        /// </summary>
        /// <param name="id">Id del ABVC a actualizar</param>
        /// <param name="abvcCreate">Cuerpo del ABVC a actualizar</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<ABVCDTO>> Put(int id, [FromBody] ABVCCreateDTO abvcCreate)
        {
            var abvc = mapper.Map<ABVC>(abvcCreate);
            var abvcRepo = await repository.Update(id, abvc);
            if (abvcRepo == null)
            {
                return NotFound();
            }
            var abvcDTO = mapper.Map<ABVCDTO>(abvcRepo);
            return new CreatedAtRouteResult("ObtenerABVCv1", new { id = abvc.Id }, abvcDTO);
        }

        /// <summary>
        /// Elimina un ABVC(Actividad Básica de la Vida Cotidiana) en específico
        /// </summary>
        /// <param name="id">Id del ABVC a eliminar</param>
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