using System;

namespace Bebruber.Domain.Services;

public interface ITimeProviderService
{
    DateTime GetCurrentDateTime();
}