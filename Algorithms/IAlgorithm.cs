using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphEditor.Algorithms.Steps;
using GraphEditor.ViewModels;

namespace GraphEditor.Algorithms
{
    public interface IAlgorithm
    {
        public IAsyncEnumerable<IStep> RunAlgorithm(Graph graph);
    }
}
    