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
    public class RangoHorasControllerTest
    {
        public List<RangoHora> listaHoras = new List<RangoHora>{
            new RangoHora{Id=1,Inicio=new TimeSpan( 8,30,0 ), Fin=new TimeSpan(10,30,0)},
            new RangoHora{Id=2,Inicio=new TimeSpan( 6,30,0 ), Fin=new TimeSpan(8,30,0)}
        };
        public RangoHora rangoHora = new RangoHora { Id = 1, Inicio = new TimeSpan(8, 30, 0), Fin = new TimeSpan(10, 30, 0) };

        [Fact]
        public async Task Verificar_GetListRangoHoras()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IRangoHoraRepository>();
            repository.Setup(x => x.GetAll()).ReturnsAsync(listaHoras);
            var controller = new RangoHorasController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;
            var arr = result.Value as List<RangoHoraDTO>;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<List<RangoHoraDTO>>(arr);
            Assert.Equal(2, arr.Count);
        }

        [Fact]
        public async Task Verificar_GetRangoHora()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IRangoHoraRepository>();
            repository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(rangoHora);
            var controller = new RangoHorasController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get(1);
            var result = actionResult.Result as OkObjectResult;
            var dto = result.Value as RangoHoraDTO;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<RangoHoraDTO>(dto);
            Assert.Equal(1, dto.Id);
        }
    }
}