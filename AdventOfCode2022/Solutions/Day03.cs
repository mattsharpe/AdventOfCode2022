
namespace AdventOfCode2022.Solutions
{
    internal class Day03
    {
        public int Part1(string[] input)
        {
            return input.Select(ProcessLine).Sum();
        }

        public int Part2(string[] input)
        {
            var chunks = input.Chunk(3);
            return chunks.Select(FindCommonItem).Sum();
        }


        private int ProcessLine(string line)
        {
            var chunks = line.Chunk(line.Length / 2);
            return FindCommonItem(chunks);
        }

        private int FindCommonItem(IEnumerable<IEnumerable<char>> chunks)
        {

            var first = chunks.First().ToHashSet();
            foreach(var set in chunks)
            {
                first.IntersectWith(set);
            }
            var item = first.Single();

            //map the char codes to 1..26 for a-z and 27..52 for A-Z
            return item < 'a' ? item - 38 : item - 96;
        }
    }
}