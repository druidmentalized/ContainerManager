using ContainerManager.containers;
using ContainerManager.transports;

namespace ContainerManager.main
{
    class Program
    {
        private static readonly List<Ship> ships = new();
        private static readonly List<Container> containers = new();

        static void Main()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                
                ShowCurrentState();

                ShowPossibleOperations();
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddContainerShip();
                        break;
                    case "2":
                        RemoveContainerShip();
                        break;
                    case "3":
                        AddContainer();
                        break;
                    case "4":
                        RemoveContainer();
                        break;
                    case "5":
                        PlaceContainerOnShip();
                        break;
                    case "6":
                        TransferContainer();
                        break;
                    case "7":
                        ReplaceContainer();
                        break;
                    case "8":
                        LoadContainer();
                        break;
                    case "9":
                        UnloadContainer();
                        break;
                    case "t":
                        RunIsolatedTestCase();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }
            }
        }

        #region Menu / Display

        private static void ShowCurrentState()
        {
            Console.WriteLine("List of container ships:");
            if (ships.Count == 0)
            {
                Console.WriteLine("  None");
            }
            else
            {
                foreach (var s in ships)
                {
                    var currentLoad = s.CurrentWeight();
                    Console.WriteLine($"  {s.shipName} (speed={s.maxSpeed}, maxContainerNum={s.maxContainerAmount}, maxWeight={s.maxWeightCapacity}), current load: {currentLoad}kg");
                }
            }

            Console.WriteLine("\nList of containers:");
            if (containers.Count == 0)
            {
                Console.WriteLine("  None");
            }
            else
            {
                foreach (var c in containers)
                {
                    Console.WriteLine($"  {c.serialNumber} [{c.GetType().Name}], mass={c.totalMass}kg");
                }
            }

            Console.WriteLine();
        }

        private static void ShowPossibleOperations()
        {
            Console.WriteLine("Possible actions:");
            Console.WriteLine("1. Add a container ship");
            Console.WriteLine("2. Remove a container ship");
            Console.WriteLine("3. Add a container");
            Console.WriteLine("4. Remove a container (from the global container list)");
            Console.WriteLine("5. Place a container on a ship");
            Console.WriteLine("6. Transfer a container between ships");
            Console.WriteLine("7. Replace a container on a ship");
            Console.WriteLine("8. Load a container with cargo");
            Console.WriteLine("9. Unload a container");
            Console.WriteLine("t. Run an isolated test scenario");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");
        }

        #endregion

        #region Ship Methods

        private static void AddContainerShip()
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
                ships.Add(newShip);

                Console.WriteLine($"Successfully added new ship: {newShip.shipName}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding ship: {ex.Message}");
            }
        }

        private static void RemoveContainerShip()
        {
            if (ships.Count == 0)
            {
                Console.WriteLine("No ships available to remove.");
                return;
            }

            Console.Write("Enter the ship name to remove (e.g., Ship-1): ");
            var name = Console.ReadLine();

            var shipToRemove = ships.FirstOrDefault(s => s.shipName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (shipToRemove == null)
            {
                Console.WriteLine($"Ship {name} not found.");
                return;
            }

            ships.Remove(shipToRemove);
            Console.WriteLine($"Removed ship {name} successfully.");
        }

        #endregion

        #region Container Methods

        private static void AddContainer()
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
                        Console.Write("Enter product type (e.g., Fish): ");
                        var productType = Console.ReadLine() ?? "";
                        Console.Write("Enter initial temperature (°C): ");
                        var temp = double.Parse(Console.ReadLine() ?? "0");
                        createdContainer = new RefrigeratedContainer(height, tareWeight, depth, maxPayload, productType, temp);
                        break;
                    case "2":
                        Console.Write("Enter pressure (atm): ");
                        var pressure = double.Parse(Console.ReadLine() ?? "0");
                        createdContainer = new GasContainer(height, tareWeight, depth, maxPayload, pressure);
                        break;
                    case "3":
                        Console.Write("Is it hazardous? (true/false): ");
                        var hazardousInput = Console.ReadLine();
                        bool hazardous = hazardousInput?.Trim().ToLower() == "true";
                        createdContainer = new LiquidContainer(height, tareWeight, depth, maxPayload, hazardous);
                        break;
                    default:
                        Console.WriteLine("Invalid container type. Returning...");
                        return;
                }

                containers.Add(createdContainer);
                Console.WriteLine($"Successfully added container: {createdContainer.serialNumber}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating container: {ex.Message}");
            }
        }

        private static void RemoveContainer()
        {
            if (containers.Count == 0)
            {
                Console.WriteLine("No containers available to remove.");
                return;
            }

            Console.Write("Enter the container serial number to remove: ");
            var serial = Console.ReadLine();
            var containerToRemove = containers.FirstOrDefault(c => c.serialNumber == serial);

            if (containerToRemove == null)
            {
                Console.WriteLine($"Container {serial} not found.");
                return;
            }

            containers.Remove(containerToRemove);
            Console.WriteLine($"Removed container {serial} from the global container list.");
        }

        #endregion

        #region Ship-Container Operations

        private static void PlaceContainerOnShip()
        {
            if (ships.Count == 0)
            {
                Console.WriteLine("No ships available to place containers onto.");
                return;
            }
            if (containers.Count == 0)
            {
                Console.WriteLine("No containers available to place on a ship.");
                return;
            }

            Console.Write("Enter the container serial number to place on a ship: ");
            var serial = Console.ReadLine();
            var container = containers.FirstOrDefault(c => c.serialNumber == serial);
            if (container == null)
            {
                Console.WriteLine($"Container {serial} not found.");
                return;
            }

            Console.Write("Enter the ship name to place container on (e.g., Ship-1): ");
            var shipName = Console.ReadLine();
            var ship = ships.FirstOrDefault(s => s.shipName.Equals(shipName, StringComparison.OrdinalIgnoreCase));
            if (ship == null)
            {
                Console.WriteLine($"Ship {shipName} not found.");
                return;
            }

            try
            {
                ship.AddContainer(container);
                Console.WriteLine($"Container {serial} placed on {shipName}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error placing container on ship: {ex.Message}");
            }
        }

        private static void TransferContainer()
        {
            if (ships.Count < 2)
            {
                Console.WriteLine("Need at least two ships to transfer containers.");
                return;
            }

            Console.Write("Enter the source ship name: ");
            var sourceName = Console.ReadLine();
            var sourceShip = ships.FirstOrDefault(s => s.shipName.Equals(sourceName, StringComparison.OrdinalIgnoreCase));
            if (sourceShip == null)
            {
                Console.WriteLine($"Ship {sourceName} not found.");
                return;
            }

            Console.Write("Enter the target ship name: ");
            var targetName = Console.ReadLine();
            var targetShip = ships.FirstOrDefault(s => s.shipName.Equals(targetName, StringComparison.OrdinalIgnoreCase));
            if (targetShip == null)
            {
                Console.WriteLine($"Ship {targetName} not found.");
                return;
            }

            Console.Write("Enter the container serial number to transfer: ");
            var serial = Console.ReadLine();

            try
            {
                Ship.TransferContainer(sourceShip, targetShip, serial);
                Console.WriteLine($"Successfully transferred {serial} from {sourceName} to {targetName}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error transferring container: {ex.Message}");
            }
        }

        private static void ReplaceContainer()
        {
            Console.Write("Enter the ship name where replacement is happening: ");
            var shipName = Console.ReadLine();
            var ship = ships.FirstOrDefault(s => s.shipName.Equals(shipName, StringComparison.OrdinalIgnoreCase));
            if (ship == null)
            {
                Console.WriteLine($"Ship {shipName} not found.");
                return;
            }

            Console.Write("Enter the container serial number to remove: ");
            var removeSerial = Console.ReadLine();
            Console.Write("Enter the container serial number to add: ");
            var addSerial = Console.ReadLine();

            var removeContainer = containers.FirstOrDefault(c => c.serialNumber == removeSerial);
            var addContainer = containers.FirstOrDefault(c => c.serialNumber == addSerial);

            if (removeContainer == null)
            {
                Console.WriteLine($"Container {removeSerial} not found in the global list.");
                return;
            }
            if (addContainer == null)
            {
                Console.WriteLine($"Container {addSerial} not found in the global list.");
                return;
            }

            try
            {
                ship.ReplaceContainer(removeSerial, addContainer);
                Console.WriteLine($"Replaced container {removeSerial} with {addSerial} on {shipName}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error replacing container: {ex.Message}");
            }
        }

        #endregion

        #region Cargo Operations

        private static void LoadContainer()
        {
            Console.Write("Enter container serial number to load: ");
            var serial = Console.ReadLine();
            var container = containers.FirstOrDefault(c => c.serialNumber == serial);
            if (container == null)
            {
                Console.WriteLine($"Container {serial} not found in global list.");
                return;
            }

            Console.Write("Enter cargo weight to load (kg): ");
            if (!double.TryParse(Console.ReadLine(), out var weight))
            {
                Console.WriteLine("Invalid weight.");
                return;
            }

            if (container is RefrigeratedContainer refCont)
            {
                Console.Write("Enter product type (must match container's type): ");
                var productType = Console.ReadLine();
                try
                {
                    refCont.LoadCargo(weight, productType);
                    Console.WriteLine($"Loaded {weight} kg of {productType} into container {serial}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading cargo: {ex.Message}");
                }
            }
            else
            {
                try
                {
                    container.LoadCargo(weight);
                    Console.WriteLine($"Loaded {weight} kg into container {serial}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading cargo: {ex.Message}");
                }
            }
        }

        private static void UnloadContainer()
        {
            Console.Write("Enter container serial number to unload: ");
            var serial = Console.ReadLine();
            var container = containers.FirstOrDefault(c => c.serialNumber == serial);
            if (container == null)
            {
                Console.WriteLine($"Container {serial} not found in global list.");
                return;
            }

            try
            {
                container.EmptyCargo();
                Console.WriteLine($"Container {serial} has been emptied.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error unloading container: {ex.Message}");
            }
        }

        #endregion

        #region Optional Test Scenario

        private static void RunIsolatedTestCase()
        {
            try
            {
                Console.WriteLine("Running Isolated Test Case...\n");

                var ship1 = new Ship(20, 5, 15000);
                var ship2 = new Ship(25, 3, 10000);

                Console.WriteLine($"Created Ship 1: \n{ship1}");
                Console.WriteLine($"Created Ship 2: \n{ship2}\n");

                var refContainer = new RefrigeratedContainer(250, 500, 300, 4000, "Fish", 2.0);
                var gasContainer = new GasContainer(300, 600, 400, 5000, 15);
                var liquidContainerSafe = new LiquidContainer(280, 450, 350, 5000, false);
                var liquidContainerHazardous = new LiquidContainer(280, 450, 350, 5000, true);

                ship1.AddContainer(refContainer);
                ship1.AddContainer(gasContainer);
                ship1.AddContainer(liquidContainerSafe);
                ship1.AddContainer(liquidContainerHazardous);

                Console.WriteLine("Containers added to Ship 1!\n");
                Console.WriteLine(ship1);

                Console.WriteLine("\nAttempting to load cargo...");
                refContainer.LoadCargo(300, "Fish");
                gasContainer.LoadCargo(400);
                liquidContainerSafe.LoadCargo(2000);

                try
                {
                    liquidContainerHazardous.LoadCargo(3000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }

                Console.WriteLine("\nUpdated Ship 1 details after loading cargo:");
                Console.WriteLine(ship1);

                Console.WriteLine("\nTransferring a container from Ship 1 to Ship 2...");
                Ship.TransferContainer(ship1, ship2, refContainer.serialNumber);
                Console.WriteLine("Transfer successful!\n");

                Console.WriteLine("Ship 1 after transfer:");
                Console.WriteLine(ship1);
                Console.WriteLine("Ship 2 after transfer:");
                Console.WriteLine(ship2);

                Console.WriteLine("\nRemoving a container from Ship 1...");
                ship1.RemoveContainer(gasContainer.serialNumber);
                Console.WriteLine("Gas container removed from Ship 1!");

                Console.WriteLine("\nFinal Ship 1 status:");
                Console.WriteLine(ship1);

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        #endregion
    }
}