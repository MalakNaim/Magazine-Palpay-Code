using Magazine_Palpay.Application.Wrapper;
using MediatR;

namespace Magazine_Palpay.Application.Features.Queries
{
    public class GetMagazineLookupByIdQuery : IRequest<Result<Data.Models.MagazineLookup>>
    {
        public int Id { get; set; }
    }
}