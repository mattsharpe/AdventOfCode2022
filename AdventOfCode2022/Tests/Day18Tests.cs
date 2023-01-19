using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day18Tests
    {
        private readonly string[] _sample =
        {
            "2,2,2",
            "1,2,2",
            "3,2,2",
            "2,1,2",
            "2,3,2",
            "2,2,1",
            "2,2,3",
            "2,2,4",
            "2,2,6",
            "1,2,5",
            "3,2,5",
            "2,1,5",
            "2,3,5",
        };

        private readonly string[] _input = File.ReadAllLines("Input/Day18.txt");

        private Day18 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day18();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(64, _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(4608, _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual(58, _day.Part2(_sample));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(2652, _day.Part2(_input));
        }
    }
}