namespace AdventOfCode2022.Solutions
{
    internal class Day20
    {
        public long Part1(string[] input)
        {
            var encrypted = ParseInput(input);
            var result = Mix(encrypted);
            return SumResult(result);
        }

        public long Part2(string[] input)
        {
            var encrypted = ParseInput(input, 811589153);
            var result = Mix(encrypted, 10);
            return SumResult(result);
        }

        private List<(long Value, int Index)> ParseInput(string[] input, long encryptionKey = 1)
        {
            //store the list as a pair of value and their original index.
            var numbers = input.Select(x => Convert.ToInt32(x) * encryptionKey)
                .Select((value, index) => (value, index)).ToList(); 
            
            return numbers;
        }

        private List<(long Value, int Index)> Mix(List<(long Value, int Index)> encrypted, int times = 1)
        {
            var result = new List<(long Value, int Index)>(encrypted);

            for (var i = 0; i < times; i++)
            {
                foreach (var number in encrypted)
                {
                    var oldIndex = result.IndexOf(number);
                    var newIndex = (oldIndex + number.Value) % (encrypted.Count - 1);

                    if (newIndex < 0)
                        newIndex = encrypted.Count + newIndex - 1;

                    result.Remove(number);
                    result.Insert((int)newIndex, number);
                }
            }


            return result;
        }

        private long SumResult(List<(long Value, int Index)> mixed)
        {
            //find the first instance of value 0, then we we want 1000 / 2000 / 3000 after this
            var indexZero = mixed.FindIndex(e => e.Value == 0);

            return new[] { 1000, 2000, 3000 }
                .Sum(x => mixed[(x + indexZero) % mixed.Count].Value);

        }
    }
}