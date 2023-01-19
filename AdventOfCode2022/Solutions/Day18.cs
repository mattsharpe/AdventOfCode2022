using System.Numerics;
namespace AdventOfCode2022.Solutions
{
    internal class Day18
    {
        public int Part1(string[] input)
        {
            var cubes = ParseInput(input);
            
            //foreach point we have, get all of it's neighbours
            var allEdges = new List<Vector3>();
            foreach (var cube in cubes)
            {
                allEdges.AddRange(GetNeighbours(cube));
            }

            return allEdges.Count(x => !cubes.Contains(x));
            
        }

        public int Part2(string[] input)
        {
            var lava = ParseInput(input);

            var bounds = GetBounds(lava);
            var water = Flood(bounds.lower, bounds, lava);

            return lava.SelectMany(GetNeighbours).Count(water.Contains);
        }
        

        private HashSet<Vector3> Flood(Vector3 from, (Vector3 min, Vector3 max) bounds, HashSet<Vector3> lava)
        {
            var result = new HashSet<Vector3>();
            var queue = new Stack<Vector3>();

            result.Add(from);
            queue.Push(from);

            while (queue.Any())
            {
                var water = queue.Pop();

                foreach (var x in GetNeighbours(water).Where(x => !result.Contains(x) &&
                                                                          PointIsEnclosed(x, bounds.min, bounds.max) &&
                                                                          !lava.Contains(x)))
                {
                    result.Add(x);
                    queue.Push(x);
                }
            }

            return result;
        }

        private bool PointIsEnclosed(Vector3 point, Vector3 minBound, Vector3 maxBound)
        {
            return minBound.X <= point.X && point.X <= maxBound.X &&
                   minBound.Y <= point.Y && point.Y <= maxBound.Y &&
                   minBound.Z <= point.Z && point.Z <= maxBound.Z;
        }
        

        (Vector3 lower, Vector3 upper) GetBounds(HashSet<Vector3> points)
        {
            var lower = new Vector3(
                points.Min(x => x.X) - 1,
                points.Min(x => x.Y) - 1,
                points.Min(x => x.Z) - 1
            );

            var upper = new Vector3(
                points.Max(x => x.X) + 1,
                points.Max(x => x.Y) + 1,
                points.Max(x => x.Z) + 1
            );

            return (lower, upper);
        }


        private HashSet<Vector3> ParseInput(string[] input)
        {
            return input.Select(x =>
            {
                var split = x.Split(",").Select(int.Parse).ToArray();
                return new Vector3(split[0], split[1], split[2]);
            }).ToHashSet();
        }

        IEnumerable<Vector3> GetNeighbours(Vector3 Vector3)
        {
            yield return Vector3 with { X = Vector3.X + 1 };
            yield return Vector3 with { X = Vector3.X - 1 };
            yield return Vector3 with { Y = Vector3.Y + 1 };
            yield return Vector3 with { Y = Vector3.Y - 1 };
            yield return Vector3 with { Z = Vector3.Z + 1 };
            yield return Vector3 with { Z = Vector3.Z - 1 };
        }
    }
}