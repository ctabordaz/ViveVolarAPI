using AutoMapper;

namespace ViveVolar.WebApi.Mapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(mapper =>
                mapper.AddProfile<DefaultProfile>()
            );
        }
    }
}