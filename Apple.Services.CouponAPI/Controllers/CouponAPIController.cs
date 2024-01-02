using Apple.Services.CouponAPI.Data;
using Apple.Services.CouponAPI.Models;
using Apple.Services.CouponAPI.Models.Dto;
using AutoMapper;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Apple.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _dbcontext;
        private ResponseDto _response;
        private IMapper _mapper;
        private ILogger<Coupon> _logger;
        public CouponAPIController(AppDbContext dbcontext, IMapper mapper, ILogger<Coupon> logger)
        {
            _logger = logger;
            _dbcontext = dbcontext;
            _response = new ResponseDto();

            // Using Dependency Injection we will inject Automapper 
            _mapper = mapper;
           

        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                _logger.LogInformation("Coupon Controller Get method- started at {date}", DateTime.UtcNow);
                IEnumerable<Coupon?>? objList;
                if (_dbcontext != null)
                {
                    objList = _dbcontext?.Coupons.ToList();
                    _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
                    _logger.LogInformation("Coupon Controller Get method executed - end at {date}", DateTime.UtcNow);
                }
                else
                {
                    // objList is null, handle accordingly
                    _logger.LogWarning("The Coupons DbSet is null.");
                    // Additional handling or logging if needed
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Coupon Controller Get method failed at {date}", DateTime.UtcNow);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(ex.Message, DateTime.UtcNow);
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                _logger.LogInformation("Coupon Controller Get with " + id + "method- started at {date}", DateTime.UtcNow);
                Coupon obj = _dbcontext.Coupons.First(u => u.CouponId == id);

                // Below line will automatically convert the Normal Coupon class Object to DTO object 
                _response.Result = _mapper.Map<CouponDto>(obj);
                _logger.LogInformation("Coupon Controller Get with " + id + "method- ended at {date}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Coupon Controller Get with ID"  + id + " Method failed at {date}", DateTime.UtcNow);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(ex.Message, DateTime.UtcNow);
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                _logger.LogInformation("Coupon Controller Get with " + code + "method- started at {date}", DateTime.UtcNow);
                Coupon obj = _dbcontext.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == code.ToLower());
                if (obj == null)
                {
                    _response.IsSuccess = false;
                }
                // Below line will automatically convert the Normal Coupon class Object to DTO object 
                _response.Result = _mapper.Map<CouponDto>(obj);
                _logger.LogInformation("Coupon Controller Get with " + code + "method- ended at {date}", DateTime.UtcNow);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Coupon Controller Get by Code with " + code + "Method failed at {date}", DateTime.UtcNow);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(ex.Message, DateTime.UtcNow);
            }
            return _response;
        }


        [HttpPost]
        public async Task<ResponseDto> post([FromBody] CouponDto couponDto)
        {
            try
            {
                _logger.LogInformation("Coupon Controller Post method - started at {date}", DateTime.UtcNow);
                var  objCoupon = _mapper.Map<Coupon>(couponDto);
                _dbcontext.Coupons.Add(objCoupon);
                await _dbcontext.SaveChangesAsync();
                // Below line will automatically convert the Normal Coupon class Object to DTO object 
                _response.Result = _mapper.Map<CouponDto>(objCoupon);
                _logger.LogInformation("Coupon Controller Post method - ended at {date}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Coupon Controller Post method failed at {date}", DateTime.UtcNow);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(ex.Message, DateTime.UtcNow);
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto put([FromBody] CouponDto couponDto)
        {
            try
            {
                _logger.LogInformation("Coupon Controller update method - started at {date}", DateTime.UtcNow);
                Coupon objCoupon = _mapper.Map<Coupon>(couponDto);
                objCoupon.CreatedDateTime = DateTime.Now;
                _dbcontext.Coupons.Update(objCoupon);
                _dbcontext.SaveChanges();
                // Below line will automatically convert the Normal Coupon class Object to DTO object 
                _response.Result = _mapper.Map<CouponDto>(objCoupon);
                _logger.LogInformation("Coupon Controller update method - ended at {date}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Coupon Controller update with " + couponDto.CouponCode + " failed at {date}", DateTime.UtcNow);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(ex.Message, DateTime.UtcNow);
            }
            return _response;
        }


        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                _logger.LogInformation("Coupon Controller Delete method with ID " + id + "- started at {date}", DateTime.UtcNow);
                Coupon objCoupon = _dbcontext.Coupons.First(u=>u.CouponId == id);

                _dbcontext.Coupons.Remove(objCoupon);
                _dbcontext.SaveChanges();
                _logger.LogInformation("Coupon Controller Delete method with ID " + id + "- ended at {date}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Coupon Controller update with ID " + id + " failed at {date}", DateTime.UtcNow);
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _logger.LogInformation(ex.Message, DateTime.UtcNow);
            }
            return _response;
        }
    }
}
