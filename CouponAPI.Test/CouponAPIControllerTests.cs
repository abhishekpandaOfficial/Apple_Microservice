using Apple.Services.CouponAPI.Controllers;
using Apple.Services.CouponAPI.Data;
using Apple.Services.CouponAPI.Models;
using Apple.Services.CouponAPI.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
            
namespace CouponAPI.Test
{
    
    public class CouponAPIControllerTests 
    {
        [Fact]
        public void Get_Returns_ResponseDto()
        {
            // Arrange
            var dbContextMock = new Mock<AppDbContext>(); // Replace with your actual DbContext type
            var mapperMock = new Mock<IMapper>(); // Replace with your actual AutoMapper type
            var loggerMock = new Mock<ILogger<Coupon>>();

            var couponController = new CouponAPIController(dbContextMock.Object, mapperMock.Object, loggerMock.Object);


            // For the given code, let's assume a successful case with some mock data
            var mockCouponData = new List<Coupon> { new Coupon { 
             CouponId = 1,
              CouponCode = "10-OFF",
               DiscountAmount = 10,
               MinAmount = 100
            } };
            dbContextMock.Setup(c => c.Coupons).ReturnsDbSet(mockCouponData);

            // Replace the following line with the actual initialization of ResponseDto based on your code
            var expectedResponse = new ResponseDto
            {
                IsSuccess = true,
                Message = "Unit Test Success",
                Result = mockCouponData
            };

            // Act
            var result = couponController.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.IsSuccess, result.IsSuccess);
            Assert.Equal(expectedResponse.Message, result.Message);
            // Add more assertions based on your specific ResponseDto structure

            // Additional assertions can be added to check specific behavior based on your application logic
        }


    }
}
