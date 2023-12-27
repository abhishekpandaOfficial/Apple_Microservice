using Apple.Services.CouponAPI.Models;
using Apple.Services.CouponAPI.Models.Dto;
using AutoMapper;

namespace Apple.Services.CouponAPI
{
    // This class is useful for converting Norrmal class to DTO and Vice Versa. 
    // Automapper creation
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>();
                config.CreateMap<Coupon,CouponDto>();
            });

            return mappingConfig;
        }
    }
}
