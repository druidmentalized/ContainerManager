using ContainerManager.Utils;

namespace ContainerManager.Containers;

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
        throw new NotImplementedException();
    }

    public override void LoadCargo(double cargoWeight)
    {
        throw new NotImplementedException();
    }
    
    public void notify()
    {
        
    }
}