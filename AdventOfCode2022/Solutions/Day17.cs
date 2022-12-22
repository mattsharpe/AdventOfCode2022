

namespace AdventOfCode2022.Solutions
{
    internal class Day17
    {
        public long Part1(string input)
        {
            //build into queues of shapes to fall and jets to fire
            //process the game over the grid
            // assume we need an absurd number of blocks for part 2 - find the pattern and calculate from there?
            
            var game = ParseInput(input);

            foreach (var _ in Enumerable.Range(0, 2022))
            {
                ProcessTurn(game);
            }
            
            return game.Height;
        }

        public long Part2(string input)
        {
            var game = ParseInput(input);
            var count = 1000000000000;

            DropRocks(count, game);
            
            return game.Height;
        }

        private TetrisGame ParseInput(string input)
        {
            var blocks= new[]
            {
                new[] { "####" },
                new[]
                {
                    ".#.",
                    "###",
                    ".#."
                },
                new[]
                {
                    "..#",
                    "..#",
                    "###"
                },
                new[]
                {
                    "#",
                    "#",
                    "#",
                    "#"
                },
                new[]
                {
                    "##",
                    "##",
                }
            };

            return new TetrisGame(
                new Queue<char>(input.Select(x => x)),
                new Queue<string[]>(blocks),
                new List<string>()
            );
        }
        

        private void DropRocks(long leftToDrop, TetrisGame game)
        {
            var visited = new Dictionary<string, (long leftToDrop, long height)>();
            
            while (leftToDrop > 0)
            {
                //grab a hash of the current state of the game - we limit the playing area so this should be safe.
                var hash = string.Join("",game.Lines);
                
                if (visited.TryGetValue(hash, out var cache))
                {
                    //we can recognise this state so there's a pattern, work out how much we increased during this pattern
                    var heightInPattern = game.Height - cache.height;

                    //an how often the pattern repeats
                    var rocksInPattern = cache.leftToDrop - leftToDrop;

                    game.PreviousLineCount += (leftToDrop / rocksInPattern) * heightInPattern;
                    leftToDrop %= rocksInPattern;
                    break;
                }

                visited[hash] = (leftToDrop, game.Height);
                ProcessTurn(game);
                leftToDrop--;
            }

            //we've skipped as far as we can so now drop the rest of the rocks
            while (leftToDrop > 0)
            {
                ProcessTurn(game);
                leftToDrop--;
            }
        }

        private void ProcessTurn(TetrisGame game)
        {
            //The tall, vertical chamber is exactly seven units wide.
            //Each rock appears so that its left edge is two units away from the left wall
            //and its bottom edge is three units above the highest rock in the room (or the floor, if there isn't one).

            bool Collides(string[] rock, (int x, int y) location)
            {
                IEnumerable<(int x, int y)> toCheck = GetSolidPointsOfRock(rock).Select(point => (point.x + location.x, point.y + location.y));
                
                var result = toCheck.Any(block =>
                {
                    if(block.x is < 0 or > 6) return true;
                    var line = game.Lines.Count == block.y ? "#######" : game.Lines[block.y];
                    var cell = line.ToCharArray()[block.x];
                    return cell == '#';
                });

                return result;

            }

            void AddBlockToMap(string[] rock, (int x, int y) location)
            {
                IEnumerable<(int x, int y)> solid = GetSolidPointsOfRock(rock).Select(point => (point.x + location.x, point.y + location.y));
                
                foreach (var (x, y) in solid)
                {
                    if (x > 6) throw new Exception("How did we get a block outside the game?");

                    var array = game.Lines[y].ToCharArray();
                    array[x] = '#';

                    game.Lines[y] = new string(array);
                }
            }

            var rock = game.Blocks.Dequeue();
            game.Blocks.Enqueue(rock);

            //add empty lines for size of rock + 3 extras
            foreach (var _ in Enumerable.Range(0, rock.Length + 3))
            {
                game.Lines.Insert(0, ".......");
            }

            var location = (x: 2, y: 0);
            
            while (true)
            {
                var jet = game.Jets.Dequeue();
                game.Jets.Enqueue(jet);

                switch (jet)
                {
                    case '<' when !Collides(rock, location with { x = location.x - 1 }):
                        location.x--;
                        break;
                    case '>' when !Collides(rock, location with { x = location.x + 1 }):
                        location.x++;
                        break;
                }

                //this is the bottom
                if (Collides(rock, location with { y = location.y + 1 }))
                {
                    break;
                }
                
                location.y++;
            }

            AddBlockToMap(rock, location);
            //remove the empty rows at the top
            game.Lines.RemoveAll(x => x == ".......");

            if (game.Lines.Count > 50)
            {
                game.PreviousLineCount += game.Lines.Count - 50;
                game.Lines = game.Lines.Take(50).ToList();
            }

            //Print(game);
        }

        //Get the locations in the rock that are solid '#'
        //Assume rock starts at 0,0, we can then transform from there
        private IEnumerable<(int x, int y)> GetSolidPointsOfRock(string[] rock)
        {
            for (var y = 0; y < rock.Length; y++)
            {
                for (var x = 0; x < rock[y].Length; x++)
                {
                    if (rock[y][x] == '#')
                    {
                        yield return (x, y);
                    }
                }
            }
        }

        private void Print(TetrisGame game)
        {
            foreach (var line in game.Lines)
            {
                Console.WriteLine(line);
            }
        }
        
    }
    class TetrisGame
    {
        public Queue<char> Jets { get; }
        public Queue<string[]> Blocks { get; }
        public List<string> Lines { get; set; }
        public long PreviousLineCount { get; set; }

        public TetrisGame(Queue<char> jets, Queue<string[]> blocks, List<string> lines)
        {
            Jets = jets;
            Blocks = blocks;
            Lines = lines;
        }

        public long Height => PreviousLineCount + Lines.Count;
    }
}