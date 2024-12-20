using GraphEditor.Algorithms.Steps.Edges;
using GraphEditor.Algorithms.Steps;
using GraphEditor.Algorithms;
using GraphEditor.ViewModels;
using GraphEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class KruskalAlgorithm : IAlgorithm
{
    public string Name => "Kruskal's Algorithm";

    public async IAsyncEnumerable<IStep> RunAlgorithm(Graph graph)
    {
        var spanningTreeEdges = new List<Edge>(); // Остовное дерево
        var parent = new Dictionary<Node, Node>();

        foreach (var node in graph.Nodes)
        {
            parent[node] = node;
        }

        Node Find(Node node)
        {
            if (parent[node] != node)
            {
                parent[node] = Find(parent[node]);
            }
            return parent[node];
        }

        void Union(Node node1, Node node2)
        {
            var root1 = Find(node1);
            var root2 = Find(node2);
            if (root1 != root2)
            {
                parent[root2] = root1;
            }
        }

        Console.WriteLine("Starting Kruskal's algorithm.");

        // Сортировка рёбер по весу
        var edges = graph.Edges.OrderBy(e => e.Weight).ToList();

        foreach (var edge in edges)
        {
            Console.WriteLine($"Processing edge {edge.Source.Name} ↔ {edge.Target.Name}, Weight: {edge.Weight}");

            // Проверка на циклы
            if (Find(edge.Source) != Find(edge.Target))
            {
                // Добавление ребра в остовное дерево и визуализация
                spanningTreeEdges.Add(edge); 
                Union(edge.Source, edge.Target);

                yield return new MarkEdgeStep(edge.Id);
                yield return new SelectEdgeStep(edge.Id);
            }
            else
            {
                Console.WriteLine($"Skipping edge {edge.Source.Name} ↔ {edge.Target.Name}, Weight: {edge.Weight} (cycle detected).");
                yield return new CheckedEdgeStep(edge.Id);
            }
        }

        Console.WriteLine("Algorithm completed.");
    }
}
