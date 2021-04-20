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
    public class EnfermeroOfertasControllerTest
    {
        public List<EnfermeroOferta> listaEnfermeroOfertas = new List<EnfermeroOferta>{
            new EnfermeroOferta{EnfermeroId=1,OfertaId=1},
            new EnfermeroOferta{EnfermeroId=1,OfertaId=2}
        };
        public EnfermeroOferta enfermeroOferta = new EnfermeroOferta { EnfermeroId = 1, OfertaId = 1 };

        [Fact]
        public async Task Verificar_GetListEnfermeroOfertas()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IEnfermeroOfertaRepository>();
            repository.Setup(x => x.GetAllByEnfermeroCorreo("correo.test@gmail.com")).ReturnsAsync(listaEnfermeroOfertas);
            var controller = new EnfermeroOfertasController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.GetByEnfermero("correo.test@gmail.com");
            var result = actionResult.Result as OkObjectResult;
            var arr = result.Value as List<EnfermeroOfertaDTO>;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<List<EnfermeroOfertaDTO>>(arr);
            Assert.Equal(2, arr.Count);
        }

        [Fact]
        public async Task Verificar_GetEnfermeroOferta()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IEnfermeroOfertaRepository>();
            repository.Setup(x => x.GetByEnfermeroByOferta(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(enfermeroOferta);
            var controller = new EnfermeroOfertasController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get(1, 1);
            var result = actionResult.Result as OkObjectResult;
            var dto = result.Value as EnfermeroOfertaDTO;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<EnfermeroOfertaDTO>(dto);
            Assert.Equal(1, dto.EnfermeroId);
            Assert.Equal(1, dto.OfertaId);
        }
    }
}