namespace GraphEditor;

public class Edge
{
    public Node Source { get; set; }
    public Node Target { get; set; }
    public bool IsDirected { get; set; }

    public Edge(Node source, Node target, bool isDirected = false)
    {
        Source = source;
        Target = target;
        IsDirected = isDirected;
    }
}