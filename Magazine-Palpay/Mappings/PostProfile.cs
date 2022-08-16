using AutoMapper;
using Magazine_Palpay.Web.ViewModels;

namespace Magazine_Palpay.Mappings
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostViewModel, Data.Models.Post>().ReverseMap();
        }
    }
}
