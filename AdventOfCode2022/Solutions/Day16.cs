using System.Text.RegularExpressions;

namespace AdventOfCode2022.Solutions
{

    internal class Day16
    {
      
        private TunnelMap ParseInput(string[] input)
        {
            var valveRegex = new Regex(@"Valve (\w+) has flow rate=(\d+)");
            int index = 0;

            var valves =  input.Select(line =>
            {
                var match = valveRegex.Match(line);
                var tunnels = Regex.Match(line, "to valves? (.*)").Groups[1].Value.Split(", ").ToArray();

                return new Valve(0, match.Groups[1].Value, int.Parse(match.Groups[2].Value), tunnels);
            }).OrderBy(x => x.Name)
                .Select(x=>x with {Id = index++})
                .ToDictionary(x => x.Name, x => x);
            
            //Floyd Warshall Algorithm
            
            var distances = new int[valves.Count, valves.Count];

            //work out the distance between each node
            foreach (var valve in valves.Values)
            {
                foreach (var otherValve in valves.Values)
                {

                    if (valve.Name == otherValve.Name)
                    {
                        distances[valve.Id, otherValve.Id] = 0;
                    } 
                    else if (valve.Tunnels.Contains(otherValve.Name))
                    {
                        distances[valve.Id, otherValve.Id] = 1;
                        distances[otherValve.Id, valve.Id] = 1;
                    }
                    else
                    {
                        distances[valve.Id, otherValve.Id] = 100;
                        distances[otherValve.Id, valve.Id] = 100;
                    }
                }
            }

            //explore all the valves and add paths between them
            foreach (var valve in valves.Values)
            foreach (var valve2 in valves.Values)
                for (var x = valve2.Id + 1; x < valves.Count; x++)
                {
                            if (distances[valve2.Id, valve.Id] + distances[valve.Id, x] >= distances[valve2.Id, x])
                            {
                                continue;
                            }
                            distances[valve2.Id, x] = distances[valve2.Id, valve.Id] + distances[valve.Id, x];
                            distances[x, valve2.Id] = distances[valve2.Id, valve.Id] + distances[valve.Id, x];
                }
           
            // now we have to minimise the search space by only considering valves that have a flow rate
            
            var valvesThatDontFlow = valves.Values.Where(x => x.Name != "AA" && x.FlowRate == 0).Select(x => x.Id).Reverse().ToList();

            var importantDistances = new int[distances.GetLength(0) - valvesThatDontFlow.Count, distances.GetLength(1) - valvesThatDontFlow.Count];
            
            // remove the distance calculations that we don't need -
            // we don't have to know the full path just how long it takes so we can prune routes involving valves that don't flow
            for (int i = 0, row = 0; i < distances.GetLength(0); i++)
            {
                if (valvesThatDontFlow.Contains(i))
                    continue;

                for (int j = 0, column = 0; j < distances.GetLength(1); j++)
                {
                    if (valvesThatDontFlow.Contains(j))
                        continue;

                    importantDistances[row, column] = distances[i, j];
                    column++;
                }
                row++;
            }
            
            var result = new TunnelMap(importantDistances, valves);
            return result;
        }


        private Dictionary<int, int> ProcessMinute(TunnelMap map, int time)
        {
            //cache to store the maximum flow rate for the current state of the valves / time
            Dictionary<int, int> bestScoreForState = new();
            
            var valvesThatFlow = map.Valves.Values.Where(x => x.Name == "AA" || x.FlowRate != 0).Select(x => x.Name).ToList();

            // Use bit masking to manage the state, makes my head hurt.
            // Tried string concatenation and hashset. CPU didn't like it
            var bitMask = Enumerable.Range(0, map.Distances.GetLength(0)).Select(x => 1 << x).ToArray();
            
            void Step(int node, int minute, int state, int flow)
            {
               
                //Are we at a better point with the current valves turned on than last time we were at this point? if so, update value
                bestScoreForState[state] = int.Max(bestScoreForState.GetValueOrDefault(state, 0), flow);

                for (var i = 0; i < valvesThatFlow.Count; i++) 
                {
                    //move to next node and extra minute to turn valve on
                    var newTime = minute - map.Distances[node, i] - 1;
                    var mask = (bitMask[i] & state) != 0;

                    if (newTime <= 0 || mask)
                        continue;
                    
                    var newState = state | bitMask[i];

                    var newFlow = flow + newTime * map.Valves[valvesThatFlow[i]].FlowRate;
                    
                    //Go to new valve, update state so it's turned on, add it's flow, repeat everything above.
                    Step(i, newTime, newState, newFlow);
                    
                }
            }

            Step(0, time, 0, 0);

            return bestScoreForState;
        }

        


        public int Part1(string[] input)
        {
            var map = ParseInput(input);
            
            var cache = ProcessMinute(map, 30);

            return cache.Values.Max();

        }
        public int Part2(string[] input)
        {
            var map = ParseInput(input);

            var cache = ProcessMinute(map, 26);

            int result = 0;
            //linq here adds about 2s to the run time :o
            foreach (var player in cache)
            {
                foreach (var elephant in cache)
                {
                    //only search the states where the elephant visits different valves
                    if ((player.Key & elephant.Key) == 0)
                    {
                        result = int.Max(result, player.Value + elephant.Value);

                    } 
                }
            }
            return result;
        }
        
    }

    record Valve(int Id, string Name, int FlowRate, string[] Tunnels);
    record TunnelMap(int[,] Distances, Dictionary<string,Valve> Valves);

    
}