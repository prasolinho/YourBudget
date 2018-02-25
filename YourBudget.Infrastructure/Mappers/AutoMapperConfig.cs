using AutoMapper;
using YourBudget.Core.Domain;
using YourBudget.Infrastructure.DTO;

namespace YourBudget.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cft =>
            {
                cft.CreateMap<User, UserDto>();
            }).CreateMapper();
    }
}