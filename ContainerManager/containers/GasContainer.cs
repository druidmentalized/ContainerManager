using ContainerManager.utils;

namespace ContainerManager.containers;

public class GasContainer : Container
{
    public double Pressure { get; }

    public GasContainer(double height, double weight,
        double depth, double maximumCargoWeight, double pressure) :
        base("G", height, weight, depth, maximumCargoWeight, TypeEnum.GAS)
    {
        Pressure = pressure;
    }
    
    public override void EmptyCargo()
    {
        CargoWeight *= 0.05;
    }
    
    public override string ToString()
    {
        return base.ToString() +
               $", Pressure: {Pressure}atm \n";
    }
    
    public override string Notify()
    {
        return $"This gas container {SerialNumber} is hazardous!";
    }
}