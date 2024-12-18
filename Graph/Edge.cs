namespace GraphEditor;

public class Edge
{
    public int Id { get; set; }
    public Node Source { get; set; }
    public Node Target { get; set; }
    public bool IsDirected { get; set; }
    public double Weight { get; set; } = 10;

    public Edge(Node source, Node target, bool isDirected = false, int id = 0)
    {
        Source = source;
        Target = target;
        IsDirected = isDirected;
        Id = id;
    }
}