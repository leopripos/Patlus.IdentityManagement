﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patlus.IdentityManagement.UseCase.Entities;

namespace Patlus.IdentityManagement.Persistence.Configurations
{
    public class HostedAccountConfiguration : IEntityTypeConfiguration<HostedAccount>
    {
        public void Configure(EntityTypeBuilder<HostedAccount> builder)
        {
            builder.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.Password)
                .IsRequired();

            builder.Property(e => e.CreatorId)
                .IsRequired();

            builder.Property(e => e.CreatedTime)
                .IsRequired();

            builder.Property(e => e.LastModifiedTime)
                .IsRequired();

            builder.Property(e => e.Archived)
                .IsRequired();

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Name).IsUnique();

            builder.HasOne(e => e.Identity)
                .WithOne(account => account.HostedAccount!)
                .HasPrincipalKey<HostedAccount>(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
