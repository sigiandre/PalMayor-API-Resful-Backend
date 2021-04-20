using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ApiVP.Repository;
using ApiVP.Api.Controllers.v1;
using ApiVP.Domain.Entities;
using ApiVP.Domain.Models;
using ApiVP.Api;
using AutoMapper;


namespace ApiVP.Tests.ControllerTests
{
    public class EnfermerosControlleTest
    {
        public List<Enfermero> listaEnfermeros = new List<Enfermero> {
            new Enfermero { Id = 1, UsuarioId = 1, PersonaId = 1, Colegiatura = "12345678", Universidad = "UPC", Experiencia = "Descripcion", GradoId = 1, EspecialidadId = 1 },
            new Enfermero { Id = 2, UsuarioId = 2, PersonaId = 2, Colegiatura = "87654321", Universidad = "UTEC", Experiencia = "Descripcion", GradoId = 2, EspecialidadId = 2 }
            };
        public Enfermero enfermero = new Enfermero { Id = 1, UsuarioId = 1, PersonaId = 1, Colegiatura = "12345678", Universidad = "UPC", Experiencia = "Descripcion", GradoId = 1, EspecialidadId = 1 };

        [Fact]
        public async Task Verificar_GetListEnfermeros()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingProfile());
                });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IEnfermeroRepository>();
            repository.Setup(x => x.GetAll()).ReturnsAsync(listaEnfermeros);
            var controller = new EnfermerosController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;
            var arr = result.Value as List<EnfermeroDTO>;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<List<EnfermeroDTO>>(arr);
            Assert.Equal(2, arr.Count);
        }

        [Fact]
        public async Task Verificar_GetEnfermero()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
               {
                   cfg.AddProfile(new MappingProfile());
               });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IEnfermeroRepository>();
            repository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(enfermero);
            var controller = new EnfermerosController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get(1);
            var result = actionResult.Result as OkObjectResult;
            var dto = result.Value as EnfermeroDTO;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<EnfermeroDTO>(dto);
            Assert.Equal(1, dto.Id);
        }

        [Fact]
        public async Task Verificar_PostEnfermero()
        {
            //ARRANGE
            Enfermero nuevo = new Enfermero { Id = 3, UsuarioId = 3, PersonaId = 3, Colegiatura = "87654321", Universidad = "UTEC", Experiencia = "Descripcion", GradoId = 2, EspecialidadId = 2 };
            EnfermeroCreateDTO nuevoCreate = new EnfermeroCreateDTO { UsuarioId = 3, Colegiatura = "87654321", Universidad = "UTEC", Experiencia = "Descripcion", GradoId = 2, EspecialidadId = 2 };
            var mockMapper = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile(new MappingProfile());
           });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IEnfermeroRepository>();
            repository.Setup(x => x.Save(It.IsAny<Enfermero>())).ReturnsAsync(nuevo).Verifiable();

            //ACT
            var controller = new EnfermerosController(repository.Object, mapper);

            var actionResult = await controller.Post(nuevoCreate);
            var result = actionResult.Result as CreatedAtRouteResult;
            var dto = result.Value as EnfermeroDTO;
            //ASSERT
            Assert.Equal(3, dto.Id);
        }
    }
}