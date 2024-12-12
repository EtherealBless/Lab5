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
            yield return new SelectNodeStep(graph.Nodes.First());
            yield return new CheckedNodeStep(graph.Nodes.First());
            yield return new SelectNodeStep(graph.Nodes[1]);
            yield return new CheckedNodeStep(graph.Nodes[1]);
            Console.WriteLine("Done");
        }
    }
}
