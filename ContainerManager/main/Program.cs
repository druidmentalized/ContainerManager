using ContainerManager.containers;
using ContainerManager.transports;

namespace ContainerManager.main;

using System;

class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine("Welcome to the Container Manager System!\n");

            var ship1 = new Ship(20, 5, 15000); // Max Speed: 20 knots, Max Containers: 5, Max Weight: 15000 kg
            var ship2 = new Ship(25, 3, 10000); // Another ship with different capacity

            Console.WriteLine($"Created Ship 1: \n{ship1}");
            Console.WriteLine($"Created Ship 2: \n{ship2}\n");

            var refContainer = new RefrigeratedContainer(250, 500, 300, 4000, "Fish", 2.0);
            var gasContainer = new GasContainer(300, 600, 400, 5000, 15); // 15 atm pressure
            var liquidContainerSafe = new LiquidContainer(280, 450, 350, 5000, false); // Safe Liquid
            var liquidContainerHazardous = new LiquidContainer(280, 450, 350, 5000, true); // Hazardous Liquid

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
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}