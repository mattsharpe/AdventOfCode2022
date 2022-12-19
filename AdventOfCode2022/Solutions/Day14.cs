
using System.Text;

namespace AdventOfCode2022.Solutions
{
    internal class Day14
    {
        public int Part1(string[] input)
        {
            var map = ParseInput(input);
            
            AddSand(map);

            return map.Values.Count(x=> x == 'o');
        }

        public int Part2(string[] input)
        {
            var map = ParseInput(input);
            
            AddSand(map, true);

            return map.Values.Count(x => x == 'o');
        }

        private Dictionary<(int X, int Y), char> ParseInput(string[] input)
        {
            var map = new Dictionary<(int X, int Y), char>();

            foreach (var line in input)
            {
                var walls = line.Split(" -> ").Select(x =>
                {
                    var lineDefinition = x.Split(",").Select(int.Parse).ToArray();
                    return (X: lineDefinition.First(), Y: lineDefinition.Last());
                }).ToArray();

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

        void AddSand(Dictionary<(int X, int Y), char> map, bool part2 = false)
        {
            var sandSource = (500, 0);
            var maxY = map.Keys.Select(k => k.Y).Max();

            //This drops a grain of sand and models it until it lands somewhere
            (int X, int Y) DropGrainOfSand((int X, int Y) location)
            {
                while (true)
                {
                    var (x, y) = location;
                    
                    //if this falls off the bottom
                    if (y > maxY)
                    {
                        //for part 2 let it hit the floor, otherwise it falls forever
                        return part2 ? (x, y) : (x, int.MinValue);
                    } 
                    
                    //if directly below is open
                    if (!map.ContainsKey((x, y + 1)))
                    {
                        location.Y += 1;
                    } 

                    //if not down to the left
                    else if (!map.ContainsKey((x - 1, y + 1)))
                    {
                        location.X -= 1;
                        location.Y += 1;
                    } 
                    //if not down to the right
                    else if (!map.ContainsKey((x + 1, y + 1)))
                    {
                        location.X += 1;
                        location.Y += 1;
                    }
                    else
                    {
                        return (x, y);
                    }

                }
            }
            //drops a grain of sand to see where it lands
            var position = DropGrainOfSand(sandSource);
            
            //While it's still piling up, drop another
            while (position.Y != int.MinValue)
            {
                //record in the map
                map[position] = 'o';

                if (position != sandSource)
                {
                    position = DropGrainOfSand(sandSource);
                    continue;
                }

                break;
            }
        }
    }
}