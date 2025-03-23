using ContainerManager.containers;
using ContainerManager.transports;

namespace ContainerManager.main
{
    class Program
    {
        private static readonly Products _products = new();
        private static readonly Containers _containers = new(_products);
        private static readonly Ships _ships = new(_containers);

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
                        _ships.Add();
                        break;
                    case "2":
                        _ships.Remove();
                        break;
                    case "3":
                        _containers.Add();
                        break;
                    case "4":
                        _containers.Remove();
                        break;
                    case "5":
                        _products.Add();
                        break;
                    case "6":
                        _products.Remove();
                        break;
                    case "7":
                        _ships.PlaceOnShip();
                        break;
                    case "8":
                        _ships.Transfer();
                        break;
                    case "9":
                        _ships.Replace();
                        break;
                    case "10":
                        _containers.Load();
                        break;
                    case "11":
                        _containers.Unload();
                        break;
                    case "t":
                        //RunIsolatedTestCase();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }

                Console.ReadLine();
            }
        }

        #region Menu / Display

        private static void ShowCurrentState()
        {
            _ships.Print();
            _containers.Print();
            _products.Print();

            Console.WriteLine();
        }

        private static void ShowPossibleOperations()
        {
            Console.WriteLine("Possible actions:");
            Console.WriteLine("1. Add a container ship");
            Console.WriteLine("2. Remove a container ship");
            Console.WriteLine("3. Add a container");
            Console.WriteLine("4. Remove a container");
            Console.WriteLine("5. Add a product");
            Console.WriteLine("6. Remove a product");
            Console.WriteLine("7. Place a container on a ship");
            Console.WriteLine("8. Transfer a container between ships");
            Console.WriteLine("9. Replace a container on a ship");
            Console.WriteLine("10. Load a container with cargo");
            Console.WriteLine("11. Unload a container");
            Console.WriteLine("t. Run an isolated test scenario");
            Console.WriteLine("0. Exit");
            Console.WriteLine();
            Console.Write("Select an option: ");
            
        }

        #endregion

        #region Optional Test Scenario

        /*private static void RunIsolatedTestCase()
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
                Ship.TransferContainer(ship1, ship2, refContainer.SerialNumber);
                Console.WriteLine("Transfer successful!\n");

                Console.WriteLine("Ship 1 after transfer:");
                Console.WriteLine(ship1);
                Console.WriteLine("Ship 2 after transfer:");
                Console.WriteLine(ship2);

                Console.WriteLine("\nRemoving a container from Ship 1...");
                ship1.RemoveContainer(gasContainer.SerialNumber);
                Console.WriteLine("Gas container removed from Ship 1!");

                Console.WriteLine("\nFinal Ship 1 status:");
                Console.WriteLine(ship1);

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }*/

        #endregion
    }
}