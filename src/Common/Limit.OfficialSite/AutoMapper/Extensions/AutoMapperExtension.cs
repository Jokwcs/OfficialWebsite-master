using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Limit.OfficialSite.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Limit.OfficialSite.AutoMapper.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            var assemblies = ConfigurationManager.GetConfig("Assembly:Mapper");

            if (string.IsNullOrEmpty(assemblies))
                return services;

            var profiles = LoadTypes(assemblies);

            if (profiles.Count != 0 || profiles.Any())
                services.AddAutoMapper(profiles.ToArray());

            return services;
        }

        private static List<Type> LoadTypes(string assemblies)
        {
            var profiles = new List<Type>();

            var parentType = typeof(Profile);

            foreach (var item in assemblies.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var types = Assembly.Load(item).GetTypes().Where(i => i.BaseType != null && i.BaseType.Name == parentType.Name);

                var enumerable = types as Type[] ?? types.ToArray();

                if (enumerable.Length != 0 || enumerable.Any())
                    profiles.AddRange(enumerable);
            }

            return profiles;
        }
    }
}