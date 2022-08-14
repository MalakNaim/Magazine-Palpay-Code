using Magazine_Palpay.Application.Wrapper;
using MediatR;

namespace Magazine_Palpay.Application.Features.MagazineLookup.Commands
{
    public class UpdateMagazineLookupCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public int LookupId { get; set; }

        public int? LookupChildId { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }
    }
}
