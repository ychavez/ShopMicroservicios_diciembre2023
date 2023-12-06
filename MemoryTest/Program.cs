using BenchmarkDotNet.Running;

namespace MemoryTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<LinqTest>();
        }
    }


   
}
