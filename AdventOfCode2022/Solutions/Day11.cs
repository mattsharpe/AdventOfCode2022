using System.Text.RegularExpressions;

namespace AdventOfCode2022.Solutions
{
    internal class Day11
    {
        public int Part1(string[] input)
        {
            ParseInput(input).ToArray();
            return 0;

        }
        
        public int Part2(string[] input)
        {
            return 0;
        }
        private IEnumerable<Monkey> ParseInput(string[] input)
        {
            var monkeys = input.Chunk(7);
            foreach (var chunkyMonkey in monkeys)
            {
                yield return ParseMonkey(chunkyMonkey);
            }
        }

        private Monkey ParseMonkey(string[] input)
        {
            var monkey = new Monkey();
            Console.WriteLine(string.Join(Environment.NewLine, input));
            //Monkey 0:
            monkey.Id = int.Parse(Regex.Match(input[0], @"(\d+)").Value);

            //Starting items: 79, 98
            monkey.Items.AddRange(input[1].Split(": ")[1].Split(",").Select(int.Parse));
            
            
            //Operation: new = old * 19
            monkey.Operation = input[2].Split(": ")[1];
            
            //Test: divisible by 23
            monkey.DivisibleBy = int.Parse(Regex.Match(input[3], @"(\d+)").Value);

            //If true: throw to monkey 2
            monkey.MonkeyIfTrue = int.Parse(Regex.Match(input[4], @"(\d+)").Value);

            //If false: throw to monkey 3
            monkey.MonkeyIfFalse = int.Parse(Regex.Match(input[5], @"(\d+)").Value);

            Console.WriteLine(monkey);

            return monkey;
        }
    }

    class Monkey
    {
        public int Id { get; set; }
        public List<int> Items { get; } = new();
        public string Operation { get; set; }
        public int DivisibleBy { get; set; }
        public int MonkeyIfTrue { get; set; }
        public int MonkeyIfFalse { get; set; }

        public override string ToString()
        {
            return @$"
Monkey {Id}:
  Starting items: {string.Join(",",Items)}
  Operation: {Operation}
  Test: divisible by {DivisibleBy}
    If true: throw to monkey {MonkeyIfTrue}
    If false: throw to monkey {MonkeyIfFalse}";

        }
    }
}