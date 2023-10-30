using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using System.Text;

namespace AdventOfCode2022.Solutions
{
    internal class Day21
    {
        public long Part1(string[] input)
        {
            var properties = ParseInput(input);
            return CompileMonkeys(properties).root;
        }

        public long Part2(string[] input)
        {
            var properties = ParseInput(input);
            var monkeys = CompileMonkeys(properties);

            bool HumanInfluenced(string name)
            {
                monkeys.humn = 0;
                var initial = monkeys.Property(name);
                monkeys.humn = 123;
                return monkeys.Property(name) == initial;
            }

            var rootFields = input.Single(x => x.StartsWith("root:")).Split(": ")[1].Split(" + ");

            var targetNumber = rootFields.Single(x => !HumanInfluenced(x));
            var myNumber = rootFields.Single(HumanInfluenced);

            Console.WriteLine($"{targetNumber} {monkeys.Property(targetNumber)} == {myNumber} {monkeys.Property(myNumber)}");

            long low = 0;
            var high = long.MaxValue; // Replace this with the highest possible value for 'current'

            while(monkeys.Property(targetNumber) != monkeys.Property(myNumber))
            {
                var mid = (low + high) / 2;
                monkeys.humn = mid; 

                if (monkeys.Property(targetNumber) < monkeys.Property(myNumber))
                {
                    high = mid - 1;
                }
                else if (monkeys.Property(targetNumber) > monkeys.Property(myNumber))
                {
                    low = mid + 1;
                }
                else
                {
                    //Multiple answers are possible so we need to find the lowest.
                    while(monkeys.Property(targetNumber) == monkeys.Property(myNumber))
                    {
                        monkeys.humn--;
                    }
                    return monkeys.humn + 1;
                }
            }
            return 0;
        }

        private IEnumerable<string> ParseInput(string[] input)
        {
            return input.Select(line => line.Split(": ")).Select(split => split[0] == "humn"
                ? $"public long {split[0]} = {split[1]};"
                : $"public long {split[0]} => {split[1]};");
        }

        public dynamic CompileMonkeys(IEnumerable<string> properties)
        {
            var sb = new StringBuilder();
            sb.AppendLine("public class Logic {");
            
            foreach(var property in properties)
            {
                sb.AppendLine(property);
            }

            sb.Append("""
                      public long Property(string name)
                      {
                          return (long)GetType().GetProperty(name).GetValue(this);
                      }
                      """);

            sb.AppendLine("}");
            
            var compilation = CSharpCompilation.Create(
                "Advent.dll",
                new[] { CSharpSyntaxTree.ParseText(sb.ToString()) },
                new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using var ms = new MemoryStream();
            compilation.Emit(ms);
            var assembly = Assembly.Load(ms.ToArray());
            var type = assembly.GetType($"Logic");
            return Activator.CreateInstance(type);
        }
    }
}