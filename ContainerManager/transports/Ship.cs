using ContainerManager.containers;

namespace ContainerManager.transports;

public class Ship
{
    private readonly int _maxSpeed;
    private readonly int _maxContainerAmount;
    private readonly double _maxWeightCapacity;
    List<Container> containers = new List<Container>();

    public Ship(int maxSpeed, int maxContainerAmount, double maxWeightCapacity)
    {
        _maxSpeed = maxSpeed;
        _maxContainerAmount = maxContainerAmount;
        _maxWeightCapacity = maxWeightCapacity;
    }

    public void AddContaier(Container container)
    {
        containers.Add(container);
    }

    public void RemoveContaier(string containerSerialNumber)
    {
        
    }
    
    public void ReplaceContainer(string containerReplaceNumber, Container container)
    {
        
    }

    public static void TransferContainer(Ship from, Ship to, string containerSerialNumber)
    {
        
    }
}