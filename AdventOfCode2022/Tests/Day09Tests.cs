using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day09Tests
    {
        private readonly string[] _sample =
        {
            "R 4",
            "U 4",
            "L 3",
            "D 1",
            "R 4",
            "D 1",
            "L 5",
            "R 2"
        }; 
        
        private readonly string[] _sample2 =
        {
            "R 5",
            "U 8",
            "L 8",
            "D 3",
            "R 17",
            "D 10",
            "L 25",
            "U 20",
        };

        private readonly string[] _input = File.ReadAllLines("Input/Day09.txt");

        private Day09 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day09();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(13, _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(6175, _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual(36, _day.Part2(_sample2));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(2578, _day.Part2(_input));
        }
    }
}