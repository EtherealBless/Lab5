using System.Windows.Media;
using GraphEditor.ViewModels;

namespace GraphEditor.Algorithms.Steps.Edges;

public class CheckedEdgeStep : IEdgeStep
{
    public int Id { get; set; }

    public Color? Color => Constants.StepsColors.CheckedEdgeColor;

    public bool IsPermanent => true;

    public CheckedEdgeStep(int id) => Id = id;
}