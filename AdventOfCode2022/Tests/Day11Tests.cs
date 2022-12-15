using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day11Tests
    {
        private readonly string[] _sample = File.ReadAllLines("Input/Day11Sample.txt");

        private readonly string[] _input = File.ReadAllLines("Input/Day11.txt");

        private Day11 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day11();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(10605, _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(0, _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual(0, _day.Part2(_sample));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(0, _day.Part2(_input));
        }
    }
}