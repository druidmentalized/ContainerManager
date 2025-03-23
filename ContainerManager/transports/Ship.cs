using System.Text;
using ContainerManager.containers;

namespace ContainerManager.transports
{
    public class Ship
    {
        private static int _shipCounter;

        public string Name { get; set; }
        public int MaxSpeed { get; set; }
        public int MaxContainerAmount { get; set; }
        public double MaxWeightCapacity { get; set; }
        public readonly HashSet<Container> Containers = new();

        public Ship(int maxSpeed, int maxContainerAmount, double maxWeightCapacity)
        {
            Name = $"Ship-{++_shipCounter}";
            MaxSpeed = maxSpeed;
            MaxContainerAmount = maxContainerAmount;
            MaxWeightCapacity = maxWeightCapacity;
        }

        public void AddContainer(Container container)
        {
            if (Containers.Count >= MaxContainerAmount)
            {
                throw new InvalidOperationException($"Cannot add container {container.SerialNumber}. Ship has reached max capacity of {MaxContainerAmount} containers!");
            }

            double currentWeight = Containers.Sum(c => c.TotalWeight);
            if (currentWeight + container.TotalWeight > MaxWeightCapacity)
            {
                throw new InvalidOperationException($"Cannot add container {container.SerialNumber}. Ship would exceed max weight ({MaxWeightCapacity} kg)!");
            }

            if (!Containers.Add(container))
            {
                throw new InvalidOperationException($"Container {container.SerialNumber} is already on this ship!");
            }
        }

        public void RemoveContainer(Container container)
        {
            if (Containers.Remove(container))
            {
                Console.WriteLine($"Container {container.SerialNumber} has been removed!");
            }
            else
            {
                Console.WriteLine($"Container {container.SerialNumber} does not exist!");
            }
        }

        public void ReplaceContainer(Container remove, Container add)
        {
            RemoveContainer(remove);
            AddContainer(add);
        }

        public static void TransferContainer(Ship from, Ship to, Container container)
        {
            from.RemoveContainer(container);
            to.AddContainer(container);
        }

        public double CurrentWeight()
        {
            return Containers.Sum(c => c.TotalWeight);
        }
        
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(
                $"{Name} (Speed={MaxSpeed}, Max Container Count={MaxContainerAmount}, Max Weight={MaxWeightCapacity}), Current Load: {CurrentWeight()}kgs");
            
            if (Containers.Count == 0)
            {
                sb.AppendLine("     No containers on this ship.");
            }
            else
            {
                foreach (var container in Containers)
                {
                    sb.AppendLine("     " + container);
                }
            }

            return sb.ToString();
        }
    }
}