using System.Text.RegularExpressions;

namespace AdventOfCode2022.Solutions
{
    internal class Day11
    {
        public long Part1(string[] input)
        {
            var monkeys = ParseInput(input).ToArray();
            foreach (var _ in Enumerable.Range(0, 20))
            {
                ProcessTurn(monkeys);
            }
            var monkeyBusiness = monkeys.OrderByDescending(x => x.InspectedCount).Take(2)
                .Aggregate(1L, (i, monkey) => i * monkey.InspectedCount);
            
            return monkeyBusiness;
        }

        public long Part2(string[] input)
        {
            var monkeys = ParseInput(input).ToArray();
            
            foreach (var _ in Enumerable.Range(0, 10000))
            { 
                ProcessTurn(monkeys, true);
            }

            var monkeyBusiness = monkeys.OrderByDescending(x => x.InspectedCount).Take(2)
                .Aggregate(1L, (i, monkey) => i * monkey.InspectedCount);

            return monkeyBusiness;
        }

        public void ProcessTurn(Monkey[] monkeys, bool part2 = false)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Any())
                {
                    var currentItem = monkey.Items.Dequeue();

                    // inspect and update worry level
                    monkey.InspectedCount++;
                    var newValue = monkey.Operation(currentItem);

                    if (part2)
                    {
                        //figure out a least common multiple for all monkeys that we can use as the mod
                        // we need this to ensure it will work safely for all monkeys
                        var mod = monkeys.Aggregate(1, (a, b) => a * b.DivisibleBy);
                        newValue %= mod;
                    }
                    else
                    {
                        // divide worry level by 3
                        newValue /= 3;
                    }

                    // test worry level and dispatch
                    var targetMonkey = newValue % monkey.DivisibleBy == 0
                        ? monkeys[monkey.MonkeyIfTrue]
                        : monkeys[monkey.MonkeyIfFalse];

                    targetMonkey.Items.Enqueue(newValue);
                }
            }
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

            var monkey = new Monkey
            {
                Id = int.Parse(Regex.Match(input[0], @"(\d+)").Value),
                Items = new Queue<long>(input[1].Split(": ")[1].Split(",").Select(long.Parse)),
                DivisibleBy = int.Parse(Regex.Match(input[3], @"(\d+)").Value),
                MonkeyIfTrue = int.Parse(Regex.Match(input[4], @"(\d+)").Value),
                MonkeyIfFalse = int.Parse(Regex.Match(input[5], @"(\d+)").Value)
            };

            //Operation: new = old * 19
            var instruction = input[2].Split("=")[1].Trim();
            if (instruction == "old * old")
            {
                monkey.Operation = x => x * x;
            }
            else
            {
                var value = int.Parse(Regex.Match(instruction, @"(\d+)").Value);

                if (instruction.Contains("*"))
                {
                    monkey.Operation = x => x * value;
                }
                else if (instruction.Contains("+"))
                {
                    monkey.Operation = x => x + value;
                }
            }

            return monkey;
        }
    }

    class Monkey
    {
        public int Id { get; set; }
        public Queue<long> Items { get; set; } = new();
        public Func<long, long> Operation { get; set; }
        public int DivisibleBy { get; set; }
        public int MonkeyIfTrue { get; set; }
        public int MonkeyIfFalse { get; set; }
        public long InspectedCount { get; set; }
    }
}