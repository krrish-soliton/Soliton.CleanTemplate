using AutoMapper;
using System;
using System.Collections.Generic;

namespace Soliton.Shared
{
    // TODO: why does this need to be in a folder?
    /// <summary>
    /// Static extensions for auto mapping models
    /// </summary>
    public static class AutoMappers
    {
        private static Dictionary<Type, Dictionary<Type, IMapper>> Mappers { get; } = [];

        /// <summary>
        /// Get mapper for generic type
        /// </summary>
        /// <typeparam name="TSource">Type of the source object</typeparam>
        /// <typeparam name="TDest">Type of the destination object</typeparam>
        /// <returns>Mapper object</returns>
        public static IMapper GetMapper<TSource, TDest>()
        {
            Mappers.TryGetValue(typeof(TSource), out Dictionary<Type, IMapper>? mapDictionary);
            if (mapDictionary == null)
            {
                mapDictionary = [];
                Mappers[typeof(TSource)] = mapDictionary;
            }
            if (!mapDictionary.TryGetValue(typeof(TDest), out IMapper? mapper))
            {
                MapperConfiguration config = new(cfg => cfg.CreateMap<TSource, TDest>());
                mapper = config.CreateMapper();
                mapDictionary[typeof(TDest)] = mapper;
            }
            return mapper;
        }

        /// <summary>
        /// Converts from generic model to destination
        /// </summary>
        /// <typeparam name="TSource">Type of source generic Model</typeparam>
        /// <typeparam name="TDest">Type of destination generic Model</typeparam>
        /// <param name="sourceModel">Source model object</param>
        /// <returns>Mapped destination model</returns>
        public static TDest ConvertTo<TSource, TDest>(this TSource sourceModel) => GetMapper<TSource, TDest>().Map<TDest>(sourceModel);

        /// <summary>
        /// Converts from generic model to destination
        /// </summary>
        /// <typeparam name="TSource">Type of source generic Model</typeparam>
        /// <typeparam name="TDest">Type of destination generic Model</typeparam>
        /// <param name="sourceModel">Source model object</param>
        /// <param name="postConversion">Action to run after conversion is done</param>
        /// <returns>Mapped destination model</returns>
        public static TDest ConvertTo<TSource, TDest>(this TSource sourceModel, Action<TDest>? postConversion)
        {
            TDest result = GetMapper<TSource, TDest>().Map<TDest>(sourceModel);
            postConversion?.Invoke(result);
            return result;
        }
    }
}
