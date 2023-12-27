using Apple.Web.Models;
using Apple.Web.Service.IService;
using Newtonsoft.Json;
using System.Text;


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
            HttpClient client = _httpClientFactory.CreateClient("AppleAPI");
            HttpRequestMessage message = null;
            message.Headers.Add("Accept", "application/json");

            //token  implementation goes here

            //---------------------
            message.RequestUri = new Uri(requestDto.Url);
            if(requestDto.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage? apiResponse = null;

        }
    }
}
