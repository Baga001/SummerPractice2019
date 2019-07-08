using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Граф 1");
            Graph G1 = new Graph("graph1.txt");
            G1.FindBridges();
            G1.SearchCycles();
            Console.WriteLine();

            Console.WriteLine("Граф 2");
            Graph G2 = new Graph("graph2.txt");
            G2.FindBridges();
            G2.SearchCycles();
            Console.WriteLine();

            Console.WriteLine("Граф 3");
            Graph G3 = new Graph("graph3.txt");
            G3.FindBridges();
            G3.SearchCycles();
            Console.WriteLine();

            Console.WriteLine("Граф 4");
            Graph G4 = new Graph("graph4.txt");
            G4.FindBridges();
            G4.SearchCycles();
            Console.WriteLine();

            Console.WriteLine("Граф 5");
            Graph G5 = new Graph("graph5.txt");
            G5.FindBridges();
            G5.SearchCycles();
            Console.WriteLine();

        }
    }
}
