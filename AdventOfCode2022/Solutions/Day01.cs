namespace AdventOfCode2022.Solutions
{
    internal class Day01
    {
        public int Part1(string input)
        {
            var elves = ParseInput(input);
            return elves.Max();
        }

        public int Part2(string input)
        {
            var elves = ParseInput(input);
            return elves.OrderDescending().Take(3).Sum();
        }

        public IEnumerable<int> ParseInput(string input)
        {
            return input.Split($"{Environment.NewLine}{Environment.NewLine}")
                .Select(x => x.Split(Environment.NewLine).Select(int.Parse))
                .Select(x => x.Sum());
        }
    }
}
