using AdventOfCode2022.Solutions;
using System.Collections.Immutable;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day12Tests
    {
        private readonly string[] _sample =
        {
            "Sabqponm",
            "abcryxxl",
            "accszExk",
            "acctuvwj",
            "abdefghi"
        };

        private readonly string[] _input = File.ReadAllLines("Input/Day12.txt");

        private Day12 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day12();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(31, _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(361, _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual(29, _day.Part2(_sample));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(354, _day.Part2(_input));
        }
    }
}