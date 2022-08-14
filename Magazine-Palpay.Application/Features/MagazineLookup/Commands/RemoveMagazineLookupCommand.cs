using Magazine_Palpay.Application.Wrapper;
using MediatR;

namespace Magazine_Palpay.Application.Features.MagazineLookup.Commands
{
    public class RemoveMagazineLookupCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public bool Deleted { get; set; }
    }
}
