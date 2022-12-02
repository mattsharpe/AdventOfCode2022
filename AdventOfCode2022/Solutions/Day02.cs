using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Solutions
{

    internal class Day02
    {
        public int Part1(string[] input)
        {
            var moves = ParseInput(input);
            return moves.Sum(Score);
        }

        public int Part2(string[] input)
        {
            //neater approach for part 2
            return input.Select(x =>
                x switch
                {
                    "A X" => 3,
                    "A Y" => 4,
                    "A Z" => 8,
                    "B X" => 1,
                    "B Y" => 5,
                    "B Z" => 9,
                    "C X" => 2,
                    "C Y" => 6,
                    "C Z" => 7,
                    _ => throw new ArgumentOutOfRangeException(nameof(x), x, null)
                }).Sum();
        }

        private IEnumerable<(string, string)> ParseInput(string[] input)
        {
            return input.Select(line =>
            {
                var split = line.Split(' ');
                return (split[0], split[1]);
            });
        }

        private int Score((string opponent, string move) game)
        {
            int score = game.move switch
            {
                "X" => 1,
                "Y" => 2,
                "Z" => 3,
                _ => 0
            };
            
            var winning = new HashSet<(string,string)>
            {
                ("A", "Y"),
                ("B", "Z"),
                ("C", "X"),
            };
            var drawing = new HashSet<(string, string)>
            {
                ("A", "X"),
                ("B", "Y"),
                ("C", "Z"),
            };

            if (winning.Contains(game))
            {
                score += 6;
            }
            else if (drawing.Contains(game))
            {
                score += 3;
            }

            return score; 
        }
    }
}