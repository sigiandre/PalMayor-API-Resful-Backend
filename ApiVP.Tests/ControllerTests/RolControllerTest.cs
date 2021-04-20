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
    public class RolesControllerTest
    {
        public List<Rol> listaRoles = new List<Rol>{
            new Rol{ Id = 1, Nombre = "Administrador"},
            new Rol{ Id = 2, Nombre = "Familiar"}
        };
        public Rol rol = new Rol { Id = 1, Nombre = "Administrador" };

        [Fact]
        public async Task Verificar_GetListRoles()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IRolRepository>();
            repository.Setup(x => x.GetAll()).ReturnsAsync(listaRoles);
            var controller = new RolesController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;
            var arr = result.Value as List<RolDTO>;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<List<RolDTO>>(arr);
        }

        [Fact]
        public async Task Verificar_GetRol()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IRolRepository>();
            repository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(rol);
            var controller = new RolesController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get(1);
            var result = actionResult.Result as OkObjectResult;
            var dto = result.Value as RolDTO;
            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<RolDTO>(dto);
            Assert.Equal(1, dto.Id);
        }


        [Fact]
        public async Task Verificar_PostRol()
        {
            Rol nuevo = new Rol { Id = 3, Nombre = "Anciano" };
            RolCreateDTO nuevoCreate = new RolCreateDTO { Nombre = "Anciano" };
            var mockMapper = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile(new MappingProfile());
           });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IRolRepository>();
            repository.Setup(x => x.Save(It.IsAny<Rol>())).ReturnsAsync(nuevo).Verifiable();
            var controller = new RolesController(repository.Object, mapper);

            //act
            var actionResult = await controller.Post(nuevoCreate);
            var result = actionResult.Result as CreatedAtRouteResult;
            var dto = result.Value as RolDTO;
            Assert.Equal(3, dto.Id);
        }
    }
}