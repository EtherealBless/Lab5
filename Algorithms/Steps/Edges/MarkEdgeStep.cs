using System.Windows.Media;

namespace GraphEditor.Algorithms.Steps.Edges;

class MarkEdgeStep : IEdgeStep
{
    public MarkEdgeStep(int id)
    {
        Id = id;
    }

    public int Id { get; set; }

    public Color? Color => Constants.StepsColors.MarkEdgeColor;

    public bool IsPermanent => true;
}