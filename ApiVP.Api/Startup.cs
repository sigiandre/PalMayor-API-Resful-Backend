using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using ApiVP.Repository.Context;
using ApiVP.Domain.Entities;
using ApiVP.Domain.Models;
using ApiVP.Repository;
using ApiVP.Repository.Implementation;
using System.Reflection;
using System.IO;
using ApiVP.Repository.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace ApiVP.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEspecialidadRepository, EspecialidadRepository>();
            services.AddTransient<IGradoRepository, GradoRepository>();
            services.AddTransient<IRangoHoraRepository, RangoHoraRepository>();
            services.AddTransient<IFechaAtencionRepository, FechaAtencionRepository>();
            services.AddTransient<IEnfermeroRepository, EnfermeroRepository>();
            services.AddTransient<IServicioRepository, ServicioRepository>();
            services.AddTransient<IOfertaRepository, OfertaRepository>();
            services.AddTransient<IRolRepository, RolRepository>();
            services.AddTransient<ITipoRepository, TipoRepository>();
            services.AddTransient<IABVCRepository, ABVCRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IFamiliarRepository, FamiliarRepository>();
            services.AddTransient<IAncianoRepository, AncianoRepository>();
            services.AddTransient<IEnfermeroOfertaRepository, EnfermeroOfertaRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            /*services.AddAutoMapper(Configuration =>
            {
                Configuration.CreateMap<Especialidad, EspecialidadDTO>();
                Configuration.CreateMap<EspecialidadCreateDTO, Especialidad>().ReverseMap();

                Configuration.CreateMap<Oferta, OfertaDTO>();
                Configuration.CreateMap<OfertaCreateDTO, Oferta>().ReverseMap();

                Configuration.CreateMap<Enfermero, EnfermeroDTO>();
                Configuration.CreateMap<EnfermeroCreateDTO, Enfermero>().ReverseMap();

                Configuration.CreateMap<RangoHora, RangoHoraDTO>();
                Configuration.CreateMap<RangoHoraCreateDTO, RangoHora>().ReverseMap();

                Configuration.CreateMap<FechaAtencion, FechaAtencionDTO>();
                Configuration.CreateMap<FechaAtencionCreateDTO, FechaAtencion>().ReverseMap();

                Configuration.CreateMap<Oferta, OfertaDTO>();
                Configuration.CreateMap<OfertaCreateDTO, Oferta>().ReverseMap();

                Configuration.CreateMap<EnfermeroOferta, EnfermeroOfertaDTO>();
                Configuration.CreateMap<EnfermeroOfertaCreateDTO, EnfermeroOferta>().ReverseMap();

                Configuration.CreateMap<Servicio, ServicioDTO>();
                Configuration.CreateMap<ServicioCreateDTO, Servicio>().ReverseMap();

                Configuration.CreateMap<Rol, RolDTO>();
                Configuration.CreateMap<RolCreateDTO, Rol>().ReverseMap();

                Configuration.CreateMap<Usuario, UsuarioDTO>();
                Configuration.CreateMap<UsuarioCreateDTO, Usuario>().ReverseMap();

                Configuration.CreateMap<Persona, PersonaDTO>();
                Configuration.CreateMap<PersonaCreateDTO, Persona>().ReverseMap();

                Configuration.CreateMap<Familiar, FamiliarDTO>();
                Configuration.CreateMap<FamiliarCreateDTO, Familiar>().ReverseMap();

                Configuration.CreateMap<Anciano, AncianoDTO>();
                Configuration.CreateMap<AncianoCreateDTO, Anciano>().ReverseMap();

                Configuration.CreateMap<AncianoABVC, AncianoABVCDTO>();
                Configuration.CreateMap<AncianoABVCCreateDTO, AncianoABVC>().ReverseMap();

                Configuration.CreateMap<ABVC, ABVCDTO>();
                Configuration.CreateMap<ABVCCreateDTO, ABVC>().ReverseMap();

                Configuration.CreateMap<Tipo, TipoDTO>();
                Configuration.CreateMap<TipoCreateDTO, Tipo>().ReverseMap();

                Configuration.CreateMap<Grado, GradoDTO>();
                Configuration.CreateMap<GradoCreateDTO, Grado>().ReverseMap();

            }, typeof(Startup));*/

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddDbContext<ApplicationSecurityDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationSecurityDbContext>()
                    .AddDefaultTokenProviders();

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Vida Plena API v1",
                    Description = "servicios api restul para la aplicación web de Vida Plena",
                    Contact = new OpenApiContact()
                    {
                        Name = "Vida Plena",
                        Email = "u201621283@upc.edu.pe",
                        Url = new Uri("https://github.com/sigiandre/PalMayor-API-Resful-Backend")
                    }
                });

                config.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "Vida Plena API v2",
                    Description = "servicios api restul para la aplicación web de Vida Plena",
                    Contact = new OpenApiContact()
                    {
                        Name = "Vida Plena",
                        Email = "u201621283@upc.edu.pe",
                        Url = new Uri("https://github.com/sigiandre/PalMayor-API-Resful-Backend")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });

            services.AddCors(options => { options.AddPolicy("All", builder => builder.WithOrigins("*").WithHeaders("*").WithMethods("*")); });

            services.AddControllers(
                config =>
                {
                    config.Conventions.Add(new ApiVersionConvention());
                }
            ).AddNewtonsoftJson(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"])),
                    ClockSkew = TimeSpan.Zero
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Vida Plena API V1");
                config.SwaggerEndpoint("/swagger/v2/swagger.json", "Vida Plena API V2");
                config.RoutePrefix = "";
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("All");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
