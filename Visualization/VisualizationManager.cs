using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GraphEditor.Algorithms;
using GraphEditor.Algorithms.Steps;
using GraphEditor.ViewModels;

namespace GraphEditor.Visualization
{
    internal class VisualizationManager
    {

        private List<GraphVM> _states = new List<GraphVM>();
        private List<IStep> _steps = new List<IStep>();
        private int _stepIndex = 0;

        public bool CanStepForward() => _stepIndex < _states.Count;

        public static async Task<VisualizationManager> WarmUp(GraphVM graphVM, IAlgorithm algorithm)
        {

            graphVM = CloneState(graphVM);
            var manager = new VisualizationManager();
            await foreach (var step in algorithm.RunAlgorithm(graphVM.Graph))
            {
                manager._states.Add(CloneState(graphVM));
                graphVM.ApplyStep(step);
                // await Task.Delay(500);
            }

            manager._states.Add(CloneState(graphVM));

            // await Task.Delay(1000);

            // graphVM.Graph = _states[0].Graph;
            // graphVM.NodesVM = _states[0].NodesVM;
            // graphVM.EdgesVM = _states[0].EdgesVM;
            return manager;
        }

        /// <summary>
        /// Steps forward in the algorithm visualization process.
        /// </summary>
        /// <returns>The next state of the graph.</returns>
        public GraphVM StepForward()
        {
            var state = _states[_stepIndex];
            _stepIndex++;
            return state;
        }

        private static GraphVM CloneState(GraphVM graphVM)
        {
            var clone = new GraphVM(graphVM.Graph);
            var nodes = graphVM.NodesVM.Select(x => CloneNode(x)).ToList();
            var edges = graphVM.EdgesVM.Select(x => CloneEdge(x)).ToList();

            clone.NodesVM = nodes;
            clone.EdgesVM = edges;
            return clone;
        }

        private static NodeVM CloneNode(NodeVM nodeVM)
        {
            return new NodeVM(nodeVM.Node, nodeVM.X, nodeVM.Y, nodeVM.Color);
        }

        private static EdgeVM CloneEdge(EdgeVM edgeVM)
        {
            return new EdgeVM(edgeVM.NodeFrom, edgeVM.NodeTo);
        }
    }
}
