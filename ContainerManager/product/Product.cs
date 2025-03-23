using ContainerManager.utils;

namespace ContainerManager.main;

public class Product
{
    public string Name { get; set; }
    public TypeEnum Type { get; set; }
    public bool IsHazardous { get; set;}
    public double StoringTemperature { get; set; }

    public Product(string name, TypeEnum type, bool isHazardous, double storingTemperature)
    {
        Name = name;
        Type = type;
        IsHazardous = isHazardous;
        StoringTemperature = storingTemperature;
    }

    public bool IsEquals(Product? compareTo)
    {
        if (compareTo == null)
        {
            return true;
        }
        return Name == compareTo.Name &&
               Type == compareTo.Type &&
               IsHazardous == compareTo.IsHazardous &&
               Math.Abs(StoringTemperature - compareTo.StoringTemperature) < 0.001;
    }

    public override string ToString()
    {
        return
            $" {Name} (Type={Type}, Hazardous={(IsHazardous ? "Yes" : "No")}, Storing Temperature={StoringTemperature})";
    }
}