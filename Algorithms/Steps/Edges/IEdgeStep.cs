using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Algorithms.Steps.Edges
{
    internal interface IEdgeStep : IStep
    {
        public EdgeVM Edge { get; set; }
    }
}
