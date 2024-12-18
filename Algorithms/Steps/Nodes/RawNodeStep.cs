using System.Windows.Media;

namespace GraphEditor.Algorithms.Steps.Nodes;

public class RawNodeStep : INodeStep
{
    public int NodeId { get; }
    public Color? Color { get; set; }

    public bool IsPermanent { get; set; }
    public RawNodeStep(int nodeId, Color? color, bool isPermanent) => (NodeId, Color, IsPermanent) = (nodeId, color, isPermanent);
}