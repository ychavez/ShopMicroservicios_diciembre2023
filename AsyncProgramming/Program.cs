using System.Diagnostics;

namespace AsyncProgramming
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var timer = new Stopwatch();
            Console.WriteLine("Iniciando desayuno");
            timer.Start();
            await Breakfast.Do1000CoffeeAsync();
            timer.Stop();
            Console.WriteLine($"El desayuno tardo {timer.ElapsedMilliseconds} ms");
        }
    }
}
