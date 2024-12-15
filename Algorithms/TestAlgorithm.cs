using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphEditor.Algorithms.Steps;
using GraphEditor.Algorithms.Steps.Nodes;
using GraphEditor.ViewModels;

namespace GraphEditor.Algorithms
{
    internal class TestAlgorithm : IAlgorithm
    {
        public string Name => "Test Algorithm";

        public async IAsyncEnumerable<IStep> RunAlgorithm(Graph graph)
        {
            yield return new SelectNodeStep(graph.Nodes.First().Id);
            yield return new CheckedNodeStep(graph.Nodes.First().Id);
            yield return new SelectNodeStep(graph.Nodes[1].Id);
            yield return new CheckedNodeStep(graph.Nodes[1].Id);
            if (graph.Nodes[3].Edges.Count > 0)
            {
                yield return new SelectNodeStep(graph.Nodes[3].Id);
            }
            Console.WriteLine("Done");
        }
    }
}
