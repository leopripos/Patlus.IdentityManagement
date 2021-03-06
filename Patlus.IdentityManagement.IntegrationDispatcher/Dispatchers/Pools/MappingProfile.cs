﻿using AutoMapper;
using Patlus.Common.Presentation.Responses.Notifications;
using Patlus.Common.UseCase.Notifications;
using Patlus.IdentityManagement.UseCase.Features.Pools.Create;
using Patlus.IdentityManagement.UseCase.Features.Pools.Update;
using Patlus.IdentityManagement.UseCase.Features.Pools.UpdateActiveStatus;

namespace Patlus.IdentityManagement.IntegrationDispatcher.Dispatchers.Pools
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ActiveStatusUpdatedNotification, ActiveStatusUpdatedNotificationDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(e => e.Entity.Id));

            CreateMap<CreatedNotification, CreatedNotificationDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(e => e.Entity.Id));

            CreateMap<DeltaValue, DeltaValueDto>();
            CreateMap<UpdatedNotification, UpdatedNotificationDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(e => e.Entity.Id));
        }
    }
}
