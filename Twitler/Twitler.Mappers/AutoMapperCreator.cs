using AutoMapper;
using Ninject.Activation;
using Twitler.Domain.Model;
using Twitler.ViewModels.Twit;

namespace Twitler.Mappers
{
    public class AutoMapperCreator
    {
        public static IMapper GetMapper(IContext context)
        {
            MapperConfiguration config = new MapperConfiguration(RegisterMappings);
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        private static void RegisterMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Twit, TwitVm>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Email));
            config.CreateMap<PostedTwitJm, Twit>();

        }
    }
}
