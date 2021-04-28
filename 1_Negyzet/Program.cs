using NegyzetNevter;
using System;

namespace _1_Negyzet
{
    class Program
    {
        static void Main(string[] args)
        {
            Negyzet negyzet = new Negyzet(3);

            Console.WriteLine("Négyzet átjója:");
            Console.WriteLine(negyzet.Atlo);

            Console.WriteLine();
            Console.WriteLine("Kocka térfogata:");
            Console.WriteLine(negyzet.TeglatestTerfogata(5));
        }
    }
}
