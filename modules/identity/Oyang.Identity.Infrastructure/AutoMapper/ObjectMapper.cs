using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Infrastructure.AutoMapper
{
    public class ObjectMapper : Oyang.Identity.Domain.IObjectMapper
    {
        private readonly IMapper _mapper;
        public ObjectMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map<TSource, TDestination>(source, destination);
        }
    }
}
