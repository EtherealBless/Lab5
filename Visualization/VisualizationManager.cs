using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GraphEditor.Algorithms;
using GraphEditor.Algorithms.Steps;
using GraphEditor.Algorithms.Steps.Edges;
using GraphEditor.Algorithms.Steps.Nodes;
using GraphEditor.ViewModels;
using System.Windows.Media;

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
        // <param name="graphVM">The initial state of the graph.</param>
        // <param name="algorithm">The algorithm to be run.</param>
        /// <returns>A visualization manager with the list of states.</returns>
        /// 




        //public static async Task<VisualizationManager> WarmUp(GraphVM graphVM, IAlgorithm algorithm)
        //{
        //    graphVM = CloneState(graphVM);
        //    var manager = new VisualizationManager();
        //    await foreach (var step in algorithm.RunAlgorithm(graphVM.Graph))
        //    {
        //        var steps = UnrollStep(step);
        //        foreach (var unrolledStep in steps)
        //        {
        //            manager._states.Add(CloneState(graphVM));
        //            graphVM.ApplyStep(unrolledStep);
        //        }
        //    }

        //    manager._states.Add(CloneState(graphVM));
        //    return manager;
        //}

        public static async Task<VisualizationManager> WarmUp(GraphVM graphVM, IAlgorithm algorithm)
        {
            graphVM = CloneState(graphVM);
            var manager = new VisualizationManager();
            await foreach (var step in algorithm.RunAlgorithm(graphVM.Graph))
            {
                var steps = UnrollStep(step);
                foreach (var unrolledStep in steps)
                {
                    manager._states.Add(CloneState(graphVM));
                    graphVM.ApplyStep(unrolledStep);
                }
            }

            manager._states.Add(CloneState(graphVM));
            return manager;
        }

        private static List<IStep> UnrollStep(IStep step)
        {
            switch (step)
            {
                case UpdateNodeStep updateNodeStep:
                    Color? color;
                    var steps = new List<RawNodeStep>();
                    while (true)
                    {
                        color = updateNodeStep.Color;
                        if (color == null)
                            break;
                        var newStep = new RawNodeStep(updateNodeStep.NodeId, color, false);
                        steps.Add(newStep);
                    };
                    steps.Last().IsPermanent = true;
                    return steps.Cast<IStep>().ToList();
            }
            return new() { step };
        }

        /// <summary>
        /// Steps forward in the algorithm visualization process.
        /// </summary>
        /// <returns>The next state of the graph.</returns>
        public GraphVM StepForward()
        {
            var state = _states[_stepIndex++];
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
            if (_stepIndex == _states.Count)
                _stepIndex--;
            var state = _states[_stepIndex--];
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
