using ContainerManager.exceptions;

namespace ContainerManager.containers;

public abstract class Container
{
    private static int _containerCounter;
    
    private readonly double _height;
    private readonly double _tareWeight;
    private readonly double _depth;
    
    public readonly string serialNumber;
    
    public double maximumPayload { get; }
    public double cargoWeight { get; set; }
    public double totalMass => cargoWeight + _tareWeight;

    protected Container(string containerType, double height, double tareWeight,
        double depth, double maximumPayload)
    {
        serialNumber = GenerateSerialNumber(containerType);
        _height = height;
        _tareWeight = tareWeight;
        _depth = depth;
        this.maximumPayload = maximumPayload;
    }

    private static string GenerateSerialNumber(string containerType)
    {
        return $"KON-{containerType}-{++_containerCounter}";
    }

    public virtual void EmptyCargo()
    {
        cargoWeight = 0;
    }

    public virtual void LoadCargo(double newCargoWeight)
    {
        if (CanLoad(newCargoWeight))
        {
            cargoWeight += newCargoWeight;
        }
    }
    
    protected virtual bool CanLoad(double newCargoMass)
    {
        if (cargoWeight + newCargoMass > maximumPayload)
        {
            throw new OverfillException($"Cannot load {newCargoMass}kg. Exceeds {maximumPayload}kg limit.");
        }
        return true;
    }

    public override string ToString()
    {
       return $"Container [{serialNumber}]\n" +
            $"Type: {GetType().Name}\n" +
            $"Height: {_height} cm\n" +
            $"Tare Weight: {_tareWeight} kg\n" +
            $"Depth: {_depth} cm\n" +
            $"Max Payload: {maximumPayload} kg\n" +
            $"Current Cargo Weight: {cargoWeight} kg\n" +
            $"Total Mass: {totalMass} kg\n";
    }
}