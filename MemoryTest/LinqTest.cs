using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace MemoryTest
{
    [MemoryDiagnoser(false)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class LinqTest
    {

        private IEnumerable<int> _item;

        [GlobalSetup]
        public void Setup()
        {
            _item = Enumerable.Range(1, 2000).ToArray();
        }


        [Benchmark]
        public int GetMax() => _item.Max();

        [Benchmark]
        public double GetAVG() => _item.Average();


        [Benchmark]
        public double GetMin() => _item.Min();

        [Benchmark]
        public int GetMax_own()
        {
            int biggest = int.MinValue;
            foreach (var item in _item)
            {
                if (item > biggest)
                    biggest = item;

            }
            return biggest;
        }





    }
}
