namespace AdventOfCode2022.Solutions
{
    internal class Day06
    {
        public int Part1(string input)
        {
            return FindPosition(input, 4);
        }

        public int Part2(string input)
        {
            return FindPosition(input, 14);
        }

        private int FindPosition(string input, int size)
        {
            for (var i = size; i < input.Length; i++)
            {
                var previous = input.Substring(i - size, size);
                if (previous.Distinct().Count() == size)
                {
                    return i;
                }
            }

            throw new Exception("Not found");
        }
    }
}