using System.Text;
using ContainerManager.containers;

namespace ContainerManager.transports
{
    public class Ship
    {
        private static int _shipCounter;

        private readonly string _shipName;
        private readonly int _maxSpeed;
        private readonly int _maxContainerAmount;
        private readonly double _maxWeightCapacity;
        private readonly Dictionary<string, Container> _containers = new();

        public Ship(int maxSpeed, int maxContainerAmount, double maxWeightCapacity)
        {
            _shipName = $"Ship-{++_shipCounter}";
            _maxSpeed = maxSpeed;
            _maxContainerAmount = maxContainerAmount;
            _maxWeightCapacity = maxWeightCapacity;
        }

        public void AddContainer(Container container)
        {
            if (_containers.Count >= _maxContainerAmount)
            {
                throw new InvalidOperationException($"Cannot add container {container.serialNumber}. Ship has reached max capacity of {_maxContainerAmount} containers!");
            }

            double currentWeight = _containers.Values.Sum(c => c.totalMass);
            if (currentWeight + container.totalMass > _maxWeightCapacity)
            {
                throw new InvalidOperationException($"Cannot add container {container.serialNumber}. Ship would exceed max weight ({_maxWeightCapacity} kg)!");
            }

            if (!_containers.TryAdd(container.serialNumber, container))
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
            if (!_containers.Remove(containerSerialNumber))
            {
                throw new KeyNotFoundException($"Container {containerSerialNumber} not found on {_shipName}.");
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
            if (!from._containers.TryGetValue(containerSerialNumber, out var movedContainer))
            {
                throw new KeyNotFoundException($"Container {containerSerialNumber} not found on {from._shipName}.");
            }

            from.RemoveContainer(movedContainer);
            to.AddContainer(movedContainer);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder()
                .AppendLine($"Ship: [{_shipName}]")
                .AppendLine($"Max Speed: {_maxSpeed} knots")
                .AppendLine($"Max Containers: {_maxContainerAmount}")
                .AppendLine($"Max Weight Capacity: {_maxWeightCapacity} kg")
                .AppendLine($"Current Load: {_containers.Values.Sum(c => c.totalMass)} kg / {_maxWeightCapacity} kg")
                .AppendLine("─────────────────────────────────────────────");

            if (_containers.Count == 0)
            {
                sb.AppendLine("No containers on this ship.");
            }
            else
            {
                foreach (var container in _containers.Values)
                {
                    sb.AppendLine(container.ToString());
                }
            }

            return sb.ToString();
        }
    }
}