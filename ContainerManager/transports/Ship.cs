using System.Text;
using ContainerManager.containers;

namespace ContainerManager.transports;

public class Ship
{
    private static int _shipCounter;

    private readonly string _shipName;
    private readonly int _maxSpeed;
    private readonly int _maxContainerAmount;
    private readonly double _maxWeightCapacity;
    private Dictionary<string, Container> containers = new();

    public Ship(int maxSpeed, int maxContainerAmount, double maxWeightCapacity)
    {
        _shipName = $"Ship-{++_shipCounter}";
        _maxSpeed = maxSpeed;
        _maxContainerAmount = maxContainerAmount;
        _maxWeightCapacity = maxWeightCapacity;
    }

    public void AddContainer(Container container)
    {
        if (containers.Count >= _maxContainerAmount)
        {
            Console.WriteLine($"Cannot add container {container.serialNumber}. Ship has reached max container capacity!");
            return;
        }

        var currentWeight = containers.Values.Sum(c => c.totalMass);
        if (currentWeight + container.totalMass > _maxWeightCapacity)
        {
            Console.WriteLine($"Cannot add container {container.serialNumber}. Ship would exceed max weight!");
            return;
        }

        if (!containers.TryAdd(container.serialNumber, container))
        {
            Console.WriteLine($"️Container {container.serialNumber} is already on this ship!");
        }
    }
    
    public void RemoveContainer(Container container)
    {
        RemoveContainer(container.serialNumber);
    }
    public void RemoveContainer(string containerSerialNumber)
    {
        if (!containers.Remove(containerSerialNumber))
        {
            Console.WriteLine($"Container {containerSerialNumber} not found on {_shipName}.");
        }
    }
    
    public void ReplaceContainer(Container remove, Container add)
    {
        RemoveContainer(remove);
        AddContainer(add);
    }
    public void ReplaceContainer(string containerReplaceNumber, Container container)
    {
        RemoveContainer(containerReplaceNumber);
        AddContainer(container);
    }

    public static void TransferContainer(Ship from, Ship to, string containerSerialNumber)
    {
        if (!from.containers.TryGetValue(containerSerialNumber, out var movedContainer))
        {
            Console.WriteLine($"Container {containerSerialNumber} not found on {from._shipName}.");
            return;
        }

        from.RemoveContainer(movedContainer);
        to.AddContainer(movedContainer);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder()
            .AppendLine($"Ship [{_shipName}]")
            .AppendLine($"Max Speed: {_maxSpeed} knots")
            .AppendLine($"Max Containers: {_maxContainerAmount}")
            .AppendLine($"Max Weight Capacity: {_maxWeightCapacity} kg")
            .AppendLine($"Current Load: {containers.Values.Sum(c => c.totalMass)} kg / {_maxWeightCapacity} kg")
            .AppendLine("─────────────────────────────────────────────");

        if (containers.Count == 0)
        {
            sb.AppendLine("No containers on this ship.");
        }
        else
        {
            foreach (var container in containers.Values)
            {
                sb.AppendLine(container.ToString());
            }
        }

        return sb.ToString();
    }
}