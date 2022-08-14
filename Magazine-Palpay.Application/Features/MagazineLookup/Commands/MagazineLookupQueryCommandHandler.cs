// --------------------------------------------------------------------------------------------------
// <copyright file="EvaluationAnswersQueryCommandHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Magazine_Palpay.Application.Features.MagazineLookup.Commands;
using Magazine_Palpay.Application.Wrapper;
using Magazine_Palpay.Data;
using Magazine_Palpay.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ma.Features.EvaluationAnswers.Commands
{
    internal class MagazineLookupQueryCommandHandler :
         IRequestHandler<RegistrationMagazineLookupCommand, Result<int>>,
         IRequestHandler<RemoveMagazineLookupCommand, Result<int>>,
         IRequestHandler<UpdateMagazineLookupCommand, Result<int>>
    {
          private readonly IMapper _mapper;
          private readonly ApplicationDbContext _context;

         public MagazineLookupQueryCommandHandler(IMapper mapper, ApplicationDbContext context)
         {
            _mapper = mapper;
            _context = context;
         }

         public async Task<Result<int>> Handle(RegistrationMagazineLookupCommand command, CancellationToken cancellationToken)
         {
            var magazineLookupMapped = _mapper.Map<MagazineLookup>(command);
            await _context.MagazineLookup.AddAsync(magazineLookupMapped, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync("تمت عملية الإضافة بنجاح");
         }

         public async Task<Result<int>> Handle(UpdateMagazineLookupCommand command, CancellationToken cancellationToken)
         {
            var lookup = await _context.MagazineLookup.Where(c => c.Id == command.Id)
                .AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (lookup != null)
            {
                var lookupMapped = _mapper.Map<MagazineLookup>(command);
                lookupMapped.LookupId = command.LookupId;
                lookupMapped.LookupChildId = command.LookupChildId;
                lookupMapped.Name = command.Name;
                lookupMapped.UpdatedAt = System.DateTime.Now;
                _context.MagazineLookup.Update(lookupMapped);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(lookupMapped.Id, "تمت عملية التعديل بنجاح");
            }
            else
            {
                return await Result<int>.FailAsync("هذا النوع غير موجود");
            }
         }

         public async Task<Result<int>> Handle(RemoveMagazineLookupCommand command, CancellationToken cancellationToken)
         {
            var lookup = await _context.MagazineLookup.FirstOrDefaultAsync(b => b.Id == command.Id, cancellationToken);
            if (lookup != null)
            {
                lookup.Deleted = true;
                _context.MagazineLookup.Update(lookup);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(lookup.Id, "تمت عملية الحذف بنجاح");
            }
            else
            {
                return await Result<int>.FailAsync("هذا النوع غير موجود");
            }
         }
    }
}