﻿using AutoMapper;
using DowntimeAlerter.Domain.Entities;
using DowntimeAlerter.MVC.UI.Models.TargetApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DowntimeAlerter.MVC.UI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TargetAppViewModel, TargetApp>();
            CreateMap<TargetApp, TargetAppViewModel>();
            CreateMap<TargetAppCreateViewModel, TargetApp>();
            CreateMap<TargetApp, TargetAppCreateViewModel>();
            CreateMap<TargetAppEditViewModel, TargetApp>();
            CreateMap<TargetApp, TargetAppEditViewModel>();
        }
    }
}
