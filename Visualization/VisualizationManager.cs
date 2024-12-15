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
        public bool CanStepBackward() => _stepIndex > 0;

        /// <summary>
        /// Warms up the visualization manager by running the algorithm on the graph,
        /// creating a list of all the states of the graph that the algorithm goes through.
        /// </summary>
        /// <param name="graphVM">The initial state of the graph.</param>
        /// <param name="algorithm">The algorithm to be run.</param>
        /// <returns>A visualization manager with the list of states.</returns>
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
        /// <summary>
        /// Steps backward in the algorithm visualization process.
        /// </summary>
        /// <returns>The previous state of the graph.</returns>
        public GraphVM StepBackward()
        {
            if (_stepIndex == 0)
                return _states[_stepIndex];
            if (_stepIndex == _states.Count - 1)
                _stepIndex--;
            var state = _states[_stepIndex];
            _stepIndex--;
            return state;
        }

        /// <summary>
        /// Creates a deep copy of the state of the graph.
        /// </summary>
        /// <param name="graphVM">The state of the graph to be cloned.</param>
        /// <returns>The cloned state of the graph.</returns>
        private static GraphVM CloneState(GraphVM graphVM)
        {
            var clone = graphVM.Clone() as GraphVM;
            return clone!;
        }

    }
}
