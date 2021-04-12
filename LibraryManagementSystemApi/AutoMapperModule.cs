using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AutoMapper;
using LibraryManagementSystem.DTO.Profiles;

namespace LibraryManagementSystemApi
{
    public class AutoMapperModule : Autofac.Module
    {

        // ReSharper disable once NotAccessedField.Local
        private readonly IEnumerable<Assembly> _assembliesToScan;

        private AutoMapperModule(IEnumerable<Assembly> assembliesToScan)
        {
            this._assembliesToScan = assembliesToScan;
        }

        public AutoMapperModule(params Assembly[] assembliesToScan) : this((IEnumerable<Assembly>) assembliesToScan)
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
           
            builder.Register<IConfigurationProvider>(ctx => new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile))))
                .SingleInstance();

            builder.Register<IMapper>(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>(), ctx.Resolve))
                .InstancePerDependency();
        }
    }
}