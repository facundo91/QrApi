﻿using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using qrAPI.Contracts;
using qrAPI.DAL.Data;
using qrAPI.Logic.Services.Interfaces;
using qrAPI.Presentation.Filters;

namespace qrAPI.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc(options => { options.Filters.Add<ValidationFilter>(); })
                .AddFluentValidation(mvcConfiguration => mvcConfiguration
                    .RegisterValidatorsFromAssemblyContaining<IDataContext>()
                    .RegisterValidatorsFromAssemblyContaining(typeof(ApiVersions))
                    .RegisterValidatorsFromAssemblyContaining(typeof(IGenericService<>)));
            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddResponseCaching();
            services.AddAutoMapper(typeof(Startup));
        }
    }
}