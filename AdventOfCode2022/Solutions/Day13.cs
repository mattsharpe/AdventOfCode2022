using System.Text.Json;

namespace AdventOfCode2022.Solutions
{
    internal class Day13
    {
        public int Part1(string[] input)
        {
            var things = ParseInput(input)
                .Select((packet, i) => JsonCompare(packet.a, packet.b) < 0 ? i + 1 : 0);

            return things.Sum();
        }

        public int Part2(string[] input)
        {
            var updatedInput = input.ToList();
            updatedInput.AddRange(new[] { "", "[[2]]", "[[6]]" });
            
            var packets = ParseInput(updatedInput);
            
            //get all the packets in a flat list
            var list = packets.SelectMany(x => new[] { x.a, x.b }).ToList();
            list.Sort(JsonCompare);

            var stringified = list.Select(x => x.ToString()).ToList();
            return (stringified.IndexOf("[[2]]") + 1) * (stringified.IndexOf("[[6]]") + 1);

        }
        private IEnumerable<(JsonElement a, JsonElement b)> ParseInput(IEnumerable<string> input)
        {
            var packets = input.Chunk(3).Select(group => (JsonSerializer.Deserialize<JsonElement>(group[0]),
                JsonSerializer.Deserialize<JsonElement>(group[1])));

            return packets;
        }

        private int JsonCompare(JsonElement left, JsonElement right)
        {
            return left.ValueKind switch
            {
                JsonValueKind.Number when right.ValueKind == JsonValueKind.Number => 
                    left.GetInt32() - right.GetInt32(),

                JsonValueKind.Array when right.ValueKind == JsonValueKind.Array => 
                    JsonCompare(left.EnumerateArray().ToArray(), right.EnumerateArray().ToArray()),

                JsonValueKind.Number when right.ValueKind == JsonValueKind.Array => 
                    JsonCompare(new[] { left }, right.EnumerateArray().ToArray()),

                JsonValueKind.Array when right.ValueKind == JsonValueKind.Number => 
                    JsonCompare(left.EnumerateArray().ToArray(), new[] { right }),

                _ => throw new NotImplementedException()
            };
        }

        private int JsonCompare(JsonElement[] left, JsonElement[] right)
        {
            foreach (var (a, b) in left.Zip(right))
            {
                var c = JsonCompare(a, b);
                if (c != 0)
                {
                    return c;
                }
            }
            return left.Length - right.Length;
        }
    }
}