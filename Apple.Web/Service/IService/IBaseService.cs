using Apple.Web.Models;

namespace Apple.Web.Service.IService
{
    public interface IBaseService
    {
       Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
