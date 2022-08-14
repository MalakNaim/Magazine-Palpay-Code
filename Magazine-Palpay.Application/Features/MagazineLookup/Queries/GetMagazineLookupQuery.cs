using System.Collections.Generic;
using MediatR;

namespace Magazine_Palpay.Application.Features.Queries
{
    public class GetMagazineLookupQuery : IRequest<List<Data.Models.MagazineLookup>>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string[] OrderBy { get; set; }
    }
}