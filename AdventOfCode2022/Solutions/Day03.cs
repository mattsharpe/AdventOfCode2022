
namespace AdventOfCode2022.Solutions
{
    internal class Day03
    {
        public int Part1(string[] input)
        {
            ParseInput(input);
            return input.Select(ProcessLine).Sum();
            
        }

        public int Part2(string[] input)
        {
            return 0;
        }

        public void ParseInput(string[] input)
        {
            foreach(var line in input)
            {
                ProcessLine(line);
            }
        }

        private int ProcessLine(string line)
        {
            var chunks = line.ToList().Chunk(line.Length / 2);
            var thing = chunks.Select(x=> x.ToHashSet());

            var first = chunks.First().ToArray().ToHashSet();
            var second = chunks.Last().ToArray().ToHashSet();

            var item = first.Intersect(second).Single();

            //map the char codes to 1..26 for a-z and 27..52 for A-Z
            return item < 'a' ? item - 38 : item - 96;
        }
    }
}