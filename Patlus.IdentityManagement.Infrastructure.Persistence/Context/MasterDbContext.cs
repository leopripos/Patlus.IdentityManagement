﻿using Microsoft.EntityFrameworkCore;
using Patlus.Common.UseCase.Services;
using Patlus.IdentityManagement.Persistence.Configurations;
using Patlus.IdentityManagement.UseCase.Entities;
using Patlus.IdentityManagement.UseCase.Services;
using System;
using System.Linq;

namespace Patlus.IdentityManagement.Persistence.Contexts
{
    public sealed class MasterDbContext : DbContext, IMasterDbContext
    {
        private readonly IIdentifierService _identifierService;
        private readonly ITimeService _timeService;
        private readonly IPasswordService _passwordService;

        public MasterDbContext(DbContextOptions<MasterDbContext> options, IIdentifierService identifierService, ITimeService timeService, IPasswordService passwordService)
            : base(options)
        {
            _identifierService = identifierService;
            _timeService = timeService;
            _passwordService = passwordService;
        }

        public IQueryable<Pool> Pools
        {
            get { return Set<Pool>(); }
        }

        public IQueryable<Identity> Identities
        {
            get { return Set<Identity>(); }
        }

        public IQueryable<HostedAccount> HostedAccounts
        {
            get { return Set<HostedAccount>(); }
        }

        void IMasterDbContext.Add<TEntity>(TEntity entity)
        {
            Set<TEntity>().Add(entity);
        }

        void IMasterDbContext.Update<TEntity>(TEntity entity)
        {
            Set<TEntity>().Update(entity);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PoolConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityConfiguration());
            modelBuilder.ApplyConfiguration(new HostedAccountConfiguration());

            var identityId = new Guid("90fdc79d-b97a-4b62-9c04-5b2f94df2026");
            var poolId = new Guid("c73d72b1-326d-4213-ab11-ba47d83b9ccf");
            var identityName = "root";
            var identityPassword = "root";

            modelBuilder.Entity<Pool>().HasData(new Pool[] {
                new Pool() {
                    Id = poolId,
                    Active = true,
                    Name = "Root Administrator",
                    Description = "Default identity pool for system administrator.",
                    CreatorId = identityId,
                    CreatedTime = _timeService.Now,
                    LastModifiedTime = _timeService.Now,
                }
            });

            modelBuilder.Entity<Identity>().HasData(new Identity[] {
                new Identity() {
                    Id = identityId,
                    AuthKey = _identifierService.NewGuid(),
                    PoolId = poolId,
                    Name = identityName,
                    Active = true,
                    CreatorId = identityId,
                    CreatedTime = _timeService.Now,
                    LastModifiedTime = _timeService.Now,
                }
            });

            modelBuilder.Entity<HostedAccount>().HasData(new HostedAccount[] {
                new HostedAccount(){
                    Id = identityId,
                    Name = identityName,
                    Password = _passwordService.GeneratePasswordHash(identityPassword),
                    CreatorId = identityId,
                    CreatedTime = _timeService.Now,
                    LastModifiedTime = _timeService.Now,
                }
            });
        }
    }
}
