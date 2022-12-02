using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day02Tests
    {
        private readonly string[] _sample =
        {
            "A Y",
            "B X",
            "C Z"
        };

        private readonly string[] _input = File.ReadAllLines("Input/Day02.txt");

        private Day02 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day02();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(15, _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(14827, _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual(12, _day.Part2(_sample));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(13889, _day.Part2(_input));
        }
    }
}