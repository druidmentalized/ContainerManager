using ContainerManager.exceptions;
using ContainerManager.main;
using ContainerManager.utils;

namespace ContainerManager.containers;

public class RefrigeratedContainer : Container
{
    public double Temperature { get; set; }

    public RefrigeratedContainer(double height, double weight,
        double depth, double maximumCargoWeight, double temperature) :
        base("C", height, weight, depth, maximumCargoWeight, TypeEnum.REFRIGATED)
    {
        Temperature = temperature;
    }

    protected override bool CanLoad(double cargoWeight, Product? product)
    {
        if (product == null) return false;

        if (product.StoringTemperature < Temperature)
        {
            throw new TemperatureDiscrepancyException(
                $"Current temperature in the container({Temperature}) can't be lower than product temperature({product.StoringTemperature}).");
        }
        return base.CanLoad(cargoWeight, product);
    }

    public override string ToString()
    {
        return base.ToString() +
               $", Maintained temperature: {Temperature}°C\n";
    }

    public override string Notify()
    {
        return "";
    }
}