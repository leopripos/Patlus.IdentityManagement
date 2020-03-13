﻿using System;

namespace Patlus.IdentityManagement.UseCase.Entities
{
    public class Token
    {
        public Guid Id { get; set; }
        public string Scheme { get; set; } = null!;
        public string Access { get; set; } = null!;
        public string Refresh { get; set; } = null!;
        public DateTimeOffset CreatedTime { get; set; }
    }
}
