using System.Numerics;
using Map = System.Collections.Generic.Dictionary<System.Numerics.Vector2, char>;

namespace AdventOfCode2022.Solutions
{
    internal class Day12
    {
        public int Part1(string[] input)
        {
            var map = ParseInput(input);
            var s = map.Single(x => x.Value == 'S');
            var e = map.Single(x => x.Value == 'E');
            
            return ShortestPathBetween(s.Key, e.Key, map);

        }

        public int Part2(string[] input)
        {
            var map = ParseInput(input); 
            
            var e = map.Single(x => x.Value == 'E').Key;

            return map.Where(x => x.Value == 'a').Select(x => ShortestPathBetween(x.Key, e, map)).Min();
        }

        public int ShortestPathBetween(Vector2 start, Vector2 end, Map map)
        {
            Dictionary<Vector2, int> distances = new() { { start, 0 } };

            Queue<Vector2> toExplore= new(new[] {start});
            
            while (toExplore.Any())
            {
                var current = toExplore.Dequeue();
                if (current == end) return distances[end];

                foreach (var neighbour in Neighbours(current).Where(map.ContainsKey))
                {
                 
                    if (distances.ContainsKey(neighbour) ||
                        GetElevation(map[neighbour]) - GetElevation(map[current]) > 1)
                    {
                        continue;
                    }
                    
                    distances[neighbour] = distances[current] + 1;
                    toExplore.Enqueue(neighbour);
                }
            }
            
            return distances.ContainsKey(end)? distances[end] : int.MaxValue;
        }

        char GetElevation(char input)
        {
            return input switch
            {
                'S' => 'a',
                'E' => 'z',
                _ => input
            };
        }

        Map ParseInput(string[] input)
        {
            Map map = new();
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    map[new Vector2(x, y)] = input[y][x];
                }
            }

            return map;
        }

        Vector2[] Neighbours(Vector2 start)
        {
           return new[]
            {
                start with { X = start.X - 1 },
                start with { X = start.X + 1 },
                start with { Y = start.Y - 1 },
                start with { Y = start.Y + 1 }
            };
        }
    }
}