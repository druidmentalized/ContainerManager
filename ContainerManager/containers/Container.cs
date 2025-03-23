using System.ComponentModel;
using ContainerManager.exceptions;
using ContainerManager.main;
using ContainerManager.utils;

namespace ContainerManager.containers;

public abstract class Container : IHazardNotifier
{
    private static int _containerCounter;

    private TypeEnum _type { get; set; }
    public Product? Product { get; private set; }
    public double Height { get; }
    public double Weight { get; }
    public double Depth { get; }
    public string SerialNumber { get; }
    public double MaximumCargoWeight { get; }
    public double CargoWeight { get; set; }
    public double TotalWeight => CargoWeight + Weight;

    protected Container(string containerType, double height, double weight,
        double depth, double maximumCargoWeight, TypeEnum type)
    {
        SerialNumber = GenerateSerialNumber(containerType);
        Height = height;
        Weight = weight;
        Depth = depth;
        MaximumCargoWeight = maximumCargoWeight;
        _type = type;
    }

    private static string GenerateSerialNumber(string containerType)
    {
        return $"KON-{containerType}-{++_containerCounter}";
    }

    public virtual void EmptyCargo()
    {
        Product = null;
        CargoWeight = 0;
    }

    public void LoadCargo(double cargoWeight, Product? product)
    {
        if (CanLoad(cargoWeight, product))
        {
            CargoWeight += cargoWeight;
            Product = product;
        }
    }

    protected virtual bool CanLoad(double cargoWeight, Product? product)
    {
        if (product == null) return false;

        if (!product.IsEquals(Product) || product.Type != _type)
        {
            throw new LoadingWrongProductException("This product can't be loaded to this type of container.");
        }

        if (CargoWeight + cargoWeight > MaximumCargoWeight)
        {
            throw new OverfillException($"Cannot load {cargoWeight}kg. Exceeds {MaximumCargoWeight}kg limit.");
        }

        return true;
    }


    public override string ToString()
    {
        return $"{SerialNumber} " +
               $"(Type: {GetType().Name}, " +
               $"Product: {(Product == null ? "nothing" : Product.Name)}, " +
               $"Height: {Height}cm, " +
               $"Tare Weight: {Weight}kgs, " +
               $"Depth: {Depth}cm, " +
               $"Max Payload: {MaximumCargoWeight} kg, " +
               $"Current Cargo Weight: {CargoWeight}kg, " +
               $"Total Mass: {TotalWeight}kgs)";
    }

    public abstract string Notify();
}