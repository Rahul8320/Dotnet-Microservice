using Mango.Web.Models;

namespace Mango.Web.Service.Interface
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
