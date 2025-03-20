using ContainerManager.utils;

namespace ContainerManager.containers;

public class RefrigeratedContainer : Container
{
    private static readonly Dictionary<string, double> _productTemperatureMap = new()
    {
        { "Bananas", 13.3 },
        { "Chocolate", 18.0 },
        { "Fish", 2.0 },
        { "Meat", -15.0 },
        { "Ice cream", -18.0 },
        { "Frozen pizza", -30.0 },
        { "Cheese", 7.2 },
        { "Sausages", 5.0 },
        { "Butter", 20.5 },
        { "Eggs", 19.0 }
    };
    private readonly string _productType;
    private readonly double _temperature;

    public RefrigeratedContainer(double height, double tareWeight,
        double depth, double maximumPayload, string productType, double temperature) :
        base("C", height, tareWeight, depth, maximumPayload)
    {
        _productType = productType;
        _temperature = temperature;
    }

    public sealed override void LoadCargo(double newCargoWeight)
    {
        throw new InvalidOperationException("Use LoadCargo(double newCargoWeight, string productType) instead.");
    }
    public void LoadCargo(double newCargoWeight, string productType)
    {
        if (productType != _productType)
        {
            throw new WrongProductTypeException($"You can load only products of type: {_productType}.");
        }
        base.LoadCargo(newCargoWeight);
    }

    public override string ToString()
    {
        return base.ToString() +
               $"ProductType: {_productType}\n" +
               $"Maintained temperature: {_temperature}\n";
    }
}