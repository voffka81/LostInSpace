namespace Assets.Scripts.Interfaces
{
    public interface IStat
    {
        string Name { get; }
    }
    public interface IStringStat
    {
        string Value { get; }

        void SetValue(string value);
    }

    public interface INumericStat
    {
        float MaxValue { get; }
        float Price { get; }
        float Quantity { get; }
        float Value { get; }

        bool deduct(float amount);
        void forceDeduct(float amount);
        void increase(float byAmount);
    }
}
