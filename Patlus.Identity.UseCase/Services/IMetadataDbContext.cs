﻿using Patlus.Identity.UseCase.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Patlus.Identity.UseCase.Services
{
    public interface IMetadataDbContext
    {
        IQueryable<Pool> Pools { get; }

        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}