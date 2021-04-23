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
    public class GradosControllerTest
    {
        public List<Grado> listaGrados = new List<Grado> { new Grado() { Id = 1, Nombre = "test" }, new Grado() { Id = 2, Nombre = "test3" }, new Grado() { Id = 3, Nombre = "test3" } };
        public Grado grado = new Grado { Id = 1, Nombre = "Bachiller" };
        [Fact]
        public async Task Verificar_GetListGrados()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IGradoRepository>();
            repository.Setup(x => x.GetAll()).ReturnsAsync(listaGrados);
            var controller = new GradosController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;
            var arr = result.Value as List<GradoDTO>;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<List<GradoDTO>>(arr);
        }

        [Fact]
        public async Task Verificar_GetGrado()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IGradoRepository>();
            repository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(grado);
            var controller = new GradosController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get(1);
            var result = actionResult.Result as OkObjectResult;
            var dto = result.Value as GradoDTO;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<GradoDTO>(dto);
            Assert.Equal(1, dto.Id);
        }
    }
}
