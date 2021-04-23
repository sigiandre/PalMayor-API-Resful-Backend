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
    public class ABVCControllerTest
    {
        public List<ABVC> listaABVCs = new List<ABVC>{
            new ABVC{ Id = 1 , Descripcion = "Se baña enteramente solo", TipoId = 1},
            new ABVC{ Id = 2 , Descripcion = "Necesista ayuda prar bañarse", TipoId = 2 }
        };
        public ABVC abvc = new ABVC { Id = 1, Descripcion = "Se baña enteramente solo", TipoId = 1 };

        [Fact]
        public async Task Verificar_GetListABVCs()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IABVCRepository>();
            repository.Setup(x => x.GetAll()).ReturnsAsync(listaABVCs);
            var controller = new ABVCsController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;
            var arr = result.Value as List<ABVCDTO>;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<List<ABVCDTO>>(arr);
            Assert.Equal(2, arr.Count);
        }

        [Fact]
        public async Task Verificar_GetABVC()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IABVCRepository>();
            repository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(abvc);
            var controller = new ABVCsController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get(1);
            var result = actionResult.Result as OkObjectResult;
            var dto = result.Value as ABVCDTO;
            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<ABVCDTO>(dto);
            Assert.Equal(1, dto.Id);
        }
    }
}