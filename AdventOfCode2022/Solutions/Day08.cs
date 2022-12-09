namespace AdventOfCode2022.Solutions
{
    internal class Day08
    {
        public int Part1(string[] input)
        {
            var trees = ParseInput(input);
            return CountVisible(trees);
        }

        public int Part2(string[] input)
        {
            return CalculateScenicScore(ParseInput(input));
        }

        private int CountVisible(int[,] trees)
        {
            // all of the outer edges are visible so include them in our count.
            // 2* each edge, minus the 4 corners that have been double counted
            var count = 2 * trees.GetLength(1) + 2 * trees.GetLength(0) - 4;

            for (var y = 1; y < trees.GetLength(1) - 1; y++)
            {
                for (var x = 1; x < trees.GetLength(0) - 1; x++)
                {
                    var height = trees[x, y];
                    var visible = true;


                    //assume the tree is visible then check all directions

                    //north
                    for (int i = 0; i < y; i++)
                    {
                        if (trees[x, i] >= height)
                        {
                            visible = false;
                        }
                    }

                    if (visible)
                    {
                        count++;
                        continue;
                    }

                    //south 
                    visible = true;
                    for (int i = y + 1; i < trees.GetLength(1); i++)
                    {
                        if (trees[x, i] >= height)
                        {
                            visible = false;
                        }
                    }

                    if (visible)
                    {
                        count++;
                        continue;
                    }

                    //east
                    visible = true;
                    for (int i = x + 1; i < trees.GetLength(0); i++)
                    {
                        if (trees[i, y] >= height)
                        {
                            visible = false;
                        }
                    }

                    if (visible)
                    {
                        count++;
                        continue;
                    }

                    //west
                    visible = true;
                    for (var i = 0; i < x; i++)
                    {
                        if (trees[i, y] >= height)
                        {
                            visible = false;
                        }
                    }

                    if (visible)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public int[,] ParseInput(string[] input)
        {
            var map = new int[input[0].Length, input.Length];

            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    map[x, y] = int.Parse("" + input[y][x]);
                }
            }
            return map;
        }

        private int CalculateScenicScore(int[,] trees)
        {
            var max = 0;

            for (var y = 0; y < trees.GetLength(1) - 1; y++)
            {
                for (var x = 0; x < trees.GetLength(0) - 1; x++)
                {
                    var height = trees[x, y];
                    var north = 0;
                    var south = 0;
                    var east = 0;
                    var west = 0;

                    //north
                    for (int i = y - 1; i >= 0; i--)
                    {
                        north++;
                        if (trees[x, i] >= height)
                        {
                            break;
                        }
                    }

                    //south 
                    for (int i = y + 1; i < trees.GetLength(1); i++)
                    {
                        south++;
                        if (trees[x, i] >= height)
                        {
                            break;
                        }
                    }

                    //east
                    for (int i = x + 1; i < trees.GetLength(0); i++)
                    {
                        east++;
                        if (trees[i, y] >= height)
                        {
                            break;
                        }
                    }

                    //west
                    for (int i = x - 1; i >= 0; i--)
                    {
                        west++;
                        if (trees[i, y] >= height)
                        {
                            break;
                        }
                    }

                    max = Math.Max(max, north * south * east * west);
                }
            }

            return max;
        }
    }
}