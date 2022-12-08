using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day07Tests
    {
        private readonly string[] _sample =
        {
            "$ cd /",
            "$ ls",
            "dir a",
            "14848514 b.txt",
            "8504156 c.dat",
            "dir d",
            "$ cd a",
            "$ ls",
            "dir e",
            "29116 f",
            "2557 g",
            "62596 h.lst",
            "$ cd e",
            "$ ls",
            "584 i",
            "$ cd ..",
            "$ cd ..",
            "$ cd d",
            "$ ls",
            "4060174 j",
            "8033020 d.log",
            "5626152 d.ext",
            "7214296 k"
        };

        private readonly string[] _input = File.ReadAllLines("Input/Day07.txt");

        private Day07 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day07();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(95437, _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(1783610, _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual(24933642, _day.Part2(_sample));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(4370655, _day.Part2(_input));
        }
    }
}