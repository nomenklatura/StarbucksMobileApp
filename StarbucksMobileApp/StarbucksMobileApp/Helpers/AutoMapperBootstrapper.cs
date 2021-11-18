using AutoMapper;
using StarbucksMobileApp.Api.ResponseModels;
using StarbucksMobileApp.Models;

namespace StarbucksMobileApp.Helpers
{
    public static class AutoMapperBootstrapper
    {
        public static IMapper mapper { get => CreateMapper(); }

        private static IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Member, MemberResponseModel>();
                cfg.CreateMap<MemberResponseModel, Member>();
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
