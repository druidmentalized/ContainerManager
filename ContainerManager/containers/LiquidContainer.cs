using ContainerManager.exceptions;
using ContainerManager.main;
using ContainerManager.utils;

namespace ContainerManager.containers;

public class LiquidContainer : Container
{
    public bool IsHazardous { get; set; }

    public LiquidContainer(double height, double weight,
        double depth, double maximumCargoWeight, bool isHazardous) :
        base("L", height, weight, depth, maximumCargoWeight, TypeEnum.LIQUID)
    {
        IsHazardous = isHazardous;
    }
    
    protected override bool CanLoad(double cargoWeight, Product? product)
    {
        if (product == null) return false;
        
        if (product.IsHazardous != IsHazardous)
        {
            throw new LoadingWrongProductException("The product and container toxicities doesn't match.");
        }
        
        base.CanLoad(cargoWeight, product);
        
        var currentLimit = !IsHazardous ? MaximumCargoWeight * 0.9 : MaximumCargoWeight * 0.5;
        if (CargoWeight + cargoWeight > currentLimit)
        {
            throw new OverfillException($"Cannot load {cargoWeight}kg. Exceeds {currentLimit}kg limit.");
        }
        return true;
    }

    public override string ToString()
    {
        return base.ToString() +
               $", Hazardous: {IsHazardous}\n";
    }
    
    public override string Notify()
    {
        return $"This liquid container {SerialNumber} is hazardous!";
    }
}