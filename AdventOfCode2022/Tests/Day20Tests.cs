using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day20Tests
    {
        private readonly string[] _sample =
        {
            "1",
            "2",
            "-3",
            "3",
            "-2",
            "0",
            "4",
        };

        private readonly string[] _input = File.ReadAllLines("Input/Day20.txt");

        private Day20 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day20();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(3, _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(7395, _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual(1623178306, _day.Part2(_sample));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(1640221678213, _day.Part2(_input));
        }
    }
}