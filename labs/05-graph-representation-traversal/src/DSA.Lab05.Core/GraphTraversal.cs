using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace DSA.Lab05
{
    /// <summary>
    /// Iterative graph traversals.
    ///
    /// Complete these methods after the adjacency representation is working.
    /// Think in terms of:
    /// frontier + visited + neighbors.
    /// </summary>
    public static class GraphTraversal
    {
        public static IReadOnlyList<string> BreadthFirstOrder(
            AdjacencyGraph graph,
            string start)
        {
            /* mark start visited
enqueue start

while frontier is not empty:
    dequeue current
    process current

    for each neighbor:
        if neighbor has not been visited:
            mark it visited
            enqueue it*/
            HashSet<string> visited = new HashSet<string>();
            Queue<string> frontier = new Queue<string>();
            List<string> result = new List<string>();

            visited.Add(start);
            frontier.Enqueue(start);
            while (frontier.Count > 0)
            {
                string current = frontier.Dequeue();

                result.Add(current);

                foreach (string neighbor in graph.GetNeighbors(current))
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        frontier.Enqueue(neighbor);
                    }
                }
            }
            return result;
        }

        public static IReadOnlyList<string> DepthFirstOrder(
            AdjacencyGraph graph,
            string start)
        {
            HashSet<string> visited = new HashSet<string>();
            Stack<string> frontier = new Stack<string>();
            List<string> result = new List<string>();

            visited.Add(start);
            frontier.Push(start);
            while (frontier.Count > 0)
            {
                string current = frontier.Pop();

                result.Add(current);
                IReadOnlyList<string> neighbors = graph.GetNeighbors(current);
                for (int i = neighbors.Count - 1; i >= 0; i--) //reverse order
                {
                    string neighbor = neighbors[i];
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        frontier.Push(neighbor);
                    }
                }
            }
            return result;
        }

        public static bool IsReachableBfs(
            AdjacencyGraph graph,
            string start,
            string target)
        {
            HashSet<string> visited = new HashSet<string>();
            Queue<string> frontier = new Queue<string>();

            visited.Add(start);
            frontier.Enqueue(start);
            while (frontier.Count > 0)
            {
                string current = frontier.Dequeue();
                if (current == target)
                {
                    return true;
                }

                foreach (string neighbor in graph.GetNeighbors(current))
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        frontier.Enqueue(neighbor);
                    }
                }
            }
            return false;
            /* As soon as the target is removed from the frontier, the answer is known:

 ```text
 target found → true
 frontier exhausted → false
 ```

 This is a useful distinction:

 ```text
 traverse everything reachable
 vs
 stop when the required answer is known*/
        }
    }
}