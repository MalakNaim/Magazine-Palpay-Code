using System.Threading.Tasks;

namespace Magazine_Palpay.Web.Services
{
    public interface IViewRenderService
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}