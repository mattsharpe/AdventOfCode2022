using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class DayXTests
    {
        private readonly string[] _sample = {""};

        private readonly string[] _input = File.ReadAllLines("Input/DayX.txt");

        private DayX _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new DayX();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(0, _day.Part1(_sample));
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