using AutoMapper;
using Manager.API.ViewModels;
using Manager.Domain.Entities;
using Manager.Services.DTO;

namespace Manager.Tests.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static IMapper GetConfiguration()
        {
            var autoMapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<User, UserDTO>().ReverseMap();
                cfg.CreateMap<CreateUserViewModel, UserDTO>().ReverseMap();
                cfg.CreateMap<UpdateUserViewModel, UserDTO>().ReverseMap();

            });
            return autoMapperConfig.CreateMapper();
        }
    }
}