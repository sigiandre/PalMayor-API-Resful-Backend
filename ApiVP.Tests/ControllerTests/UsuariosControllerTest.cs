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
    public class UsuariosControllerTest
    {
        public List<Usuario> listaUsuarios = new List<Usuario>{
            new Usuario{Id=1,Correo="usuario1@gmail.com",Contrasenya="contraseña123",RolId=1},
            new Usuario{Id=2,Correo="usuario2@gmail.com",Contrasenya="contraseña456",RolId=2},
        };
        public Usuario usuario = new Usuario { Id = 1, Correo = "usuario1@gmail.com", Contrasenya = "contraseña123", RolId = 1 };

        [Fact]
        public async Task Verificar_GetListUsuario()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IUsuarioRepository>();
            repository.Setup(x => x.GetAll()).ReturnsAsync(listaUsuarios);
            var controller = new UsuariosController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get();
            var result = actionResult.Result as OkObjectResult;
            var arr = result.Value as List<UsuarioDTO>;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<List<UsuarioDTO>>(arr);
            Assert.Equal(2, arr.Count);
        }

        [Fact]
        public async Task Verificar_GetUsuario()
        {
            //ARRANGE
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IUsuarioRepository>();
            repository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(usuario);
            var controller = new UsuariosController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Get(1);
            var result = actionResult.Result as OkObjectResult;
            var dto = result.Value as UsuarioDTO;

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<UsuarioDTO>(dto);
            Assert.Equal(1, dto.Id);
        }

        [Fact]
        public async Task Validar_PostUsuario()
        {
            Usuario nuevo = new Usuario
            {
                Id = 3,
                Correo = "usuario1@gmail.com",
                Contrasenya = "contraseña123",
                RolId = 1
            };
            UsuarioCreateDTO nuevoCreate = new UsuarioCreateDTO
            {
                Correo = "usuario1@gmail.com",
                Contrasenya = "contraseña123",
                RolId = 1
            };
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IUsuarioRepository>();
            repository.Setup(x => x.Save(It.IsAny<Usuario>())).ReturnsAsync(nuevo).Verifiable();
            var controller = new UsuariosController(repository.Object, mapper);

            //act
            var actionResult = await controller.Post(nuevoCreate);
            var result = actionResult.Result as CreatedAtRouteResult;
            var dto = result.Value as UsuarioDTO;
            Assert.Equal(3, dto.Id);
        }

        [Fact]
        public async Task Validar_LoginUsuario()
        {
            //ARRANGE
            Usuario usuarioRegistrado = new Usuario
            {
                Id = 3,
                Correo = "usuario.registrado@gmail.com",
                Contrasenya = "registrado123",
                RolId = 2
            };
            UsuarioCreateDTO registradoDTO = new UsuarioCreateDTO
            {
                Correo = "usuario.registrado@gmail.com",
                Contrasenya = "registrado123",
                RolId = 2
            };
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var repository = new Mock<IUsuarioRepository>();
            repository.Setup(x => x.Login(It.IsAny<Usuario>())).ReturnsAsync(usuarioRegistrado).Verifiable();
            var controller = new UsuariosController(repository.Object, mapper);

            //ACT
            var actionResult = await controller.Login(registradoDTO);
            var result = actionResult.Result as CreatedAtRouteResult;
            var dto = result.Value as UsuarioDTO;

            //ASSERT
            Assert.Equal(registradoDTO.Correo, dto.Correo);
        }

    }
}