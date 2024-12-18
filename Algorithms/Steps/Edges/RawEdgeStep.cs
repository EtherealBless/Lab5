using System.Windows.Media;

namespace GraphEditor.Algorithms.Steps.Edges;

class RawEdgeStep : IEdgeStep
{
    public int Id { get; set; }

    public Color? Color{ get; set; }

    public bool IsPermanent => false;

    public RawEdgeStep(int id, Color? color)
    {
        Id = id;
        Color = color;
    }
}