using Apple.Services.CouponAPI.Data;
using Apple.Services.CouponAPI.Models;
using Apple.Services.CouponAPI.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Apple.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _dbcotext;
        private ResponseDto _response;
        private IMapper _mapper;
        public CouponAPIController(AppDbContext dbcontext, IMapper mapper)
        {
            _dbcotext = dbcontext;
            _response = new ResponseDto();

            // Using Dependency Injection we will inject Automapper 
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _dbcotext.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon obj = _dbcotext.Coupons.First(u => u.CouponId == id);

                // Below line will automatically convert the Normal Coupon class Object to DTO object 
                _response.Result = _mapper.Map<CouponDto>(obj);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon obj = _dbcotext.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == code.ToLower());
                if (obj == null)
                {
                    _response.IsSuccess = false;
                }
                // Below line will automatically convert the Normal Coupon class Object to DTO object 
                _response.Result = _mapper.Map<CouponDto>(obj);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost]
        public ResponseDto post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon objCoupon = _mapper.Map<Coupon>(couponDto);
                _dbcotext.Coupons.Add(objCoupon);
                _dbcotext.SaveChanges();
                // Below line will automatically convert the Normal Coupon class Object to DTO object 
                _response.Result = _mapper.Map<CouponDto>(objCoupon);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon objCoupon = _mapper.Map<Coupon>(couponDto);
                objCoupon.CreatedDateTime = DateTime.Now;
                _dbcotext.Coupons.Update(objCoupon);
                _dbcotext.SaveChanges();
                // Below line will automatically convert the Normal Coupon class Object to DTO object 
                _response.Result = _mapper.Map<CouponDto>(objCoupon);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon objCoupon = _dbcotext.Coupons.First(u=>u.CouponId == id);
                
                _dbcotext.Coupons.Remove(objCoupon);
                _dbcotext.SaveChanges();

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
