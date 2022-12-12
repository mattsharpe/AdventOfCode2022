
namespace AdventOfCode2022.Solutions
{
    internal class Day09
    {
        public int Part1(string[] input)
        {
            return SwingRope(input, 2);
        }

        public int Part2(string[] input)
        {
            return SwingRope(input, 10);
        }
        private int SwingRope(string[] input, int ropeLength)
        {
            var knots = Enumerable.Repeat((x:0, y:0), ropeLength).ToList();
            var tailVisited = new HashSet<(int x, int y)>{ knots.Last() };

            foreach (var line in input)
            {
                var direction = line.Split(" ")[0];
                var distance = int.Parse(line.Split(" ")[1]);

                foreach (var _ in Enumerable.Range(0,distance))
                {
                    var (dx, dy) = _moves[direction];
                    var head = knots[0];
                    knots[0] = (head.x + dx, head.y + dy);

                    //move the next knot in the line until we reach then end
                    for (int i = 0; i < knots.Count-1; i++)
                    {
                        knots[i+1] = MoveTail(knots[i], knots[i + 1]);
                    }

                    tailVisited.Add(knots.Last());
                }
            }

            return tailVisited.Count;
        }

        (int x, int y) MoveTail((int x, int y) head, (int x, int y) tail)
        {
            //if we're far enough away, the next knot will need to move.
            var dX = Math.Abs(head.x - tail.x);
            var dY = Math.Abs(head.y - tail.y);

            if (!(Math.Max(dX, dY) > 1))
            {
                return tail;
            }

            (int x, int y) result;

            if (head.x > tail.x)
            {
                result = tail with { x = tail.x + 1 };
            }
            else if (head.x < tail.x)
            {
                result = tail with { x = tail.x - 1 };
            }
            else
            {
                result = tail;
            }

            if ( head.y > result.y)
            {
                result = result with { y = result.y + 1 };
            }
            else if (head.y < result.y)
            {
                result = result with { y = result.y - 1 };
            }

            return result;

        }

        private readonly Dictionary<string, (int x, int y)> _moves = new()
        {
            { "U", (0, 1) },
            { "D", (0, -1) },
            { "L", (-1, 0) },
            { "R", (1, 0) },
        };
    }
}