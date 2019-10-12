﻿using Patlus.Common.UseCase.Entities;
using System;

namespace Patlus.IdentityManagement.UseCase.Entities
{
    public class Pool : IStandardEntity
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid CreatorId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastModifiedTime { get; set; }

        public bool Archived { get; set; }
    }
}
