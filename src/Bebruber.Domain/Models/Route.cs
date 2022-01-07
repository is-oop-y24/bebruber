using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Models;

public class Route : IReadOnlyCollection<RouteSector>
{
    private readonly IReadOnlyCollection<RouteSector> _sectors;

    public Route(IReadOnlyCollection<RouteSector> sectors)
    {
        _sectors = sectors.ThrowIfNull().ToList();
    }

    public int Count => _sectors.Count;

    public IEnumerator<RouteSector> GetEnumerator()
    {
        return _sectors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}