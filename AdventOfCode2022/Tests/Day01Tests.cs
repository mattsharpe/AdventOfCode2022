using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day01Tests
    {
        private string Sample =
            "1000\r\n2000\r\n3000\r\n\r\n4000\r\n\r\n5000\r\n6000\r\n\r\n7000\r\n8000\r\n9000\r\n\r\n10000";
    
        private string Input = File.ReadAllText("Input/Day01.txt");

        private Day01 _day01 = new();
        
        [TestInitialize]
        public void Initialize()
        {
            _day01 = new Day01();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(24000, _day01.Part1(Sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(72602, _day01.Part1(Input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual(45000, _day01.Part2(Sample));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(207410, _day01.Part2(Input));
        }
    }
}
