using System.Collections.Generic;

namespace GraphEditor.ViewModels;

public class Graph
{
    public List<NodeVM> Nodes { get; set; } = new List<NodeVM>();
    public List<EdgeVM> Edges { get; set; } = new List<EdgeVM>();
}