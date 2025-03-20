using ContainerManager.exceptions;
using ContainerManager.utils;

namespace ContainerManager.containers;

public class LiquidContainer : Container, IHazardNotifier
{
    private readonly bool _hazardous;

    public LiquidContainer(double height, double tareWeight,
        double depth, double maximumPayload, bool hazardous) :
        base("L", height, tareWeight, depth, maximumPayload)
    {
        _hazardous = hazardous;
    }
    
    protected override bool CanLoad(double newCargoMass)
    {
        var currentLimit = !_hazardous ? maximumPayload * 0.9 : maximumPayload * 0.5;
        if (cargoWeight + newCargoMass > currentLimit)
        {
            throw new OverfillException($"Cannot load {newCargoMass}kg. Exceeds {currentLimit}kg limit.");
        }
        return true;
    }

    public override string ToString()
    {
        return base.ToString() +
               $"Hazardous: {_hazardous}\n";
    }
    
    public void notify()
    {
        Console.WriteLine($"Liquid container {serialNumber} is in hazardous situation!");
    }
}