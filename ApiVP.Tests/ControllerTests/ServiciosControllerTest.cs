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
    public class ServiciosControllerTest
    {
        public List<Servicio> listaServicios = new List<Servicio>{
            new Servicio{Id=1,EnfermeroId=1,OfertaId=1,Estado="activo",Costo=100},
            new Servicio{Id=2,EnfermeroId=2,OfertaId=2,Estado="activo",Costo=200},
        };
        public Servicio servicio = new Servicio { Id = 1, EnfermeroId = 1, OfertaId = 1, Estado = "activo", Costo = 100 };

        [Fact]
        public async Task Verificar_GetListServicios()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IServicioRepository>();
            repository.Setup(x => x.GetAll()).ReturnsAsync(listaServicios);
            var controller = new ServiciosController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;
            var arr = result.Value as List<ServicioDTO>;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<List<ServicioDTO>>(arr);
            Assert.Equal(2, arr.Count);
        }

        [Fact]
        public async Task Verificar_GetServicio()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IServicioRepository>();
            repository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(servicio);
            var controller = new ServiciosController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get(1);
            var result = actionResult.Result as OkObjectResult;
            var dto = result.Value as ServicioDTO;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<ServicioDTO>(dto);
            Assert.Equal(1, dto.Id);
        }
        [Fact]
        public async Task Verificar_PostServicio()
        {
            //ARRANGE
            Servicio nuevo = new Servicio { Id = 3, EnfermeroId = 3, OfertaId = 2, Estado = "activo", Costo = 100 };
            ServicioCreateDTO nuevoCreate = new ServicioCreateDTO { EnfermeroId = 3, OfertaId = 2 };
            var mockMapper = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile(new MappingProfile());
           });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IServicioRepository>();
            repository.Setup(x => x.Save(It.IsAny<Servicio>())).ReturnsAsync(nuevo).Verifiable();
            var controller = new ServiciosController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Post(nuevoCreate);
            var result = actionResult.Result as CreatedAtRouteResult;
            var dto = result.Value as ServicioDTO;

            //ASSERT
            Assert.Equal(3, dto.Id);
        }
    }
}