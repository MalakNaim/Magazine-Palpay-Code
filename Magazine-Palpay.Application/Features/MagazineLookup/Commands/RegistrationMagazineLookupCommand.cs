using Magazine_Palpay.Application.Wrapper;
using MediatR;

namespace Magazine_Palpay.Application.Features.MagazineLookup.Commands
{
    public class RegistrationMagazineLookupCommand : IRequest<Result<int>>
    {
        public int LookupId { get; set; }

        public int? LookupChildId { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }
    }
}
