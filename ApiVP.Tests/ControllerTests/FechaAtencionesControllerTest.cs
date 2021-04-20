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
    public class FechaAtencionesControllerTest
    {
        public List<FechaAtencion> listaAtenciones = new List<FechaAtencion>{
            new FechaAtencion{Id=1,Fecha=new DateTime(2020,04,03), RangoHoraId=1,OfertaId=1},
            new FechaAtencion{Id=2,Fecha=new DateTime(2020,03,04), RangoHoraId=1,OfertaId=2}
        };
        public FechaAtencion fechaAtencion = new FechaAtencion { Id = 1, Fecha = new DateTime(2020, 04, 03), RangoHoraId = 1, OfertaId = 1 };

        [Fact]
        public async Task Verificar_GetListFechaAtenciones()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IFechaAtencionRepository>();
            repository.Setup(x => x.GetAll()).ReturnsAsync(listaAtenciones);
            var controller = new FechaAtencionesController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;
            var arr = result.Value as List<FechaAtencionDTO>;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<List<FechaAtencionDTO>>(arr);
            Assert.Equal(2, arr.Count);
        }

        [Fact]
        public async Task Verificar_GetFechaAtencion()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IFechaAtencionRepository>();
            repository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(fechaAtencion);
            var controller = new FechaAtencionesController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get(1);
            var result = actionResult.Result as OkObjectResult;
            var dto = result.Value as FechaAtencionDTO;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<FechaAtencionDTO>(dto);
            Assert.Equal(1, dto.Id);
        }
    }
}