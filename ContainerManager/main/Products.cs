using System.IO.Pipes;
using ContainerManager.utils;

namespace ContainerManager.main;

public class Products
{
    private readonly List<Product> _products = new();

    public int Count => _products.Count;

    public void Print()
    {
        Console.WriteLine("\nList of products:");
        if (_products.Count == 0)
        {
            Console.WriteLine("  None");
        }
        else
        {
            foreach (var p in _products)
            {
                Console.WriteLine("  " + p);
            }
        }
    }

    public void Add()
    {
        Console.Write("Name of the product: ");
        var name = Console.ReadLine();
        Console.WriteLine("Type of the product: ");
        Console.WriteLine("1 - Liquid");
        Console.WriteLine("2 - Gas");
        Console.WriteLine("3 - Refrigerated");
        if (!int.TryParse(Console.ReadLine(), out var typeInput))
        {
            Console.WriteLine("Invalid container type.");
            return;
        }

        var type = (TypeEnum)typeInput;
        var isHazardous = false;
        var storingTemperature = 0.0;
        switch (typeInput)
        {
            case 1:
                Console.Write("Is the product hazardous? (true/false) ");
                var hazardousInput = Console.ReadLine();
                isHazardous = hazardousInput?.Trim().ToLower() == "true";
                break;
            case 2: break;
            case 3:
                Console.Write("Storing temperature of the product: ");
                if (!double.TryParse(Console.ReadLine(), out storingTemperature))
                {
                    Console.WriteLine("Invalid temperature.");
                    return;
                }
                break;
            default:
                Console.WriteLine("Invalid product type. Returning...");
                return;
        }

        

        _products.Add(new Product(name, type, isHazardous, storingTemperature));
        Console.WriteLine($"New product {name} was successfully added.");
    }

    public void Add(Product product)
    {
        _products.Add(product);
    }

    public void Remove()
    {
        if (_products.Count == 0)
        {
            Console.WriteLine("No products available to remove.");
        }
        
        Console.Write("Name of the product: ");
        var name = Console.ReadLine();
        var productToRemove = Find(name);

        if (productToRemove == null)
        {
            Console.WriteLine($"Product {name} not found.");
            return;
        }
        
        _products.Remove(productToRemove);
        Console.WriteLine($"Product {name} was successfully removed.");
    }

    
    // Helper
    public Product? Find(string? productName)
    {
        return _products.FirstOrDefault(p => p.Name == productName);
    }

    public void Clear()
    {
        _products.Clear();
    }
}