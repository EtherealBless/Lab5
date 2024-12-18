using System.Windows.Media;
using GraphEditor.ViewModels;

namespace GraphEditor.Algorithms.Steps.Edges;

public class SelectEdgeStep : IEdgeStep
{
    public int Id { get; set; }

    public Color? Color => Constants.StepsColors.SelectedEdgeColor;

    public bool IsPermanent => false;

    public SelectEdgeStep(int id) => Id = id;
}