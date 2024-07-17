using Assets.Scripts.Interfaces;

public class StringStat : IStringStat
{
    public string Name { get; private set; }
    public string Value { get; private set; }

    public StringStat(string name, string startValue)
    {
        Name = name;
        Value = startValue;
    }

    public void SetValue(string value)
    {
        Value = value;
    }
}
