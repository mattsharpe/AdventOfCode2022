using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day05Tests
    {
        private readonly string[] _sample = {    
"    [D]    ",
"[N] [C]    ",
"[Z] [M] [P]",
" 1   2   3 ",
"",
"move 1 from 2 to 1",
"move 3 from 1 to 3",
"move 2 from 2 to 1",
"move 1 from 1 to 2",
            
        };

        private readonly string[] _input = File.ReadAllLines("Input/Day05.txt");

        private Day05 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day05();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual("CMZ", _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual("RLFNRTNFB", _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            Assert.AreEqual("MCD", _day.Part2(_sample));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual("MHQTLJRLB", _day.Part2(_input));
        }
    }
}