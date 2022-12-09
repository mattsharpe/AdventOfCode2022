using System.Security.Cryptography.X509Certificates;
using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day08Tests
    {
        private readonly string[] _sample =
        { 
            "30373",
            "25512",
            "65332",
            "33549",
            "35390"
        };

        private readonly string[] _input = File.ReadAllLines("Input/Day08.txt");

        private Day08 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day08();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(21, _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(1776, _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual(8, _day.Part2(_sample));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(234416, _day.Part2(_input));
        }
    }
}