using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Infrastructure
{
    public class MappingProvider : IMappingProvider
    {
        private IMapper mapper;

        public MappingProvider(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TDestination MapTo<TDestination>(object source)
        {
            return this.mapper.Map<TDestination>(source);
        }

        //public IEnumerable<TDestination> ProjectTo<TDestination>(IEnumerable<object> source)
        //{
        //    return this.ProjectTo<TDestination>(source);
        //}

        //public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable<object> source)
        //{
        //    return this.ProjectTo<TDestination>(source);
        //}
    }
}
