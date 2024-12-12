using GraphEditor.Constants;
using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphEditor.Algorithms.Steps.Nodes
{
    internal class CheckedNodeStep : INodeStep
    {
        public NodeVM Node { get; }
        public Color Color => StepsColors.CheckedNodeColor;
        public bool IsPermanent => true;

        public CheckedNodeStep(NodeVM node) => Node = node;
    }
}
