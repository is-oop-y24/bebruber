namespace Bebruber.Utility.Tools;

public class TypeLocator
{
    private readonly Dictionary<string, Type> _dictionary;

    public TypeLocator()
    {
        _dictionary = new Dictionary<string, Type>();
    }

    public string GetKey(Type type)
        => type.Name;

    public TypeLocator RegisterType(Type type)
    {
        _dictionary[GetKey(type)] = type;
        return this;
    }

    public Type Resolve(string key)
        => _dictionary[key];
}