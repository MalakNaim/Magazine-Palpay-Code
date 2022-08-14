// --------------------------------------------------------------------------------------------------
// <copyright file="EvaluationQuestionsQueryHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Magazine_Palpay.Application.Wrapper;
using Magazine_Palpay.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Magazine_Palpay.Application.Features.Queries
{
    internal class MagazineLookupQueryHandler :
        IRequestHandler<GetMagazineLookupQuery, List<Data.Models.MagazineLookup>>,
        IRequestHandler<GetMagazineLookupByIdQuery,
             Result<Data.Models.MagazineLookup>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public MagazineLookupQueryHandler(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }

        public async Task<List<Data.Models.MagazineLookup>> Handle(GetMagazineLookupQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.MagazineLookup.ToList();
            return _mapper.Map<List<Data.Models.MagazineLookup>>(queryable);
        }

        public async Task<Result<Data.Models.MagazineLookup>> Handle(GetMagazineLookupByIdQuery query, CancellationToken cancellationToken)
        {
            var lookup = await _context.MagazineLookup.AsNoTracking()
                .Where(p => p.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);
            var mappedLookup = _mapper.Map<Data.Models.MagazineLookup>(lookup);
            return await Result<Data.Models.MagazineLookup>.SuccessAsync(mappedLookup);
        }
    }
}