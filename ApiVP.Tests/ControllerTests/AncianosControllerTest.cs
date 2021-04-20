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
    public class AncianosControllerTest
    {
        public List<Anciano> listaAncianos = new List<Anciano>{
            new Anciano{ Id = 1, FamiliarId =1 , PersonaId = 2},
            new Anciano{ Id = 2, FamiliarId =1 , PersonaId = 3}
        };

        public Anciano anciano = new Anciano { Id = 1, FamiliarId = 1, PersonaId = 2 };

        [Fact]
        public async Task Verificar_GetListAncianos()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IAncianoRepository>();
            repository.Setup(x => x.GetAll()).ReturnsAsync(listaAncianos);
            var controller = new AncianosController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;
            var arr = result.Value as List<AncianoDTO>;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<List<AncianoDTO>>(arr);
            Assert.Equal(2, arr.Count);
        }

        [Fact]
        public async Task Verificar_GetAnciano()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IAncianoRepository>();
            repository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(anciano);
            var controller = new AncianosController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get(1);
            var result = actionResult.Result as OkObjectResult;
            var dto = result.Value as AncianoDTO;
            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<AncianoDTO>(dto);
            Assert.Equal(1, dto.Id);
        }

        [Fact]
        public async Task Verificar_PostRol()
        {
            //ARRANGE
            Anciano nuevo = new Anciano { Id = 3, PersonaId = 2 };
            AncianoCreateDTO nuevoCreate = new AncianoCreateDTO { FamiliarId = 3 };
            var mockMapper = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile(new MappingProfile());
           });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IAncianoRepository>();
            repository.Setup(x => x.Save(It.IsAny<Anciano>())).ReturnsAsync(nuevo).Verifiable();

            //ACT
            var controller = new AncianosController(repository.Object, mapper);

            var actionResult = await controller.Post(nuevoCreate);
            var result = actionResult.Result as CreatedAtRouteResult;
            var dto = result.Value as AncianoDTO;
            //ASSERT
            Assert.Equal(3, dto.Id);
        }

    }
}