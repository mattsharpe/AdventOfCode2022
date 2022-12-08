namespace AdventOfCode2022.Solutions
{
    internal class Day07
    {
        public long Part1(string[] input)
        {
            var directories = ParseInput(input);
                
            return directories.Where(x => x.Size < 100000)
                                .Sum(x=>x.Size);
        }
        
        public int Part2(string[] input)
        {
            var directories = ParseInput(input);

            var usedSpace = directories.Single(x => x.Path == "/").Size;
            var unUsedSpace = 70_000_000 - usedSpace;
            var required = 30_000_000 - unUsedSpace;

            Console.WriteLine(required);

            return directories.Where(x => x.Size >= required).Min(x => x.Size);
        }

        private List<Directory> ParseInput(string[] input)
        {
            var workingDirectory = new Stack<string>();

            var directories = new Dictionary<string, Directory>
            {
                {  "/", new Directory{ Path = "/"} }
            };

            foreach (var item in input)
            {
                if (item.StartsWith("$ "))
                {
                    if (item == "$ cd ..")
                    {
                        workingDirectory.Pop();
                    }
                    else if (item == "$ cd /")
                    {
                        workingDirectory.Push("/");
                    }
                    else if (item.StartsWith("$ cd "))
                    {
                        var currentDir = directories[string.Join("/", workingDirectory)];
                        workingDirectory.Push(item[5..]);

                        var path = string.Join("/", workingDirectory);
                        var directory = directories.GetValueOrDefault(path);
                        if (directory == null)
                        {
                            directory = new Directory { Path = path };
                            directories[directory.Path] = directory;
                        }

                        currentDir.Directories.Add(directory);
                    }
                    
                }
                else if (!item.StartsWith("dir"))
                {
                    var file = (item.Split(" ")[1], Convert.ToInt32(item.Split(" ")[0]));

                    var directory = directories[string.Join("/", workingDirectory)];
                    directory.Files.Add(file);
                }
            }
            ;
            return directories.Values.ToList();

        }
    }

    class Directory
    {
        public string Path { get; set; }
        public List<Directory> Directories { get; set;  } = new();
        public List<(string name, int size)> Files { get; set; } = new();

        public int Size => Files.Sum(x => x.size) + Directories.Sum(x => x.Size);
    }
}