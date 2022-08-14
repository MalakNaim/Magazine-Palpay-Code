// --------------------------------------------------------------------------------------------------
// <copyright file="EvaluationAnswersProfile.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------
using AutoMapper;
using Magazine_Palpay.Application.Features.MagazineLookup.Commands;
using Magazine_Palpay.Application.ViewModels;
using Magazine_Palpay.Data.Models;

namespace Magazine_Palpay.Application.Mappings
{
    public class MagazineLookupProfile : Profile
    {
        public MagazineLookupProfile()
        {
            CreateMap<RegistrationMagazineLookupCommand, MagazineLookup>()
              .ReverseMap();
            CreateMap<UpdateMagazineLookupCommand, MagazineLookup>().ReverseMap();
            CreateMap<MagazineLookup, MagazineLookup>().ReverseMap();
            CreateMap<RegistrationMagazineLookupCommand, MagazineLookupVM>()
                .ReverseMap();
            CreateMap<UpdateMagazineLookupCommand, MagazineLookupVM>().ReverseMap();
            CreateMap<MagazineLookup, MagazineLookupVM>().ReverseMap();
        }
    }
}