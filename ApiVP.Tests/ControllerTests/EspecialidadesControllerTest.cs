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
    public class EspecialidadesControllerTest
    {
        public List<Especialidad> listaEspecialidades = new List<Especialidad>{
            new Especialidad{ Id = 1, Nombre= "Geriatria" }, new Especialidad { Id = 2, Nombre = "Geriatria"}
        };
        public Especialidad especialidad = new Especialidad { Id = 1, Nombre = "Geriatría" };

        [Fact]
        public async Task Verificar_GetListEspcialidades()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IEspecialidadRepository>();
            repository.Setup(x => x.GetAll()).ReturnsAsync(listaEspecialidades);
            var controller = new EspecialidadesController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;
            var arr = result.Value as List<EspecialidadDTO>;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<List<EspecialidadDTO>>(arr);
            Assert.Equal(2, arr.Count);
        }

        [Fact]
        public async Task Verificar_GetEspecialidad()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IEspecialidadRepository>();
            repository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(especialidad);
            var controller = new EspecialidadesController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get(1);
            var result = actionResult.Result as OkObjectResult;
            var dto = result.Value as EspecialidadDTO;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<EspecialidadDTO>(dto);
            Assert.Equal(1, dto.Id);
        }
    }
}