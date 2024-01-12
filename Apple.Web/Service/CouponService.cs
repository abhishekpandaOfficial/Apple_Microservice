using Apple.Web.Models;
using Apple.Web.Service.IService;
using Apple.Web.Utility;
using static System.Net.WebRequestMethods;

namespace Apple.Web.Service
{
    public class CouponService : ICouponService
    {
        // use dependency injection to inject the base service
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {

            _baseService = baseService; 

        }

        public async Task<ResponseDto?> GetAllCouponAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiTypeEnum.GET,
                 Url = SD.CouponAPIBase + "/api/v1/coupon"
            });
        }
        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
				ApiType = SD.ApiTypeEnum.GET,
                Url = SD.CouponAPIBase+ "/api/v1/coupon/GetbyId/"+id
				//Url = "https://localhost:7001/api/v1/coupon/GetbyId/" + id
			});
        }
        public async Task<ResponseDto?> GetCouponByCodeAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiTypeEnum.GET,
                Url = SD.CouponAPIBase + "/api/v1/coupon/GetByCode/" + couponCode
            });
        }
        public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiTypeEnum.POST,
                 Data = couponDto,
                Url = SD.CouponAPIBase + "/api/v1/coupon"
            });
        }
        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiTypeEnum.PUT,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/v1/coupon"
            });
        }
        public async Task<ResponseDto?> DeleteCouponAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiTypeEnum.DELETE,
                Url = SD.CouponAPIBase + "/api/v1/coupon/"+couponId
               // Url = "https://localhost:7001/api/v1/coupon/" + couponId
			});
        }

       

       

       

       
    }
}
