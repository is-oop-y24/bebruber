﻿using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Enumerations;

public class CarCategory : Enumeration<int, CarCategory>
{
    protected CarCategory(string name, int value)
        : base(name, value)
    { }

    public static CarCategory Economy { get; } = new CarCategory(nameof(Economy), 1);
    public static CarCategory Comfort { get; } = new CarCategory(nameof(Comfort), 2);
    public static CarCategory Business { get; } = new CarCategory(nameof(Business), 3);
}