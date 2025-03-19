namespace ContainerManager.Containers;

public abstract class Container
{
    internal static int _containerCounter = 0;
    
    private double _mass;
    private double _cargoWeight;
    private readonly double _height;
    private readonly double _tareWeight;
    private readonly double _depth;
    private readonly double _maximumPayload;
    private readonly string _serialNumber;

    public Container(string containerType, double height, double tareWeight,
        double depth, double maximumPayload)
    {
        _serialNumber = GenerateSerialNumber(containerType);
        _height = height;
        _tareWeight = tareWeight;
        _depth = depth;
        _maximumPayload = maximumPayload;
    }

    private static string GenerateSerialNumber(string containerType)
    {
        return $"KON-{containerType}-{++_containerCounter}";
    }

    public abstract void EmptyCargo();
    public abstract void LoadCargo(double cargoWeight);
}