using Assets.Scripts.Interfaces;

public class NumericStat : INumericStat
{
    public string Name { get; private set; }
    public float Value { get; private set; }
    public float Price { get; private set; }
    public float Quantity { get; private set; }
    public float MaxValue { get; private set; }

    public NumericStat(string name, float startValue, float maxValue)
    {
        Name = name;
        Value = startValue;
        MaxValue = maxValue;
    }

    public NumericStat(string name, float price, float quantity, float maxValue)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        MaxValue = maxValue;
    }

    public void increase(float byAmount)
    {
        if (Value < MaxValue)
        {
            Value += byAmount;
        }
    }

    public bool deduct(float amount)
    {
        if (Value >= amount)
        {
            Value -= amount;
            return true;
        }
        return false;
    }

    public void forceDeduct(float amount)
    {
        Value -= amount;
    }

}
