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
    public class OfertasControllerTest
    {
        public List<Oferta> listaOfertas = new List<Oferta>{
            new Oferta{ Id=1, AncianoId=1, Estado="activo",Direccion="Direccion test",Descripcion="Descripcion test"},
            new Oferta{ Id=2, AncianoId=2, Estado="activo",Direccion="Direccion test",Descripcion="Descripcion test"}
        };
        public Oferta oferta = new Oferta { Id = 1, AncianoId = 1, Estado = "activo", Direccion = "Direccion test", Descripcion = "Descripcion test" };

        [Fact]
        public async Task Verificar_GetListOfertas()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IOfertaRepository>();
            repository.Setup(x => x.GetAll()).ReturnsAsync(listaOfertas);
            var controller = new OfertasController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;
            var arr = result.Value as List<OfertaDTO>;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<List<OfertaDTO>>(arr);
            Assert.Equal(2, arr.Count);
        }

        [Fact]
        public async Task Verificar_GetOferta()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IOfertaRepository>();
            repository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(oferta);
            var controller = new OfertasController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get(1);
            var result = actionResult.Result as OkObjectResult;
            var dto = result.Value as OfertaDTO;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<OfertaDTO>(dto);
            Assert.Equal(1, dto.Id);
        }

        [Fact]
        public async Task Validar_PostOfertas()
        {
            Oferta nuevo = new Oferta
            {
                Id = 3,
                AncianoId = 3,
                Estado = "activo",
                Direccion = "Direccion test",
                Descripcion = "Descripcion test"
            };
            OfertaCreateDTO nuevoCreate = new OfertaCreateDTO
            {
                Direccion = "DireccionTest",
                Descripcion = "DescripcionTest",
                AncianoId = 3
            };
            var mockMapper = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile(new MappingProfile());
           });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IOfertaRepository>();
            repository.Setup(x => x.Save(It.IsAny<Oferta>())).ReturnsAsync(nuevo).Verifiable();
            var controller = new OfertasController(repository.Object, mapper);

            //act
            var actionResult = await controller.Post(nuevoCreate);
            var result = actionResult.Result as CreatedAtRouteResult;
            var dto = result.Value as OfertaDTO;
            Assert.Equal(3, dto.Id);
        }
    }
}