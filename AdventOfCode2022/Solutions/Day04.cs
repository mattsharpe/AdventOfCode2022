namespace AdventOfCode2022.Solutions
{
    internal class Day04
    {
        public int Part1(string[] input)
        {
            return input.Count(FullyContained);
        }

        public int Part2(string[] input)
        {
            return input.Count(Overlaps);
        }

        private bool Overlaps(string pair)
        {
            var ranges = GetPairOfRanges(pair);

            bool overlaps(Range first, Range second) =>
                first.upper >= second.lower && first.lower <= second.upper;

            return overlaps(ranges.Item1, ranges.Item2) ||
                overlaps(ranges.Item2, ranges.Item1);
        }

        private bool FullyContained(string pair)
        {
            var ranges = GetPairOfRanges(pair);

            //true if first is contained in second
            bool contains(Range first, Range second) =>
                first.lower >= second.lower && first.upper <= second.upper;

            var result = contains(ranges.Item1, ranges.Item2) ||
                contains(ranges.Item2, ranges.Item1);

            return result;
        }

        private static (Range, Range) GetPairOfRanges(string pair)
        {
            var pairs = pair.Split(",")
                .Select(range =>
                {
                    var numbers = range.Split("-").Select(int.Parse);
                    return new Range(numbers.First(), numbers.Last());
                });

            return (pairs.First(), pairs.Last());
        }
    }

    record Range(int lower, int upper);
}