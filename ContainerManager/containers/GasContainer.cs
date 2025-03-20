using ContainerManager.utils;

namespace ContainerManager.containers;

public class GasContainer : Container, IHazardNotifier
{
    private double _pressure;
    
    public GasContainer(double height, double tareWeight,
        double depth, double maximumPayload, double pressure) :
        base("G", height, tareWeight, depth, maximumPayload)
    {
        _pressure = pressure;
    }
    
    public override void EmptyCargo()
    {
        cargoWeight *= 0.05;
    }
    
    public void notify()
    {
        Console.WriteLine($"Gas container {serialNumber} is in hazardous situation!");
    }
}