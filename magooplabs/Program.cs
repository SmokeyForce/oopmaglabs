using System;
using ParkingManagementSystem.Lab1;
using ParkingManagementSystem.Lab2;
using ParkingManagementSystem.Lab3;
using ParkingManagementSystem.Lab4;
using ParkingManagementSystem.Lab5;

namespace CombinedDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Demo:\n");

            Lab1Demo.RunDemo();

            Console.WriteLine("\n-----------------------------\n");

            Lab2Demo.RunDemo();
            
            Console.WriteLine("\n-----------------------------\n");

            Lab3Demo.RunDemo();
            
            Console.WriteLine("\n-----------------------------\n");

            Lab4Demo.RunDemo();

            Console.WriteLine("\n-----------------------------\n");

            Lab5Demo.RunDemo();
            
            Console.WriteLine("\n-----------------------------\n");

            Lab6Demo.RunDemo();

            Console.WriteLine("Press any button to exit");
            Console.ReadKey();
        }
    }
}
