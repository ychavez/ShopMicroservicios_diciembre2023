using BenchmarkDotNet.Attributes;
using System;

namespace MemoryTest
{
    [MemoryDiagnoser(false)]
    public class SpanTest
    {

        public static readonly string _dateAsText = "05 12 2023";


        [Benchmark]
        public (int day, int month, int year) DateWithstring() 
        {
            var dayAsText = _dateAsText.Substring(0, 2);
            var monthAsText = _dateAsText.Substring(3, 2);
            var yearAsText = _dateAsText.Substring(6);

            var day = int.Parse(dayAsText);
            var month = int.Parse(monthAsText);
            var year = int.Parse(yearAsText);

            return (day, month, year);

        }


        [Benchmark]
        public (int day, int month, int year) DateWithstringSpan()
        {
            ReadOnlySpan<char> dateSpan = _dateAsText;

            var dayAsText = dateSpan.Slice(0, 2);
            var monthAsText = dateSpan.Slice(3, 2);
            var yearAsText = dateSpan.Slice(6);

            var day = int.Parse(dayAsText);
            var month = int.Parse(monthAsText);
            var year = int.Parse(yearAsText);

            return (day, month, year);

        }


    }
}
