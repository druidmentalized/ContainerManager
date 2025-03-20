using ContainerManager.utils;

namespace ContainerManager.containers;

public class GasContainer : Container, IHazardNotifier
{
    private readonly double _pressure;
    
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

    public override string ToString()
    {
        return base.ToString() +
               $"Pressure: {_pressure}atm \n" +
               $"{notify()}\n";
    }
    
    public string notify()
    {
        return $"This gas container {serialNumber} is in hazardous situation!";
    }
}