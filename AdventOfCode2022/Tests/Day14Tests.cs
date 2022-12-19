using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day14Tests
    {
        private readonly string[] _sample =
        {
            "498,4 -> 498,6 -> 496,6",
            "503,4 -> 502,4 -> 502,9 -> 494,9"
        };

        private readonly string[] _input = File.ReadAllLines("Input/Day14.txt");

        private Day14 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day14();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(24, _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(672, _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual(93, _day.Part2(_sample));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(26831, _day.Part2(_input));
        }
    }
}