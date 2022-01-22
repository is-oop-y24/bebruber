using Bebruber.Domain.Entities;
using Bebruber.Domain.Tools;

namespace Bebruber.Application.Services.Exceptions;

public class RideEntryEnqueuedException : BebruberException
{
    public RideEntryEnqueuedException(RideEntry entry)
        : base($"{nameof(RideEntry)} {entry} already enqueued") { }
}