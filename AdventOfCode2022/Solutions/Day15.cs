using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Solutions
{
    internal class Day15
    {
        public int Part1(string[] input, int y = 2_000_000)
        {
            var sensors = ParseInput(input).ToList();
            return PositionsWithoutABeacon(sensors, y);

        }
        public long Part2(string[] input)
        {
            var sensors = ParseInput(input).ToList();
            return Part2(sensors);
        }

        private IEnumerable<SensorBeacon> ParseInput(string[] input)
        {
            var sensors = input.Select(x =>
            {
                var numbers = Regex.Matches(x, @"-?\d+",RegexOptions.Compiled).Select(m => int.Parse(m.Value)).ToArray();
                return new SensorBeacon(
                    new Location(numbers[0], numbers[1]),
                    new Location(numbers[2], numbers[3]));
            });
            return sensors;
        }
        private int PositionsWithoutABeacon(IEnumerable<SensorBeacon> sensors, int row)
        {
            //get the distance of each sensor from the target row
            var distances = sensors
                .Select(sensor => 
                    (
                        sensor.Location.X,
                        distance: sensor.Size - Math.Abs(row - sensor.Location.Y)
                    )
                );

            //get the range of coordinates that are covered by beacons - we know there won't be any beacons in here.
            //This should only be sensors that are covering our target range in the row specified
            var ranges = distances.Where(s => s.distance >= 0)
                .Select(s => (min: s.X - s.distance, max: s.X + s.distance))
                .OrderBy(s => s.min);

            var noBeacons = new List<(int min, int max)>();

            foreach (var range in ranges)
            {
                if (noBeacons.Count != 0 && range.min <= noBeacons.Last().max + 1)
                {
                    var result = (noBeacons.Last().min, Math.Max(range.max, noBeacons.Last().max));
                    noBeacons[^1] = result;
                }
                else
                {
                    noBeacons.Add(range);
                }
            }

            var beacons = sensors
                .Where(s => s.NearestBeacon.Y == row)
                .Select(s => s.NearestBeacon.X)
                .Distinct()
                .ToList();

            var potential=  noBeacons
                .Select(r => r with { max = r.max - beacons.Count(x => x>= r.min && x<= r.max) });
            
            return potential.Sum(x=>x.max - x.min +1);
            
        }
        
        private static bool IsPointInRange(Location point, (Location midpoint, int r) range) => 
            point.ManhattanDistance(range.midpoint) <= range.r;

        private long Part2(IEnumerable<SensorBeacon> pairs)
        {
            HashSet<Location> sensors = new();
            HashSet<Location> beacons = new();
            //Model the covered area as a range with radius of the manhattan distance between sensor and beacon
            List<(Location Midpoint, int Radius)> ranges = new();
            
            foreach (var pair in pairs)
            {
                sensors.Add(pair.Location);
                beacons.Add(pair.NearestBeacon);
                ranges.Add((pair.Location, pair.Size));
            }

            var lower = 0;
            var upper = 4000000;
            HashSet<Location> toCheck = new();

            for (var i = 0; i < ranges.Count; i++)
            {
                var (location, radius1) = ranges[i];

                for (var j = 0; j < ranges.Count; j++)
                {
                    var (midpoint, radius) = ranges[j];
                    
                    if (i == j || location.ManhattanDistance(midpoint) != radius1 + radius + 2) continue;

                    var maxY = Math.Min(location.Y + radius1, midpoint.Y + radius);
                    var minY = Math.Max(location.Y - radius1, midpoint.Y - radius);

                    var maxX = Math.Max(location.X - radius1, midpoint.X - radius);
                    var minX = Math.Min(location.X + radius1, midpoint.X + radius);

                    for (var y = minY; y < maxY; y++)
                    {
                        var x1 = location.X + (radius1 + 1 - Math.Abs(y - location.Y));
                        var x2 = location.X - (radius1 + 1 - Math.Abs(y - location.Y));

                        if (x1 >= lower && x1 <= upper && x1 >= maxX && x1 <= minX)
                        {
                            toCheck.Add(new Location(x1, y));
                        }
                        if (x2 >= lower && x2 <= upper && x2 >= maxX && x2 <= minX)
                        {
                            toCheck.Add(new Location(x2, y));
                        }
                    }
                }
            }

            long result = 0;

            foreach (var location in toCheck)
            {
                if (sensors.Contains(location) || beacons.Contains(location) )
                {
                    continue;
                }
                var found = ranges.All(range => !IsPointInRange(location, range));
                if (found)
                {
                    result = location.X * 4000000L + location.Y;
                    break;
                }
            }
            return result;
        }

    }
    
    record SensorBeacon(Location Location, Location NearestBeacon)
    {
        public int Size = Location.ManhattanDistance(NearestBeacon);
        
    }

    record Location(int X, int Y)
    {
        public int ManhattanDistance(Location other)
            => Math.Abs(other.X - X) + Math.Abs(other.Y - Y);
    }
}