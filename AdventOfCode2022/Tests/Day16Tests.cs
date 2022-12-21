using AdventOfCode2022.Solutions;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day16Tests
    {
        private readonly string[] _sample =
        {
            "Valve AA has flow rate=0; tunnels lead to valves DD, II, BB",
            "Valve BB has flow rate=13; tunnels lead to valves CC, AA",
            "Valve CC has flow rate=2; tunnels lead to valves DD, BB",
            "Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE",
            "Valve EE has flow rate=3; tunnels lead to valves FF, DD",
            "Valve FF has flow rate=0; tunnels lead to valves EE, GG",
            "Valve GG has flow rate=0; tunnels lead to valves FF, HH",
            "Valve HH has flow rate=22; tunnel leads to valve GG",
            "Valve II has flow rate=0; tunnels lead to valves AA, JJ",
            "Valve JJ has flow rate=21; tunnel leads to valve II",
        };

        private readonly string[] _input = File.ReadAllLines("Input/Day16.txt");

        private Day16 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day16();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(1651, _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(1896, _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual(1707, _day.Part2(_sample));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(2576, _day.Part2(_input));
        }
    }
}