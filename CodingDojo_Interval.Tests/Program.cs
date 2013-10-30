using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodingDojo_Interval.Tests
{
    [TestFixture]
    public class Program
    {

        [TestCase("1", "1")]
        [TestCase("2", "2")]
        [TestCase("5,1", "1,5")]
        [TestCase("5,1,11", "1,5,11")]
        [TestCase("1,2", "1-2")]
        [TestCase("1,2,5", "1-2,5")]
        [TestCase("1,2,3", "1-3")]
        [TestCase("1,6,4,12,3,2,11,9,10,76,50", "1-4,6,9-12,50,76")]
        public void ItDescribesTheSequenceCorrectly(string input, string output)
        {
            var numberIntervals = new NumberIntervals();

            var result = numberIntervals.Describe(input);

            Assert.That(result, Is.EqualTo(output));
        }
    }

    public class NumberIntervals
    {
        public string Describe(string numberInterval)
        {
            var values = numberInterval.Split(',').Select(x => Convert.ToInt32(x)).OrderBy(i => i).ToList();
            var list = Describe(values);
            return string.Join(",", list.ToArray());
        }

        private List<String> Describe(List<int> ordered)
        {
            return Describe(new List<string>(), ordered);
        }

        private List<String> Describe(List<String> head, List<int> tail)
        {
            var numbers = FindSequence(tail);
            if (numbers.Count == 1)
            {
                head.Add(numbers.FirstOrDefault().ToString());
            }
            else
            {
                var first = numbers.Min();
                var last = numbers.Max();
                head.Add(string.Format("{0}-{1}", first, last));
            }

            if (tail.Any())
                Describe(head, tail);

            return head;
        }

        private List<int> FindSequence(List<int> ordered)
        {
            var top = ordered.First();
            var next = top;

            var sequence = new List<int>();

            do
            {
                sequence.Add(top);
                ordered.Remove(top);
                if (!ordered.Any())
                    break;
                next = ordered.First();
                if (top == next - 1)
                    top = next;
            } while (top == next);

            return sequence;
        }
    }
}