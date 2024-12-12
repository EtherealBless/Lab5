using System.Collections.Generic;

namespace GraphEditor.ViewModels;

public class Graph
{
    public List<Node> Nodes { get; set; } = new List<Node>();
    public List<Edge> Edges { get; set; } = new List<Edge>();
}