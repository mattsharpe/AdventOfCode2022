using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day04Tests
    {
        private readonly string[] _sample = {
            "2-4,6-8",
            "2-3,4-5",
            "5-7,7-9",
            "2-8,3-7",
            "6-6,4-6",
            "2-6,4-8",
        };

        private readonly string[] _input = File.ReadAllLines("Input/Day04.txt");

        private Day04 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day04();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(2, _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(518, _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual(4, _day.Part2(_sample));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(909, _day.Part2(_input));
        }
    }
}