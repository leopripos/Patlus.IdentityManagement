﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patlus.Common.Rest.Authentication;
using Patlus.IdentityManagement.Presentation.Auhtorization.Policies;
using Patlus.IdentityManagement.UseCase.Entities;
using Patlus.IdentityManagement.UseCase.Features.Pools.Create;
using Patlus.IdentityManagement.UseCase.Features.Pools.GetAll;
using Patlus.IdentityManagement.UseCase.Features.Pools.GetOne;
using Patlus.IdentityManagement.UseCase.Features.Pools.Update;
using Patlus.IdentityManagement.UseCase.Features.Pools.UpdateActiveStatus;
using System;
using System.Threading.Tasks;

namespace Patlus.IdentityManagement.Rest.Features.Pools
{
    [ApiController]
    [Route("pools")]
    [Authorize]
    public class PoolsController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserResolver _userResolver;

        public PoolsController(IMediator mediator, IMapper mapper, IUserResolver userResolver)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userResolver = userResolver;
        }

        /// <summary>
        /// Get the list of Pool
        /// </summary>
        /// <returns>Array of pool</returns>
        [HttpGet]
        [Authorize(Policy = PoolPolicy.Read)]
        public async Task<PoolDto[]> GetAll()
        {
            var pools = await _mediator.Send(new GetAllQuery()
            {
                RequestorId = _userResolver.Current.Id
            });

            return _mapper.Map<Pool[], PoolDto[]>(pools);
        }

        /// <summary>
        /// Get pool with specific id
        /// </summary>
        /// <param name="poolId">Pool Id</param>
        /// <returns>Pool</returns>
        [HttpGet("{poolId}")]
        [Authorize(Policy = PoolPolicy.Read)]
        public async Task<ActionResult<PoolDto>> GetById(Guid poolId)
        {
            var pool = await _mediator.Send(new GetOneQuery()
            {
                Condition = (e => e.Id == poolId),
                RequestorId = _userResolver.Current.Id
            });

            if (pool == null)
            {
                return NotFound();
            }

            return _mapper.Map<Pool, PoolDto>(pool);
        }

        /// <summary>
        /// Create new Pool
        /// </summary>
        /// <param name="form">New Pool Form</param>
        /// <returns>Created Pool</returns>
        [HttpPost]
        [Authorize(Policy = PoolPolicy.Create)]
        public async Task<ActionResult<PoolDto>> Create([FromBody] CreateForm form)
        {
            var command = new CreateCommand
            {
                Active = form.Active,
                Name = form.Name,
                Description = form.Description,
                RequestorId = _userResolver.Current.Id
            };

            var pool = await _mediator.Send(command);

            return Created(new Uri($"{Request.Path}/{pool.Id}", UriKind.Relative), _mapper.Map<PoolDto>(pool));
        }

        /// <summary>
        /// Update the detail of specific pool.
        /// Updating pool using partial update, so you have to update which field you want to update.
        /// No need to pass all value
        /// </summary>
        /// <param name="poolId">Pool Id</param>
        /// <param name="form">Updated Pool Form</param>
        /// <returns>Pool Updated</returns>
        [HttpPatch("{poolId}")]
        [Authorize(Policy = PoolPolicy.Update)]
        public async Task<ActionResult<PoolDto>> Update(Guid poolId, [FromBody] UpdateForm form)
        {
            var command = new UpdateCommand()
            {
                Id = poolId,
                RequestorId = _userResolver.Current.Id
            };

            if (form.HasName)
            {
                command.Name = form.Name;
            }

            if (form.HasDescription)
            {
                command.Description = form.Description;
            }

            var pool = await _mediator.Send(command);

            return Ok(_mapper.Map<PoolDto>(pool));
        }

        /// <summary>
        /// Update active status of specific pool
        /// </summary>
        /// <param name="poolId">Pool Id</param>
        /// <param name="form">Update Form</param>
        /// <returns>Pool Updated</returns>
        [HttpPut("{poolId}/active")]
        [Authorize(Policy = PoolPolicy.UpdateActiveStatus)]
        public async Task<PoolDto> UpdateActiveStatus(Guid poolId, [FromBody] UpdateActiveStatusForm form)
        {
            var command = new UpdateActiveStatusCommand
            {
                Id = poolId,
                Active = form.Active,
                RequestorId = _userResolver.Current.Id
            };

            var pool = await _mediator.Send(command);

            return _mapper.Map<Pool, PoolDto>(pool);
        }
    }
}
