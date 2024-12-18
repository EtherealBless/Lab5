using System;
using System.Collections.Generic;
using System.Linq;
using GraphEditor.ViewModels;

namespace GraphEditor.YunPart
{
    public class ShortestPathLogic
    {
        public List<NodeVM> FindShortestPath(NodeVM startNode, NodeVM endNode, GraphVM graphVM)
        {
            var distances = new Dictionary<NodeVM, double>();
            var previousNodes = new Dictionary<NodeVM, NodeVM>();
            var priorityQueue = new SortedSet<(double, NodeVM)>(Comparer<(double, NodeVM)>.Create((x, y) => x.Item1.CompareTo(y.Item1)));

            foreach (var node in graphVM.NodesVM)
            {
                distances[node] = double.PositiveInfinity;
                previousNodes[node] = null;
            }

            distances[startNode] = 0;
            priorityQueue.Add((0, startNode));

            while (priorityQueue.Count > 0)
            {
                var (currentDistance, currentNode) = priorityQueue.First();
                priorityQueue.Remove((currentDistance, currentNode));

                if (currentNode == endNode)
                {
                    break;
                }

                foreach (var edge in graphVM.EdgesVM.Where(e => e.NodeFrom == currentNode || e.NodeTo == currentNode))
                {
                    var neighbor = edge.NodeFrom == currentNode ? edge.NodeTo : edge.NodeFrom;
                    var distance = currentDistance + edge.Weight;

                    if (distance < distances[neighbor])
                    {
                        distances[neighbor] = distance;
                        previousNodes[neighbor] = currentNode;
                        priorityQueue.Add((distance, neighbor));
                    }
                }
            }

            var path = new List<NodeVM>();
            for (var at = endNode; at != null; at = previousNodes[at])
            {
                path.Add(at);
            }

            path.Reverse();
            return path;
        }
    }
}
