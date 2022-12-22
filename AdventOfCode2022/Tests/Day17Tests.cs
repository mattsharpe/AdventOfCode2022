using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day17Tests
    {
        private readonly string _sample = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>" ;

        private readonly string _input = File.ReadAllText("Input/Day17.txt");

        private Day17 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day17();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(3068, _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(3059, _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual(1514285714288, _day.Part2(_sample));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(1500874635587, _day.Part2(_input));
        }
    }
}