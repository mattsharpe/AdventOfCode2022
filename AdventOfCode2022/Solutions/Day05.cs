using System.Text.RegularExpressions;

namespace AdventOfCode2022.Solutions
{
    internal class Day05
    {
        public string Part1(string[] input)
        {
            var stacks = ParseInput(input);
            var instructions = ParseInstructions(input);

            MoveStacks(stacks, instructions);

            return string.Join("", stacks.Select(x => x.Value.Peek()));
            
        }

        public string Part2(string[] input)
        {
            var stacks = ParseInput(input);
            var instructions = ParseInstructions(input);

            MoveWithCrateMover9001(stacks, instructions);

            return string.Join("", stacks.Select(x => x.Value.Peek()));
        }



        private IEnumerable<(int number, int from , int to)> ParseInstructions(string[] input)
        {
            var regex = new Regex(@"move (\d+) from (\d+) to (\d+)");

            var matches = regex.Matches(string.Join("", input));

            foreach (var match in matches.Cast<Match>())
            {
                yield return 
                    (
                        int.Parse(match.Groups[1].Value),
                        int.Parse(match.Groups[2].Value),
                        int.Parse(match.Groups[3].Value)
                    );
            }

        }

        private Dictionary<int, Stack<char>> ParseInput(string[] input)
        {
            var stackState = input.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var stacks = stackState.Last()
                .Chunk(4)
                .ToDictionary(x => int.Parse(x),
                    x => new Stack<char>());

            foreach (var line in stackState.Reverse().Skip(1))
            {
                var chunks = line.Chunk(4).ToArray();
                for (var i = 0; i < chunks.Length; i++)
                {
                    var item = chunks[i][1];
                    if (item != ' ')
                    {
                        stacks[i + 1].Push(item);
                    }
                }
            }

            return stacks;

        }

        private void MoveStacks(Dictionary<int, Stack<char>> stacks, 
            IEnumerable<(int number, int from, int to)> instructions)
        {
            foreach (var instruction in instructions)
            {
                
                foreach (var _ in Enumerable.Range(0, instruction.number))
                {
                    var item = stacks[instruction.from].Pop();
                    stacks[instruction.to].Push(item);
                }
            }
        }

        private void MoveWithCrateMover9001(Dictionary<int, Stack<char>> stacks,
            IEnumerable<(int number, int from, int to)> instructions)
        {
            foreach (var instruction in instructions)
            {
                var middleMan = new Stack<char>();
                foreach (var _ in Enumerable.Range(0, instruction.number))
                {
                    middleMan.Push(stacks[instruction.from].Pop());
                }

                foreach (var item in middleMan)
                {
                    stacks[instruction.to].Push(item);
                }
            }
        }
    }
}