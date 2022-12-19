
using System.Text;

namespace AdventOfCode2022.Solutions
{
    internal class Day14
    {
        public int Part1(string[] input)
        {
            var map = ParseInput(input);

            Console.WriteLine(Print(map));
            return 0;
        }

        public int Part2(string[] input)
        {
            return 0;
        }

        private Dictionary<(int X, int Y), char> ParseInput(string[] input)
        {
            var map = new Dictionary<(int X, int Y), char>();

            foreach (var line in input)
            {
                var walls = line.Split(" -> ").Select(x =>
                {
                    var lineDefinition = x.Split(",").Select(int.Parse);
                    return (X: lineDefinition.First(), Y: lineDefinition.Last());
                });

                var pairs = walls.Zip(walls.Skip(1));

                foreach(var (start, end) in pairs)
                {
                    var minX = Math.Min(start.X, end.X);
                    var maxX = Math.Max(start.X, end.X);
                    var minY = Math.Min(start.Y, end.Y);
                    var maxY = Math.Max(start.Y, end.Y);

                    for (var y = minY; y <= maxY; y++)
                    {
                        for (var x = minX; x <= maxX; x++)
                        {
                            map[(x, y)] = '#';
                        }
                    }
                }
            }
            return map;
        }

        private string Print(Dictionary<(int X, int Y), char> map)
        {
            var minX = map.Keys.Select(k => k.X).Min();
            var maxX = map.Keys.Select(k => k.X).Max();
            var minY = map.Keys.Select(k => k.Y).Min();
            var maxY = map.Keys.Select(k => k.Y).Max();

            var output = new StringBuilder();
            for (var y = minY; y <= maxY; y++)
            {
                var line = new StringBuilder();
                for (var x = minX; x <= maxX; x++)
                {
                    if (map.TryGetValue((x, y), out char value))
                    {
                        line.Append(value);
                    }
                    else
                    {
                        line.Append('.');
                    }
                }
                output.AppendLine(line.ToString());
            }

            return output.ToString();
        }

    }
}