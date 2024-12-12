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
    public class SelectNodeStep : INodeStep
    {
        public int NodeId { get; private set; }

        public Color Color => StepsColors.SelectedNodeColor;

        public bool IsPermanent => false;

        public SelectNodeStep(int nodeId)
        {
            NodeId = nodeId;
        }
    }
}
