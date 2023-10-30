using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day21Tests
    {
        private readonly string[] _sample = 
        {
            "root: pppw + sjmn",
            "dbpl: 5",
            "cczh: sllz + lgvd",
            "zczc: 2",
            "ptdq: humn - dvpt",
            "dvpt: 3",
            "lfqf: 4",
            "humn: 5",
            "ljgn: 2",
            "sjmn: drzm * dbpl",
            "sllz: 4",
            "pppw: cczh / lfqf",
            "lgvd: ljgn * ptdq",
            "drzm: hmdt - zczc",
            "hmdt: 32",
        };

        private readonly string[] _input = File.ReadAllLines("Input/Day21.txt");

        private Day21 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day21();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(152, _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(87457751482938, _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual(301, _day.Part2(_sample));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(3221245824363, _day.Part2(_input));
        }
    }
}