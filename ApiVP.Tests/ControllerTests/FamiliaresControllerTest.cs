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

namespace ApiVP.Tests
{
    public class FamiliaresControllerTest
    {
        public List<Familiar> listaFamiliares = new List<Familiar>{
            new Familiar{ Id = 1, UsuarioId = 1, PersonaId = 1}, new Familiar{ Id = 2, UsuarioId = 2, PersonaId = 2}
        };
        public Familiar familiar = new Familiar { Id = 1, UsuarioId = 1, PersonaId = 1 };
        [Fact]
        public async Task Verificar_GetListFamiliares()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IFamiliarRepository>();
            repository.Setup(x => x.GetAll()).ReturnsAsync(listaFamiliares);
            var controller = new FamiliaresController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;
            var arr = result.Value as List<FamiliarDTO>;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<List<FamiliarDTO>>(arr);
            Assert.Equal(2, arr.Count);
        }

        [Fact]
        public async Task Verificar_GetFamiliar()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IFamiliarRepository>();
            repository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(familiar);
            var controller = new FamiliaresController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get(1);
            var result = actionResult.Result as OkObjectResult;
            var dto = result.Value as FamiliarDTO;
            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<FamiliarDTO>(dto);
            Assert.Equal(1, dto.Id);
        }

        [Fact]
        public async Task Verificar_PostFamiliar()
        {
            Familiar nuevo = new Familiar { Id = 3, UsuarioId = 3, PersonaId = 3 };
            FamiliarCreateDTO nuevoCreate = new FamiliarCreateDTO { UsuarioId = 3 };
            var mockMapper = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile(new MappingProfile());
           });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IFamiliarRepository>();
            repository.Setup(x => x.Save(It.IsAny<Familiar>())).ReturnsAsync(nuevo).Verifiable();
            var controller = new FamiliaresController(repository.Object, mapper);

            //act
            var actionResult = await controller.Post(nuevoCreate);
            var result = actionResult.Result as CreatedAtRouteResult;
            var dto = result.Value as FamiliarDTO;
            Assert.Equal(3, dto.Id);
        }
    }
}