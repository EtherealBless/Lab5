using System.Collections.Generic;
using System.Windows.Media;

namespace GraphEditor.Algorithms.Steps.Nodes;

public class UpdateNodeStep : INodeStep
{
    public int NodeId { get; }
    private int _count = 0;
    public Color? Color => new Color?[] { Constants.StepsColors.NodeColor, Constants.StepsColors.SelectedNodeColor, null }[_count++ % 3];

    public bool IsPermanent => true;

    public UpdateNodeStep(int nodeId) => NodeId = nodeId;
}