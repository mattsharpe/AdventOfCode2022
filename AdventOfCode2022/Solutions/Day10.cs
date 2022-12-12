using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdventOfCode2022.Solutions
{
    internal class Day10
    {
        public int Part1(string[] input)
        {
            return GetSignalStrengths(input).size;
        }

        public string Part2(string[] input)
        {
            return GetSignalStrengths(input, true).output;
        }

        private (int size,string output) GetSignalStrengths(string[] input, bool part2 = false)
        {
            var register = 1;
            var cycles = 0;
            var result = 0;
            var output = new StringBuilder();

            void RunCycle()
            {
                if (!part2)
                {
                    cycles++;
                    if (cycles % 40 != 20) return;
                    result += cycles * register;
                    Console.WriteLine($"{cycles}  {register}");
                }
                else
                {
                    //the end of a CRT display
                    if (cycles % 40 == 0 && cycles != 0)
                    {
                        output.AppendLine();
                    }

                    output.Append(Math.Abs(register - cycles % 40) <= 1 ? '#' : '.');
                    cycles++;
                }
            }

            foreach (var line in input)
            {
                
                if (line == "noop")
                {
                    RunCycle();
                }
                else
                {
                    RunCycle();
                    RunCycle();

                    var size = int.Parse(line.Split(" ")[1]);
                    register += size;
                }
            }

            return (result, output.ToString());
        }
    }
}