using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain
{
    public interface IObjectMapper
    {
        TDestination Map<TSource, TDestination>();
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
