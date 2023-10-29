using System.Text.RegularExpressions;

namespace AdventOfCode2022.Solutions
{
    internal class Day19
    {
        public int Part1(string[] input)
        {
            return ParseInput(input).Sum(x => x.QualityLevel());
        }

        public int Part2(string[] input)
        {
            return ParseInput(input)
                    .Take(3)
                    .Select(x=>x.MaxGeodes(32))
                    .Aggregate((x, y) => x * y);
        }

        private IEnumerable<Blueprint> ParseInput(string[] input)
        {
            return input.Select(x => new Blueprint(x));
        }
    }

    internal record Robot(int Id, Resources Cost, Resources Produces);
    internal record State(int Time, Resources Available, Resources Producing, int BlackList);
    internal record Resources(int Ore, int Clay, int Obsidian, int Geode)
    {
        public Resources Add(Resources other) => new(Ore + other.Ore, Clay + other.Clay, Obsidian + other.Obsidian, Geode + other.Geode);
        public Resources Subtract(Resources other) => new(Ore - other.Ore, Clay - other.Clay, Obsidian - other.Obsidian, Geode - other.Geode);
    }

    internal class Blueprint
    {
        private readonly int _id;
        private readonly Resources _maxCost;

        public List<Robot> Robots { get; set; }

        public Blueprint(string blueprint)
        {

            var numbers = Regex.Matches(blueprint, @"\d+", RegexOptions.Compiled).Select(m => int.Parse(m.Value)).ToArray();

            _id = numbers[0];

            Robots = new List<Robot>
            {
                new(1, Cost: new Resources(numbers[1], 0, 0, 0), new Resources(1, 0, 0, 0)),
                new(2, Cost: new Resources(numbers[2], 0, 0, 0), new Resources(0, 1, 0, 0)),
                new(4, Cost: new Resources(numbers[3], numbers[4], 0, 0), new Resources(0, 0, 1, 0)),
                new(8, Cost: new Resources(numbers[5], 0, numbers[6], 0), new Resources(0, 0, 0, 1)),
            };

            _maxCost = new Resources(
                Ore: Robots.Select(robot => robot.Cost.Ore).Max(),
                Clay: Robots.Select(robot => robot.Cost.Clay).Max(),
                Obsidian: Robots.Select(robot => robot.Cost.Obsidian).Max(),
                Geode: int.MaxValue
            );

        }

        public int QualityLevel()
        {
            return MaxGeodes(24) * _id;
        }
        
        public int MaxGeodes(int timeLimit)
        {

            var toExplore = new PriorityQueue<State, int>();
            var explored = new HashSet<State>();
            var initialState = new State(timeLimit, new Resources(0, 0, 0, 0), new Resources(1, 0, 0, 0), 0);
            
            AddToQueue(initialState);

            var max = 0;
            while (toExplore.Count > 0)
            {
                var state = toExplore.Dequeue();

                //if the rest of the queue has lower potential then stop
                if (MaximumPotentialGeodes(state) < max)
                {
                    break;
                }

                if (explored.Contains(state))
                {
                    continue;
                }

                explored.Add(state);

                if (state.Time == 0)
                {
                    max = Math.Max(max, state.Available.Geode);
                }
                else
                {
                    //Generate the next states to explore - we have 5 options, build one of the robots or wait another minute for more resources
                    
                    //which robots can we afford to build - this generates the next states to explore
                    var affordableRobots = Robots
                        .Where(robot => state.Available.Ore >= robot.Cost.Ore &&
                                        state.Available.Clay >= robot.Cost.Clay &&
                                        state.Available.Obsidian >= robot.Cost.Obsidian).ToList();

                    
                    foreach (var robot in affordableRobots)
                    {
                        if (ShouldBuild(state, robot))
                        {
                            AddToQueue(new
                                State(Time: state.Time - 1,
                                    Available: state.Available.Add(state.Producing).Subtract(robot.Cost),
                                    Producing: state.Producing.Add(robot.Produces),
                                    BlackList: 0));
                        }
                    }

                    //dont build anything, wait until the next tick so we have more resources
                    AddToQueue(
                        state with
                        {
                            Time = state.Time - 1,
                            Available = state.Available.Add(state.Producing),
                            BlackList = affordableRobots.Sum(robot => robot.Id),
                        }
                    );
                }
            }

            return max;

            //Assume can build a geode robot each turn, what is the absolute max we would be producing from here?
            int MaximumPotentialGeodes(State state)
            {
                var future = (state.Producing.Geode * state.Time) + (state.Time - 1) * state.Time / 2;
                return state.Available.Geode + future;
            }

            // Should we build this Robot based on the current state?
            bool ShouldBuild(State state, Robot robot)
            {
                //check if we've already decided not to build this type
                if ((state.BlackList & robot.Id) != 0)
                {
                    return false;
                }

                var futureProduction = state.Producing.Add(robot.Produces);
                return futureProduction.Ore <= _maxCost.Ore &&
                        futureProduction.Clay <= _maxCost.Clay &&
                        futureProduction.Obsidian <= _maxCost.Obsidian;
            }

            
            //Work out hte max geodes we could make from this state and use as the queue priority
            // smaller numbers have higher priority so invert the max geode value
            void AddToQueue(State state)
            {
                var priority = -MaximumPotentialGeodes(state);
                toExplore.Enqueue(state, priority);
            }
        }

    }
}