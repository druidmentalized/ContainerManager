using ContainerManager.containers;
using ContainerManager.transports;

namespace ContainerManager.main;

public class Ships
{
    private readonly List<Ship> _ships = new();
    private Containers _containers;

    public Ships(Containers containers)
    {
        _containers = containers;
    }

    public void Print()
    {
        Console.WriteLine("List of container ships:");
        if (_ships.Count == 0)
        {
            Console.WriteLine("  None");
        }
        else
        {
            foreach (var s in _ships)
            {
                Console.WriteLine("  " + s);
            }
        }
    }
    
    public void Add()
    {
        try
        {
            Console.Write("Enter max speed (knots): ");
            var maxSpeed = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter max container amount: ");
            var maxContainerAmount = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter max weight capacity (kg): ");
            var maxWeightCapacity = double.Parse(Console.ReadLine() ?? "0");

            var newShip = new Ship(maxSpeed, maxContainerAmount, maxWeightCapacity);
            _ships.Add(newShip);

            Console.WriteLine($"Successfully added new ship: {newShip.Name}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding ship: {ex.Message}");
        }
    }
    
    public void Remove()
    {
        if (_ships.Count == 0)
        {
            Console.WriteLine("No ships available to remove.");
            return;
        }

        Console.Write("Enter the ship name to remove (e.g., Ship-1): ");
        var name = Console.ReadLine();

        var shipToRemove = _ships.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (shipToRemove == null)
        {
            Console.WriteLine($"Ship {name} not found.");
            return;
        }

        _ships.Remove(shipToRemove);
        Console.WriteLine($"Removed ship {name} successfully.");
    }
    
    public void PlaceOnShip()
    {
        if (_ships.Count == 0)
        {
            Console.WriteLine("No ships available to place containers onto.");
            return;
        }
        
        if (_containers.Count == 0)
        {
            Console.WriteLine("No containers available to place on a ship.");
            return;
        }

        Console.Write("Enter the container serial number to place on a ship: ");
        var serialNumber = Console.ReadLine();
        var container = _containers.Find(serialNumber);
        if (container == null)
        {
            Console.WriteLine($"Container {serialNumber} not found.");
            return;
        }

        Console.Write("Enter the ship name to place container on (e.g., Ship-1): ");
        var shipName = Console.ReadLine();
        var ship = _ships.FirstOrDefault(s => s.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase));
        if (ship == null)
        {
            Console.WriteLine($"Ship {shipName} not found.");
            return;
        }

        try
        {
            ship.AddContainer(container);
            Console.WriteLine($"Container {serialNumber} placed on {shipName}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error placing container on ship: {ex.Message}");
        }
    }
    
    public void Transfer()
    {
        if (_ships.Count < 2)
        {
            Console.WriteLine("Need at least two ships to transfer containers.");
            return;
        }

        Console.Write("Enter the source ship name: ");
        var sourceName = Console.ReadLine();
        var sourceShip = _ships.FirstOrDefault(s => s.Name.Equals(sourceName, StringComparison.OrdinalIgnoreCase));
        if (sourceShip == null)
        {
            Console.WriteLine($"Ship {sourceName} not found.");
            return;
        }

        Console.Write("Enter the target ship name: ");
        var targetName = Console.ReadLine();
        var targetShip = _ships.FirstOrDefault(s => s.Name.Equals(targetName, StringComparison.OrdinalIgnoreCase));
        if (targetShip == null)
        {
            Console.WriteLine($"Ship {targetName} not found.");
            return;
        }

        Console.Write("Enter the container serial number to transfer: ");
        var serialNumber = Console.ReadLine();
        var container = _containers.Find(serialNumber);
        try
        {
            Ship.TransferContainer(sourceShip, targetShip, container);
            Console.WriteLine($"Successfully transferred {serialNumber} from {sourceName} to {targetName}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error transferring container: {ex.Message}");
        }
    }
    
    public void Replace()
    {
        Console.Write("Enter the ship name where replacement is happening: ");
        var shipName = Console.ReadLine();
        var ship = _ships.FirstOrDefault(s => s.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase));
        if (ship == null)
        {
            Console.WriteLine($"Ship {shipName} not found.");
            return;
        }

        Console.Write("Enter the container serial number to remove: ");
        var removeSerialNumber = Console.ReadLine();
        Console.Write("Enter the container serial number to add: ");
        var addSerialNumber = Console.ReadLine();

        var removeContainer = _containers.Find(removeSerialNumber);
        var addContainer = _containers.Find(addSerialNumber);

        if (removeContainer == null)
        {
            Console.WriteLine($"Container {removeSerialNumber} not found in the container list.");
            return;
        }
        if (addContainer == null)
        {
            Console.WriteLine($"Container {addSerialNumber} not found in the container list.");
            return;
        }

        try
        {
            ship.ReplaceContainer(removeContainer, addContainer);
            Console.WriteLine($"Replaced container {removeSerialNumber} with {addSerialNumber} on {shipName}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error replacing container: {ex.Message}");
        }
    }
}