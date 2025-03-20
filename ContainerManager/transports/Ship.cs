using System.Text;
using ContainerManager.containers;

namespace ContainerManager.transports
{
    public class Ship
    {
        private static int _shipCounter;

        public readonly string shipName;
        public readonly int maxSpeed;
        public readonly int maxContainerAmount;
        public readonly double maxWeightCapacity;
        public readonly Dictionary<string, Container> containers = new();

        public Ship(int maxSpeed, int maxContainerAmount, double maxWeightCapacity)
        {
            shipName = $"Ship-{++_shipCounter}";
            this.maxSpeed = maxSpeed;
            this.maxContainerAmount = maxContainerAmount;
            this.maxWeightCapacity = maxWeightCapacity;
        }

        public void AddContainer(Container container)
        {
            if (containers.Count >= maxContainerAmount)
            {
                throw new InvalidOperationException($"Cannot add container {container.serialNumber}. Ship has reached max capacity of {maxContainerAmount} containers!");
            }

            double currentWeight = containers.Values.Sum(c => c.totalMass);
            if (currentWeight + container.totalMass > maxWeightCapacity)
            {
                throw new InvalidOperationException($"Cannot add container {container.serialNumber}. Ship would exceed max weight ({maxWeightCapacity} kg)!");
            }

            if (!containers.TryAdd(container.serialNumber, container))
            {
                throw new InvalidOperationException($"Container {container.serialNumber} is already on this ship!");
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
                throw new KeyNotFoundException($"Container {containerSerialNumber} not found on {shipName}.");
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
                throw new KeyNotFoundException($"Container {containerSerialNumber} not found on {from.shipName}.");
            }

            from.RemoveContainer(movedContainer);
            to.AddContainer(movedContainer);
        }

        public double CurrentWeight()
        {
            return containers.Values.Sum(c => c.totalMass);
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder()
                .AppendLine($"Ship: [{shipName}]")
                .AppendLine($"Max Speed: {maxSpeed} knots")
                .AppendLine($"Max Containers: {maxContainerAmount}")
                .AppendLine($"Max Weight Capacity: {maxWeightCapacity} kg")
                .AppendLine($"Current Load: {containers.Values.Sum(c => c.totalMass)} kg / {maxWeightCapacity} kg")
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
}