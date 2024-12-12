using System.Collections.Generic;

namespace GraphEditor;
public class Node
{
    public Node(int id, string name)
    {
        Id = id;
        Name = name;
        Edges = new List<Edge>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Edge> Edges { get; set; }
}
