using ContainerManager.Utils;

namespace ContainerManager.Containers;

public class RefrigeratedContainer : Container
{
    private readonly ProductType _productType;

    public RefrigeratedContainer(double height, double tareWeight,
        double depth, double maximumPayload, ProductType productType) :
        base("C", height, tareWeight, depth, maximumPayload)
    {
        _productType = productType;
    }
    
    public override void EmptyCargo()
    {
        throw new NotImplementedException();
    }

    public override void LoadCargo(double cargoWeight)
    {
        throw new NotImplementedException();
    }
}