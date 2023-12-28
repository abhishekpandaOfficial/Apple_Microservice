using Apple.Web.Models;
using Apple.Web.Service.IService;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using static Apple.Web.Utility.SD;


namespace Apple.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;

        }
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {

            HttpClient client = _httpClientFactory.CreateClient("AppleAPI");
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");

            //token  implementation goes here

            //---------------------
            message.RequestUri = new Uri(requestDto.Url);
            if(requestDto.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage? apiResponse = null;

            // checking the method type (GET / POST/ PUT / DELETE from the request types)

            switch(requestDto.ApiType)
            {
                case ApiTypeEnum.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiTypeEnum.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case ApiTypeEnum.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;

                default:
                    message.Method = HttpMethod.Get;
                    break;

            }

            apiResponse = await client.SendAsync(message);
            switch(apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new() { IsSuccess = false, Message="Not Found" };

                case HttpStatusCode.Forbidden:
                    return new() { IsSuccess = false, Message = "Access Denied" };

                case HttpStatusCode.Unauthorized:
                    return new() { IsSuccess = false, Message = "Un-Authorized" };

                case HttpStatusCode.InternalServerError:
                    return new() { IsSuccess = false, Message = "Internal Server Error" };

                default:
                    var apicontent = await apiResponse.Content.ReadAsStringAsync();
                    var apiresonseDto = JsonConvert.DeserializeObject<ResponseDto>(apicontent);
                        return apiresonseDto;
                 }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message.ToString(),
                };
                return dto;
            }
        }
    }
}
