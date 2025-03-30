using AutoMapper;

namespace RapidPay.Application.AutoMapper
{
    public class AutoMapperSetup
    {
        public static MapperConfiguration RegisterMapping()
        {
            return new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new ViewModelToDomainMappingProfile());
                configuration.AddProfile(new DomainToViewModelMappingProfile());
            });
        }
    }
}
