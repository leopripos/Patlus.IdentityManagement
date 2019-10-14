﻿using Patlus.Common.UseCase;
using System;

namespace Patlus.IdentityManagement.Rest.Features.Pools
{
    public class PoolDto : IDto
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}