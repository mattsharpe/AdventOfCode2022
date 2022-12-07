using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day06Tests
    {
        private readonly string _input = File.ReadAllText("Input/Day06.txt");

        private Day06 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day06();
        }

        [DataTestMethod]
        [DataRow("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
        [DataRow("nppdvjthqldpwncqszvftbrmjlhg", 6)]
        [DataRow("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
        [DataRow("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
        public void Part1Sample(string input, int expected)
        {
            Assert.AreEqual(expected, _day.Part1(input));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(1833, _day.Part1(_input));
        }

        [DataTestMethod]
        [DataRow("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
        [DataRow("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
        [DataRow("nppdvjthqldpwncqszvftbrmjlhg", 23)]
        [DataRow("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
        [DataRow("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
        public void Part2Sample(string input, int expected)
        {
            Assert.AreEqual(expected, _day.Part2(input));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(3425, _day.Part2(_input));
        }
    }
}