﻿namespace Patlus.IdentityManagement.Rest.Features.Pools
{
    public class CreateForm
    {
        public bool? Active { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
