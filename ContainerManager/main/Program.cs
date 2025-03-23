using System.Data;
using ContainerManager.containers;
using ContainerManager.transports;
using ContainerManager.utils;

namespace ContainerManager.main
{
    class Program
    {
        private static readonly Products Products = new();
        private static readonly Containers Containers = new(Products);
        private static readonly Ships Ships = new(Containers);

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
                        Ships.Add();
                        break;
                    case "2":
                        Ships.Remove();
                        break;
                    case "3":
                        Containers.Add();
                        break;
                    case "4":
                        Containers.Remove();
                        break;
                    case "5":
                        Products.Add();
                        break;
                    case "6":
                        Products.Remove();
                        break;
                    case "7":
                        Ships.PlaceOnShip();
                        break;
                    case "8":
                        Ships.Transfer();
                        break;
                    case "9":
                        Ships.Replace();
                        break;
                    case "10":
                        Containers.Load();
                        break;
                    case "11":
                        Containers.Unload();
                        break;
                    case "g":
                        Generate();
                        break;
                    case "c":
                        Clear();
                        break;
                    case "t":
                        RunTestWithGeneratedData();
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
            Ships.Print();
            Containers.Print();
            Products.Print();

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
            Console.WriteLine("g. Generate test data");
            Console.WriteLine("c. Clear data");
            Console.WriteLine("t. Run test with generated data");
            Console.WriteLine("0. Exit");
            Console.WriteLine();
            Console.Write("Select an option: ");
            
        }

        #endregion
        
        private static void Generate()
        {
            Clear();
            
            var ship1 = new Ship(20, 5, 15000);
            Ships.Add(ship1);
            var ship2 = new Ship(25, 3, 10000);
            Ships.Add(ship2);
            
            var refContainer = new RefrigeratedContainer(250, 500, 300, 4000, 10);
            var gasContainer = new GasContainer(300, 600, 400, 5000, 15);
            var liquidContainerSafe = new LiquidContainer(280, 450, 350, 5000, false);
            var liquidContainerHazardous = new LiquidContainer(280, 450, 350, 5000, true);
                
            Containers.Add(refContainer);
            Containers.Add(gasContainer);
            Containers.Add(liquidContainerSafe);
            Containers.Add(liquidContainerHazardous);

            var bananas = new Product("Bananas", TypeEnum.REFRIGATED, false, 12);
            var air = new Product("Air", TypeEnum.GAS);
            var water = new Product("Water", TypeEnum.LIQUID);
            var oil = new Product("Oil", TypeEnum.LIQUID, true);
                
            Products.Add(bananas);
            Products.Add(air);
            Products.Add(water);
            Products.Add(oil);
            
            Console.WriteLine("Test data generated successfully.");
        }

        private static void Clear()
        {
            Ships.Clear();
            Containers.Clear();
            Products.Clear();
        }

        private static void RunTestWithGeneratedData()
        {
            
        }
    }
}