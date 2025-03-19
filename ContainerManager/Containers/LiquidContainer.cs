using ContainerManager.Utils;

namespace ContainerManager.Containers;

public class LiquidContainer : Container, IHazardNotifier
{
    public LiquidContainer(double height, double tareWeight,
        double depth, double maximumPayload) :
        base("L", height, tareWeight, depth, maximumPayload) { }
    
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