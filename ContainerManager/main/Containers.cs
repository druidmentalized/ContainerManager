using ContainerManager.containers;

namespace ContainerManager.main;

public class Containers
{
    private readonly List<Container> _containers = new();
    private Products _products;
    
    public int Count => _containers.Count;

    public Containers(Products products)
    {
        _products = products;
    }
    
    public void Print()
    {
        Console.WriteLine("\nList of containers:");
        if (_containers.Count == 0)
        {
            Console.WriteLine("  None");
        }
        else
        {
            foreach (var c in _containers)
            {
                Console.WriteLine("  " + c);
            }
        }
    }
    
    public void Add()
    {
        try
        {
            Console.WriteLine("Choose container type to add:");
            Console.WriteLine("1) Refrigerated");
            Console.WriteLine("2) Gas");
            Console.WriteLine("3) Liquid");
            Console.Write("Selection: ");
            var selection = Console.ReadLine();

            Console.Write("Height (cm): ");
            double height = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Tare Weight (kg): ");
            double tareWeight = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Depth (cm): ");
            double depth = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Maximum Payload (kg): ");
            double maxPayload = double.Parse(Console.ReadLine() ?? "0");

            Container createdContainer = null;
            
            switch (selection)
            {
                case "1":
                    Console.Write("Enter initial temperature (°C): ");
                    var temperature = double.Parse(Console.ReadLine() ?? "0");
                    createdContainer =
                        new RefrigeratedContainer(height, tareWeight, depth, maxPayload, temperature);
                    break;
                case "2":
                    Console.Write("Enter pressure (atm): ");
                    var pressure = double.Parse(Console.ReadLine() ?? "0");
                    createdContainer = new GasContainer(height, tareWeight, depth, maxPayload, pressure);
                    break;
                case "3":
                    Console.Write("Is it hazardous? (true/false): ");
                    var hazardousInput = Console.ReadLine();
                    var hazardous = hazardousInput?.Trim().ToLower() == "true";
                    createdContainer = new LiquidContainer(height, tareWeight, depth, maxPayload, hazardous);
                    break;
                default:
                    Console.WriteLine("Invalid container type. Returning...");
                    return;
            }

            _containers.Add(createdContainer);
            Console.WriteLine($"Successfully added container: {createdContainer.SerialNumber}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating container: {ex.Message}");
        }
    }

    public void Add(Container container)
    {
        _containers.Add(container);
    }

    public void Remove()
    {
        if (_containers.Count == 0)
        {
            Console.WriteLine("No containers available to remove.");
            return;
        }

        Console.Write("Enter the container serial number to remove: ");
        var serialNumber = Console.ReadLine();
        var containerToRemove = Find(serialNumber);

        if (containerToRemove == null)
        {
            Console.WriteLine($"Container {serialNumber} not found.");
            return;
        }

        _containers.Remove(containerToRemove);
        Console.WriteLine($"Container {serialNumber} was successfully removed.");
    }
    
    public void Load()
    {
        if (_products.Count == 0)
        {
            Console.WriteLine("No products available to load.");
            return;
        }

        Console.Write("Enter product name to load: ");
        var productName = Console.ReadLine();
        var product = _products.Find(productName);
        
        if (product == null)
        {
            Console.WriteLine($"Product {productName} not found in product list.");
            return;
        }
        
        Console.Write("Enter cargo weight to load (kg): ");
        if (!double.TryParse(Console.ReadLine(), out var weight))
        {
            Console.WriteLine("Invalid weight.");
            return;
        }
        
        Console.Write("Enter container serial number to load: ");
        var serialNumber = Console.ReadLine();
        var container = Find(serialNumber);
        
        if (container == null)
        {
            Console.WriteLine($"Container {serialNumber} not found in container list.");
            return;
        }

        try
        {
            container.LoadCargo(weight, product);
            Console.WriteLine($"Loaded {product.Name} {weight}kgs into container {serialNumber}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading cargo: {ex.Message}");
        }
    }
    
    public void Unload()
    {
        Console.Write("Enter container serial number to unload: ");
        var serialNumber = Console.ReadLine();
        var container = Find(serialNumber);
        if (container == null)
        {
            Console.WriteLine($"Container {serialNumber} not found in container list.");
            return;
        }

        try
        {
            container.EmptyCargo();
            Console.WriteLine($"Container {serialNumber} has been emptied.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error unloading container: {ex.Message}");
        }
    }
    
    // Helper
    public Container? Find(string? serialNumber)
    {
        return _containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
    }

    public void Clear()
    {
        _containers.Clear();
    }
}