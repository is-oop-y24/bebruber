using System;

namespace Bebruber.Domain.Tools;

public abstract class BebruberException : Exception
{
    protected BebruberException() { }

    protected BebruberException(string? message)
        : base(message) { }

    protected BebruberException(string? message, Exception? innerException)
        : base(message, innerException) { }
}