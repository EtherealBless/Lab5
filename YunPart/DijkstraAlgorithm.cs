using GraphEditor.Algorithms;
using GraphEditor.Algorithms.Steps;
using GraphEditor.Algorithms.Steps.Edges;
using GraphEditor.Algorithms.Steps.Nodes;
using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphEditor.YunPart
{
    public class DijkstraAlgorithm : IAlgorithm
    {
        public (List<Node>, List<IStep>) FindShortestPath(Node startNode, Node endNode, Graph Graph)
        {
            var steps = new List<IStep>();
            var distances = new Dictionary<Node, double>();
            var previousNodes = new Dictionary<Node, Node>();
            var priorityQueue = new PriorityQueue<(double, Node), double>();
            var lastEdgeId = 0;
            foreach (var node in Graph.Nodes)
            {
                distances[node] = double.PositiveInfinity;
                previousNodes[node] = null!;
            }

            distances[startNode] = 0;
            priorityQueue.Enqueue((0, startNode), 0);

            while (priorityQueue.Count > 0)
            {
                var (currentDistance, currentNode) = priorityQueue.Dequeue();

                steps.Add(new UpdateNodeStep(currentNode.Id));

                if (currentNode == endNode)
                {
                    steps.Add(new SelectEdgeStep(lastEdgeId));
                    steps.Add(new SelectNodeStep(currentNode.Id));
                    break;
                }

                foreach (var edge in currentNode.Edges)
                {
                    var neighbor = edge.Source == currentNode ? edge.Target : edge.Source;
                    var distance = currentDistance + edge.Weight;
                    steps.Add(new SelectEdgeStep(edge.Id));
                    if (distance < distances[neighbor])
                    {
                        distances[neighbor] = distance;
                        previousNodes[neighbor] = currentNode;
                        priorityQueue.Enqueue((distance, neighbor), distance);
                        steps.Add(new MarkEdgeStep(edge.Id));
                    }
                    lastEdgeId = edge.Id;
                }
            }

            var path = new List<Node>();
            for (var at = endNode; at != null; at = previousNodes[at])
            {
                path.Add(at);
            }

            path.Reverse();
            return (path, steps);
        }

        public async IAsyncEnumerable<IStep> RunAlgorithm(Graph Graph)
        {
            var (path, steps) = FindShortestPath(Graph.StartNode ?? Graph.Nodes.First(), Graph.EndNode ?? Graph.Nodes.Last(), Graph);
            foreach (var step in steps)
            {
                yield return step;
            }
            yield return new CheckedNodeStep(path[0].Id);
            for (int i = 1; i < path.Count; i++)
            {
                yield return new CheckedEdgeStep(path[i - 1].Edges.First(e => e.Target == path[i] || e.Source == path[i]).Id);
                yield return new CheckedNodeStep(path[i].Id);
            }
            yield return new UpdateNodeStep(path.Last().Id);
        }
    }
}
