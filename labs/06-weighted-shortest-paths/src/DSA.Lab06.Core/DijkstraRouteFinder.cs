using System;
using System.Collections.Generic;

namespace DSA.Lab06
{
    public sealed class DijkstraRouteFinder
    {
        public RouteResult FindRoute(WeightedGraph graph, string start, string goal)
        {
            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            if (!graph.ContainsNode(start) || !graph.ContainsNode(goal))
            {
                return RouteResult.NotFound(0);
            }

            if (start == goal)
            {
                return RouteResult.Success(new[] { start }, 0, 0);
            }

            var bestCost = new Dictionary<string, int>();
            var cameFrom = new Dictionary<string, string>();
            var frontier = new MinFrontier<string>();

            bestCost[start] = 0;
            frontier.Enqueue(start, 0); // added node

            int expandedNodes = 0;

            while (frontier.Count > 0)
            {
                MinFrontier<string>.Entry entry = frontier.Dequeue();// get next node

                if (bestCost.TryGetValue(entry.Value, out int cost) && cost != entry.Priority)// ignore the stale entries
                {
                    continue;
                }
                expandedNodes++;                                                              // expand current nodes

                if (entry.Value == goal)
                {
                    List<string> path = new List<string>();
                    string currentNode = goal;

                    while (currentNode != start)                                   // reconstruct
                    {
                        path.Add(currentNode);
                        currentNode = cameFrom[currentNode];
                    }
                    path.Add(start);
                    path.Reverse();
                    return RouteResult.Success(path, bestCost[goal], expandedNodes);
                }

                foreach (WeightedEdge edge in graph.GetOutgoing(entry.Value))//I'm gonna lose it! // relax each outgoing edge (getting the cost)
                {
                    int candidate = bestCost[entry.Value] + edge.Cost;
                    if (!bestCost.TryGetValue(edge.To, out int knownCost) || knownCost > candidate)
                    {
                        bestCost[edge.To] = candidate;
                        cameFrom[edge.To] = entry.Value;
                        frontier.Enqueue(edge.To, candidate);
                    }
                }
            }
            return RouteResult.NotFound(expandedNodes);

            // TODO Lab 6A:
            // 1. Repeatedly remove the smallest-priority frontier candidate.
            // 2. Ignore stale entries when a cheaper cost is already known.
            // 3. Finalize/expand the current node.
            // 4. If it is the goal, reconstruct the route using cameFrom.
            // 5. Relax each outgoing edge:
            //      candidate = bestCost[current] + edge.Cost
            //    If candidate improves the known cost, update both bestCost and cameFrom,
            //    then enqueue the neighbor with the improved total cost.
            //
            // Implement one small piece at a time and run the relevant tests.
        }
    }
}