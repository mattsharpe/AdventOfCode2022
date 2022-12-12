using AdventOfCode2022.Solutions;

namespace AdventOfCode2022.Tests
{
    [TestClass]
    public class Day10Tests
    {
        private readonly string[] _sample = File.ReadAllLines("Input/Day10Sample.txt");

        private readonly string[] _input = File.ReadAllLines("Input/Day10.txt");

        private Day10 _day = new();

        [TestInitialize]
        public void Initialize()
        {
            _day = new Day10();
        }

        [TestMethod]
        public void Part1Sample()
        {
            Assert.AreEqual(13140, _day.Part1(_sample));
        }

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(13740, _day.Part1(_input));
        }

        [TestMethod]
        public void Part2Sample()
        {
            var expected = "##..##..##..##..##..##..##..##..##..##..\r\n" +
                           "###...###...###...###...###...###...###.\r\n" +
                           "####....####....####....####....####....\r\n" +
                           "#####.....#####.....#####.....#####.....\r\n" +
                           "######......######......######......####\r\n" +
                           "#######.......#######.......#######.....";
            
            Assert.AreEqual(expected, _day.Part2(_sample));
        }

        [TestMethod]
        public void Part2()
        {
            var result = "####.#..#.###..###..####.####..##..#....\r\n" +
                         "...#.#..#.#..#.#..#.#....#....#..#.#....\r\n" +
                         "..#..#..#.#..#.#..#.###..###..#....#....\r\n" +
                         ".#...#..#.###..###..#....#....#....#....\r\n" +
                         "#....#..#.#....#.#..#....#....#..#.#....\r\n" +
                         "####..##..#....#..#.#....####..##..####.";
            
            Assert.AreEqual(result, _day.Part2(_input));
        }
    }
}