using MediatR;
using Microservice.BuildingBlocks.CQRS.Commands;
using Microservice.BuildingBlocks.CQRS.Extensions;
using Microservice.BuildingBlocks.CQRS.Requests;
using Microservice.BuildingBlocks.Domain.AggregateRoots;
using Microservice.BuildingBlocks.Domain.BusinessRules;
using Microservice.BuildingBlocks.Domain.Entities;
using Microservice.BuildingBlocks.Domain.Enumerations;
using Microservice.BuildingBlocks.Domain.Events;
using Microservice.BuildingBlocks.Domain.Repositories;
using Microservice.BuildingBlocks.Domain.UnitOfWorks;
using Microservice.BuildingBlocks.Domain.ValueObjects;
using System.Diagnostics.Tracing;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace Microservice.BuildingBlocks.CQRS.Queries;

/// <summary>
/// Represents Query functionality in CQRS architecture approach.
/// <para><see href="https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs">Read more about CQRS</see>.</para>
/// </summary>
/// <typeparam name="TResult">Type of the object, that will be returned as the command execution result.</typeparam>
public interface IQuery<out TResult> : IRequest<TResult>;
