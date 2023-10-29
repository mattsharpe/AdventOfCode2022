using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day19Tests
    {
        private readonly string[] _sample =
        {
            "Blueprint 1: Each ore robot costs 4 ore. Each clay robot costs 2 ore. Each obsidian robot costs 3 ore and 14 clay. Each geode robot costs 2 ore and 7 obsidian.",
            "Blueprint 2: Each ore robot costs 2 ore. Each clay robot costs 3 ore. Each obsidian robot costs 3 ore and 8 clay. Each geode robot costs 3 ore and 12 obsidian."
        };

        private readonly string[] _input = File.ReadAllLines("Input/Day19.txt");

        private Day19 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day19();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(33, _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(1356, _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual(3472, _day.Part2(_sample));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(27720, _day.Part2(_input));
        }
    }
}