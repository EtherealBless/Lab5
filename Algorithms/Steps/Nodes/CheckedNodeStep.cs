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
    public class CheckedNodeStep : INodeStep
    {
        public int NodeId { get; }
        public Color? Color => StepsColors.CheckedNodeColor;
        public bool IsPermanent => true;

        public CheckedNodeStep(int nodeId) => NodeId = nodeId;
    }
}
