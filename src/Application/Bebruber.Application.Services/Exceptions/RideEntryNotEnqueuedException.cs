using Bebruber.Domain.Entities;
using Bebruber.Domain.Tools;

namespace Bebruber.Application.Services.Exceptions;

public class RideEntryNotEnqueuedException : BebruberException
{
    public RideEntryNotEnqueuedException(Guid id)
        : base($"{nameof(RideEntry)} with id {id} is not enqueued") { }
}